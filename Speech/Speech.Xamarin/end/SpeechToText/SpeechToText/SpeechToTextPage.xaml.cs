using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SpeechToText
{
    public partial class SpeechToTextPage : ContentPage
    {
        const string AUTH_URL = "https://api.cognitive.microsoft.com/sts/v1.0";
        const string DICTATION_URL = "https://speech.platform.bing.com/speech/recognition/dictation/cognitiveservices/v1?language=en-US";

        // BING SPEECH API KEY
        const string AUTH_KEY = "FILL IN YOUR OWN KEY HERE";

        AudioRecorderService recorder;
        string authenticationToken;
        HttpClient client;

        public SpeechToTextPage()
        {
            InitializeComponent();

            // Instantiate the AudioRecorderService
            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

        }

        /**
        * Click-event on the RecordButton.
        **/
        async void Record_Clicked(object sender, EventArgs e)
        {
            await RecordAudio();
        }

        /**
        * Click-event on the ToTextButton.
        **/
        async void To_Text_Clicked(object sender, EventArgs e)
        {
            await ToText();
        }

        /**
        * Function that will get an authenticationToken 
        * if needed and then call and await the result
        * of the SpeechToTextRequest. It will Deserialize the result and
        * add the result of the text to the ResultLabel.
        **/
        async Task ToText()
        {
            if (authenticationToken == null)
            {
                authenticationToken = await FetchTokenAsync();
            }

            var response = JsonConvert.DeserializeObject<Response>(await SendSpeechToTextRequestAsync());

            ResultLabel.Text += " " + response.DisplayText;

            ToTextButton.IsEnabled = false;
        }




        // -------------------------- Audio Tasks --------------------------
        /**
        * Function that uses the Plugin.Audiorecorder Nuget package to record
        * audio and update the AudioRecorderService instance.
        * It also changes the button states and texts at some places.
        **/
        async Task RecordAudio()
        {

            if (!recorder.IsRecording) //Record button clicked
            {

                RecordButton.IsEnabled = false;

                //start recording audio
                var audioRecordTask = await recorder.StartRecording();

                RecordButton.Text = "Stop Recording";
                RecordButton.IsEnabled = true;

                await audioRecordTask;

                RecordButton.Text = "Record";
            }
            else //Stop button clicked
            {
                RecordButton.IsEnabled = false;

                //stop the recording...
                await recorder.StopRecording();

                RecordButton.IsEnabled = true;
                ToTextButton.IsEnabled = true;
            }
        }



		// -------------------------- API Tasks --------------------------
	   /**
        * Function that gets a Jason WebToken from the Cognitive API
        **/
		async Task<string> FetchTokenAsync()
        {
            if (client == null)
            {
                client = new HttpClient();
            }

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AUTH_KEY);
            UriBuilder uriBuilder = new UriBuilder(AUTH_URL);
            uriBuilder.Path += "/issueToken";

            var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
            return await result.Content.ReadAsStringAsync();
        }

        /**
        * Function that sends the audio FileStream to the speech API and
        * returns the response as a string.
        **/
        async Task<string> SendSpeechToTextRequestAsync()
        {
            var path = recorder.GetAudioFilePath();
            var content = new StreamContent(recorder.GetAudioFileStream());
            content.Headers.TryAddWithoutValidation("Content-Type", "audio/wav;codec=\"audio/pcm\";samplerate=16000");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            var response = await client.PostAsync(DICTATION_URL, content);

            return await response.Content.ReadAsStringAsync();
        }
    }




	// -------------------------- Response class --------------------------
	/**
     * Class that contains the fields of the repsonse of the Speech to Text API
     **/
	class Response {
        public string RecognitionStatus { get; set; }
        public string DisplayText { get; set; }
        public int Offset { get; set; }
        public int Duration { get; set; }
    }

}
