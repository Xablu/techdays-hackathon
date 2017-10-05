using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Plugin.Media;
using Xamarin.Forms;

namespace EmotionsRecogniser
{
    public partial class EmotionsRecogniserPage : ContentPage
    {
        public EmotionsRecogniserPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            // Initialize the Camera plugin
			await CrossMedia.Current.Initialize();

            // Start the camera and wait till the picture is taken
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Sample",
				Name = "test.jpg"
			});

            // Return if the file is empty
            if (file == null) {
                return;
            }

            // Set the image view source to the one from the picture
            PhotoImage.Source = ImageSource.FromStream(file.GetStream);

            // Start the ActivityIndicator
            ActivityIndicator.IsRunning = true;

            // Start and wait for the request to the Azure Emotion Api
            Emotion[] emotions = await GetHappiness(file.GetStream());

            // After you get the results, stop the ActivityIndicator again.
            ActivityIndicator.IsRunning = false;

            // Set the labels for their emotion score
            GetHighestEmotion(emotions);
		}

        // Function that tries to get the emotion scores from the Azure Emotion API
        // from a picture of a face.
        private static async Task<Emotion[]> GetHappiness(Stream stream)
		{
			string emotionKey = "REPLACE THIS WITH YOUR OWN API KEY";

			EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);

            var emotionResults = await emotionClient.RecognizeAsync(stream);

            if (emotionResults == null || emotionResults.Count() == 0)
			{
				throw new Exception("Can't detect face");
			}

			return emotionResults;
		}

        // Function that sets labels
        public void GetHighestEmotion(Emotion[] emotionResults)
		{

			foreach (var emotionResult in emotionResults)
			{
                AngerLabel.Text = "Anger: " + String.Format("{0:0.##}", emotionResult.Scores.Anger);
                ContemptLabel.Text = "Contempt: " + String.Format("{0:0.##}", emotionResult.Scores.Contempt);
                DisgustLabel.Text = "Disgust: " + String.Format("{0:0.##}", emotionResult.Scores.Disgust);
                FearLabel.Text = "Fear: " + String.Format("{0:0.##}", emotionResult.Scores.Fear);
                HappinessLabel.Text = "Happiness: " + String.Format("{0:0.##}", emotionResult.Scores.Happiness);
                NeutralLabel.Text = "Neutral: " + String.Format("{0:0.##}", emotionResult.Scores.Neutral);
                SadnessLabel.Text = "Sadness: " + String.Format("{0:0.##}", emotionResult.Scores.Sadness);
                SurpriseLabel.Text = "Surprise: " + String.Format("{0:0.##}", emotionResult.Scores.Surprise);
			}

		}
    }
}
