# React-Native Emotion API assigment
## Set-up:
1. Go to the dir you want to create your react app
2. Download react native with NPM command npm install -g react-native-cli (assuming you have node installed https://nodejs.org/en/download/)
3. Do command npm init AzureEmotions
4. Delete the package.json file
5. Clone or download [Github repository](https://github.com/Xablu/techdays-hackathon)
6. In the cloned repo go to Emotion/Emotion.React-Native/Start/AzureEmotions/ and copy package.json and index.js into your AzureEmotions project
7. Go back to your project AzureEmotions and do npm install
8. Do command react-native link
9. Add following permissions to your android manifest (android/app/src/main):     
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />                                               
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
10. Run the project by typing the command react-native run-(android OR ios)

## Assignment:
First you should get a free API key from Azure (https://azure.microsoft.com/en-us/try/cognitive-services/). It will give you two API keys and a endpoint for where to send your POST request. 

1. Create UI
    * Insert a Camera [component](https://github.com/lwansbrough/react-native-camera) 
    * Insert an Image component so you can display the image you just took.
    * Insert Textfields for displaying the response from the emotion API (anger, happiness, sadnes, etc..)
2. Processing image from camera
    * Get the path for the newly taken image (Path, not the mediaUri).
    * Load in the image and make it a base 64 string
    * Send the image to the Azure Emotions API using [react-native-fetch-blob](https://github.com/wkh237/react-native-fetch-blob)'s fetch method.
3. Display JSON response for user.


## Useful links
https://docs.microsoft.com/en-us/azure/cognitive-services/emotion/home
https://westus.dev.cognitive.microsoft.com/docs/services/5639d931ca73072154c1ce89/operations/563b31ea778daf121cc3a5fa/console
https://github.com/lwansbrough/react-native-camera 
https://github.com/wkh237/react-native-fetch-blob 
https://facebook.github.io/react-native/docs/getting-started.html