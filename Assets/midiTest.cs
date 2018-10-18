using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlsaSharp;
using LambMidi.Input;
public class midiTest : MonoBehaviour {
    [SerializeField]
    MidiListener listener;
    void Start(){
     listener.OnMessageRecieved += printmessage;
     listener.StartListening();
     Debug.Log(listener.isListening);   
    }
    void OnDisable(){
        listener.StopListening();
    }
    void printmessage(ChannelMessage cm){
        string res = "";
        foreach(var d in cm.data){
            res += d + " ";
        }
        print(res);
        //print(cm.length + " " + cm.start);
    }

}
