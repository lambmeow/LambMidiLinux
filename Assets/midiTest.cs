using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlsaSharp;
using System.Threading;
public class midiTest : MonoBehaviour {
    byte[] m_data = new byte[3];
    int m_Start = -1;
    int m_len = -1;

    public void Listening ()
    {
        using (var seq = new AlsaSequencer (AlsaIOType.Input, AlsaIOMode.NonBlocking)) {
			AlsaClientInfo clientInfo = new AlsaClientInfo { Client = -1 };
			while(seq.QueryNextClient(clientInfo)){
				print(clientInfo.Name + " ID:\t" + clientInfo.PortCount);
			}
		}
		
	}
				
    void Start(){
        Listening();
    }
    void Update(){
       
    }


}
