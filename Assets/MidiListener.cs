using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlsaSharp;
using System.Threading;

public class MidiListener : MonoBehaviour {
	[SerializeField]
	private string client_name;
	[SerializeField]
	private int midiPortNumber;
	
	private Thread thread;


	public void StartListening(){
		if(!SequencerManager.isRunning)
			SequencerManager.StartSequencer(AlsaIOType.Input,AlsaIOMode.NonBlocking);
	}

	static void CheckForNote(string n, int portnum){
		int appPort = SequencerManager.sequencer.CreateSimplePort (string.Format("{0}-listen",n), AlsaPortCapabilities.Write | AlsaPortCapabilities.NoExport, AlsaPortType.Application | AlsaPortType.MidiGeneric);
		var lp = -1;
		var cinfo = new AlsaClientInfo{Client = -1};
		while(SequencerManager.sequencer.QueryNextClient(cinfo))
			if(cinfo.Name.Contains(n))
				lp = cinfo.Client;
		SequencerManager.sequencer.ConnectFrom(appPort,lp,portnum);	
		//TODO: Add a start listener function that creates a channel message and sends it to a delegate event. 	
	}

}
public struct ChannelMessage {
	public byte[] data;
	public int start;
	public int length;

	public ChannelMessage(byte[] d, int s, int l){
		data = d;
		start = s;
		length = l;
	}
}

