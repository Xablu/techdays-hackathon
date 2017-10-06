## Emotion api app
#### Creating an emotion recognising android app in Xamarin.Forms with the Azure emotion API

##### Challenge:
* Create an android application with Xamarin.Forms to take a picture and display the emotion levels of the face(s) found using the [Azure Emotion API](https://azure.microsoft.com/nl-nl/services/cognitive-services/emotion/).

##### Set up:
1. [Download](https://www.visualstudio.com/downloads/) and install Visual Studio for your platform.
2. [Download or clone](https://github.com/Xablu/techdays-hackathon) the repository and navigate to the [correct start files](https://github.com/Xablu/techdays-hackathon/tree/master/Emotion/Emotion.Xamarin/Start/EmotionsRecogniser) and open EmotionRecogniser.sln in Visual Studio. The starter solution is a Xamarin.Forms solution which has the following Nuget packages installed and ready for use:
    * [Xam.Plugin.Media](https://github.com/jamesmontemagno/MediaPlugin)
    * Microsoft.ProjectOxford.Emotion: a useful Nuget package with methods built in for calls to the Azure Api ([Example](https://github.com/xamarin/mini-hacks/blob/master/microsoft-cognitive-services/Android.md))
3. To debug the app you will need either a physical android device or a virtual android device. 
(Note: When using a virtual device, make sure to set it up so that it uses the camera of your host device as you will need it for this app)
4. If everything went alright, you should be able to build and run the starter app in Visual Studio. (The app is just an empty white screen)
5. To use the Azure Emotion API, retrieve a free subscription key [here](https://azure.microsoft.com/en-us/try/cognitive-services/)

##### Assignment:
1. Create a UI in the EmotionsRecogniserPage.xaml containing at least:
    *  A button to open the camera
    *  An image to display the picture taken
    *  Some labels to display the results
2. Implement a click event on the button to open the camera and take a picture
    * Make sure to initialize the [Media Plugin](https://github.com/jamesmontemagno/MediaPlugin)
    * Take a picture and make sure to return a Stream
    * Set the source of the image to the picture
3. Send the picture to the Emotion API and display the results
    * Create a new EmotionServiceClient with your own Subscription key.
    * Retrieve the emotion results by calling the RecognizeAsync function and pass in the Stream.
    * Finally, display the results by setting the text values of the created labels


