## Speech to text api app
#### Creating a Text to Speech android app in Xamarin.Forms with the Bing Speech API

##### Challenge:
* Create an android application with Xamarin.Forms to take record audio and display the display what you said using the [Bing Speech API](https://azure.microsoft.com/en-us/services/cognitive-services/speech/).

##### Set up:
1. [Download](https://www.visualstudio.com/downloads/) and install Visual Studio for your platform.
2. [Download or clone](https://github.com/Xablu/techdays-hackathon) the repository and navigate to the [correct start files](https://github.com/Xablu/techdays-hackathon/tree/master/Speech/Speech.Xamarin/start/SpeechToText) and open SpeechToText.sln in Visual Studio. The starter solution is a Xamarin.Forms solution which has the following Nuget packages installed and ready for use:
    * [Plugin.AudioRecorder](https://github.com/NateRickard/Plugin.AudioRecorder): for easy cross.platform audio recording
    * [Newtonsoft.Json](https://www.newtonsoft.com/json): for serializing and deserializing JSON.
3. To debug the app a **physical android device is highly recommended**, because the app will need to record audio.
4. If everything went alright, you should be able to build and run the starter app in Visual Studio. (The app is just an empty white screen)
5. To use the Bing Speech Api, retrieve a free subscription key [here](https://azure.microsoft.com/en-us/try/cognitive-services/)

##### Assignment:
1. Create a UI
    * Add two buttons: one to start/stop recording and one to send your audio to the API.
    * Add a label to display the result text.
2. Audio
    * [Initialize an AudioRecorderService](https://github.com/NateRickard/Plugin.AudioRecorder/).
    * On the RecordButton click event, use the AudioRecorderSerivce instance to record audio.
3. Api
    * To send a request, you will need to request a Jason WebToken using your [free subscription key](https://azure.microsoft.com/en-us/try/cognitive-services/).
        * Create an instance of an HttpClient (save it in a class-wide variable for re-use) and add your subscription key to the DefaultRequestHeader as an "Ocp-Apim-Subscription-Key".
        * Send a PostAsync request to "https://api.cognitive.microsoft.com/sts/v1.0/issueToken" without a body. The content of the result can be read as a string, which is your Bearer token. Now you can make the request to the Bing Api and send your audio file:
        * First set your required bearer token to the HttpClient instance DefaultRequestHeaders.Authorization by setting it to a new AuthenticationHeaderValue.
        * Create a new StreamContent instance with the Stream of the recorded audiofile.
        * Set the Content-Type Headers of the streamcontent instance to the correct audio format: "audio/wav;codec=\"audio/pcm\";samplerate=16000".
        * Send a PostAsync request to the correct [http endpoint](https://docs.microsoft.com/en-us/azure/cognitive-services/speech/getstarted/getstartedrest?tabs=Powershell) and pass in the StreamContent instance. Await and read the content of the response as a string.
        * Finally, update the label for the result text with the response and test your app!

##### Useful links:
* [Bing Speech API documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/speech/home): Explanation about the Bing Speech API, requests, responses etc.
* [Developer.Xamarin Bing Speech example](https://developer.xamarin.com/guides/xamarin-forms/cloud-services/cognitive-services/speech-recognition/): for some useful code snippets