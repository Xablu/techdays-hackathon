# React-Native Emotion API assigment
## Set-up:
1. Create a directory in which you want to create your react application.
2. Open a terminal and navigate to the directory.
3. Enter the command 'react-native init AzureEmotions'. This will create an react-native application with the name AzureEmotions.
4. Navigate in to the AzureEmotions project and delete the package.json and index.js file.
5. Clone or download [Github repository](https://github.com/Xablu/techdays-hackathon)
6. In the cloned repo navigate to Emotion/Emotion.React-Native/Start/AzureEmotions/ and copy package.json and index.js into your AzureEmotions project
7. In the terminal (which is in your AzureEmotions directory) enter the command 'npm install'. When that is done, enter the command 'react-native link'.
8. Add the following permissions to you android manifest.xml (path: AzureEmotions/android/app/src/main):
    * '<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />'
    * '<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />'
    * '<uses-permission android:name="android.permission.CAMERA" />'
9. Run the project by entering the command 'react-native run-android' NB: Make sure you have an emulator running or connected device. **IF you are using an emulator you need to go to you AVD manager->advenced settings and set camera to webcam.**

## Assignment:
First you should get a [free API key](https://azure.microsoft.com/en-us/try/cognitive-services/) from Azure. It will give you two API keys and an endpoint for where to send your POST request. 

1. Create UI
    * Insert a Camera [component](https://github.com/lwansbrough/react-native-camera) 
    * Insert an Image component so you can display the image you just took.
    * Insert Textfields for displaying the response from the emotion API (anger, happiness, sadness, contempt, disgust, fear, neutral and suprise)
    * In react native you can add [styles](https://facebook.github.io/react-native/docs/stylesheet.html) to your component (CSS like), and position them as needed on the screen with [flexbox](http://facebook.github.io/react-native/releases/0.49/docs/flexbox.html#layout-with-flexbox).
2. Camera component setup:
    * Inside the [Camera component](https://github.com/lwansbrough/react-native-camera) you can add a button component with a onPress prop to trigger a function that will capture the image. 
    * You also need to make a reference of the camera so that you can interact with the component outside of the render() function. [see Usage](https://github.com/lwansbrough/react-native-camera)
3. Capture image function:
    * Here you need to call the camera.capture() function which will take an image for you. This function needs a [promise](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/then) for when its done. This promise will have two parameters (onSuccess, onfailure). The onSuccess parameter will contain a JSON object with a path to the picture. 
    * Now that you have the path for the image, you can use [react-native-fetch-blob's fs feature](https://github.com/wkh237/react-native-fetch-blob#user-content-file-system) to get the image file in base-64. The readFile() function also needs a promise, where the onSuccess parameter is the base-64 string.
    * Now that you have the image in base-64, it's time to send it to the Azure emotions API for an evaluation. Send the API post request with the help of [react-native-fetch-blob's fetch method](https://github.com/wkh237/react-native-fetch-blob#user-content-regular-request). with the API key and Content-Type(aplication/octet-stream) in the header, and the base-64 string in the body. 
    * In the promise (.then), take the JSON response and display emotion values for the user.
4. Displaying captured image:
    * When the image is captured, update the src of the imageview to the path of the captured image


## Useful links

* https://docs.microsoft.com/en-us/azure/cognitive-services/emotion/home
* https://westus.dev.cognitive.microsoft.com/docs/services/5639d931ca73072154c1ce89/operation/563b31ea778daf121cc3a5fa/console
* https://github.com/lwansbrough/react-native-camera 
* https://github.com/wkh237/react-native-fetch-blob
* https://facebook.github.io/react-native/docs/getting-started.html
