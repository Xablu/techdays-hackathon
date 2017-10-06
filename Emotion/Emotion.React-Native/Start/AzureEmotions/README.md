# Set up:
1. Go to the dir you want to create your react app
2. Download react native with NPM command npm install -g create-react-native-app (assuming you have node installed https://nodejs.org/en/download/)
3. Do command npm init AzureEmotions
4. Delete the package.json file
5. Go to https://github.com/Xablu/techdays-hackathon/ and clone git repository
6. In the cloned repo go to Emotion/Emotion.React-Native/Start/AzureEmotions/ and copy package.json and index.js into your AzureEmotions project
7. Go back to your project AzureEmotions and do npm install
8. Do command react-native link
9. Add following permissions to your android manifest (android/app/src/main):     
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />                                               
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
10. Run the project by typing the command react-native run-(android OR ios)

#Assignment: