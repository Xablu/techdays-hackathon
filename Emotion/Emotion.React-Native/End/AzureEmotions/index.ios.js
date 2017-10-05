import React, { Component } from 'react';
import {AppRegistry, View, Text, Image, ActivityIndicator, StyleSheet } from 'react-native';

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

        this.state = {
            anger: 0.0,
            contempt: 0.0,
            disgust: 0.0,
            fear: 0.0,
            hapiness: 0.0,
            neutral: 0.0,
            sadness: 0.0,
            surprise: 0.0,
            imageSource: 'home',
            pending: false,
            selfie: true, //Is the front camera active?
            cameraType: cameraTypes.front //will be back or front camera
        }
    }

    render() {
        return(
            <View style={styles.mainContainer}>

                <View style={styles.cameraContainer}>
                    <Camera
                    ref={(cam) =>{
                    this.camera = cam;
                    }}
                    style={styles.preview}
                    aspect={Camera.constants.Aspect.fill}
                    captureTarget={Camera.constants.CaptureTarget.cameraRoll}
                    type={this.state.cameraType}>

                        <View style={styles.buttonContainer}>
                            <Text
                                style={styles.capture}
                                onPress={() => this.takePicture()}>[CAPTURE]</Text>

                            <Text
                                style={styles.capture}
                                onPress={() => this.switchCamera()}>[Front/Back]</Text>
                        </View>

				    </Camera> 
                </View>
                
                <View style={[{flex: 1},{flexDirection: 'row'}]}>
                
                <View style={styles.imageContainer}>
                    <Image
                        style={styles.image}
                        source={{uri: this.state.imageSource}}></Image>
                </View>

                <View style={styles.contentContainer}>
                    <Text>Result:</Text>
                    <Text>Anger {this.state.anger}</Text>
                    <Text>Contempt {this.state.contempt}</Text>
                    <Text>Disgust {this.state.disgust}</Text>
                    <Text>Fear {this.state.fear}</Text>
                    <Text>Hapiness {this.state.hapiness}</Text>
                    <Text>Neutral {this.state.neutral}</Text>
                    <Text>Sadness {this.state.sadness}</Text>
                    <Text>suprise {this.state.surprise}</Text>
                </View>
                </View>
                {this.requestPending()}
            </View>
        );
    }

    
    requestPending = () => {
        if(this.state.pending) {
            return(
                <ActivityIndicator
                size='large'
                style={styles.loading}/>
            );
        } else {
            return null;
        }
    }

    switchCamera = () => {
		console.log('Switch camera')
		if(this.state.selfie) {
			this.setState({
			selfie: false,
			cameraType: cameraTypes.back
			});
		} else {
			this.setState({
			selfie: true,
			cameraType: cameraTypes.front
			});
		}
    }
  
    takePicture = () => {
		console.log('Take picture');
        const options = {};
        var self = this; //This not recognized in the nested scopes
        this.setState({
            pending: true
        });
		//options.location = ...
		this.camera.capture({metadata: options})
			.then((img) => {
               //First get the image file in base 64 
            RNFetchBlob.fs.readFile(img.path, 'base64')
                .then((imgData) => {
                    //Then send the base 64 string (will be converted to binary by the fetch blob library)
                    RNFetchBlob.fetch('POST', 'https://westus.api.cognitive.microsoft.com/emotion/v1.0/Recognize', 
                    {
                    "Content-Type":"application/octet-stream",
                    "Ocp-Apim-Subscription-Key":"86b31e5b87dd43848edfcb2f344c55f4"
                    },imgData)
                    .then((response) => {
                        //parse response
                        var parsedResponse = JSON.parse(response.data);
                        //change state
                        self.setState({
                            imageSource: img.path,
                            pending: false,
                            anger: parsedResponse[0].scores.anger.toFixed(2),
                            contempt: parsedResponse[0].scores.contempt.toFixed(2),
                            disgust: parsedResponse[0].scores.disgust.toFixed(2),
                            fear: parsedResponse[0].scores.fear.toFixed(2),
                            hapiness: parsedResponse[0].scores.happiness.toFixed(2),
                            neutral: parsedResponse[0].scores.neutral.toFixed(2),
                            sadness: parsedResponse[0].scores.sadness.toFixed(2),
                            surprise: parsedResponse[0].scores.surprise.toFixed(2)
                        });
                    })
                })
			
        })
    }
}


const styles = StyleSheet.create({
    mainContainer: {
        flex: 1,
        alignItems: 'center',
    },
    cameraContainer: {
        flex: 1
    },
    imageContainer: {
        flex: 2,
        justifyContent: 'center',
        alignItems: 'center'
    },
    contentContainer: {
        flex: 1,
        justifyContent: 'space-between',
        marginBottom: 40
    },
    image: {
        margin: 2,
        width: 200,
        height: 200
    },
    preview: {
        flex: 1,
        justifyContent: 'flex-end',
        alignItems: 'center'
      },
      capture: {
        flex: 0,
        backgroundColor: '#fff',
        borderRadius: 5,
        color: '#000',
        padding: 10,
        margin: 40
      },
      buttonContainer: {
        flexDirection: 'row',
        justifyContent: 'center'
      },
      loading: {
        position: 'absolute',
        left: 0,
        right: 0,
        top: 0,
        bottom: 0,
        alignItems: 'center',
        justifyContent: 'center'
      }
});


AppRegistry.registerComponent('AzureEmotions', () => App);
