import React, { Component } from 'react';
import {AppRegistry, View, Text, StyleSheet } from 'react-native';

import Camera from 'react-native-camera'; //Camera component
import RNFetchBlob from 'react-native-fetch-blob'; //Library for fetching local files and sending bianry data

//variables for switching between back and fron camera
const cameraTypes = {
	front: Camera.constants.Type.front,
	back: Camera.constants.Type.back
}; 

export default class App extends Component {

    constructor() {
        super();
    }

    //Render return the UI
    render() {
        return(
            <View style={styles.mainContainer}>
                <Text>Hello World</Text>
            </View>
        );
    }

}


const styles = StyleSheet.create({
    mainContainer: {
        flex: 1,
        alignItems: 'center',
    }
});
