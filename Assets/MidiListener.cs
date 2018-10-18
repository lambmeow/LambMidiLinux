using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlsaSharp;


namespace LambMidi.Input
{
public delegate void Listener(ChannelMessage message);
public class MidiListener : MonoBehaviour {
	[SerializeField]
	private string client_name;
	[SerializeField]
	private int midiPortNumber;
	
	private bool listening;

    public bool isListening
    {
        get
        {
            return listening;
        }
    }
	private int appPort = -1;

    public event Listener OnMessageRecieved;

	public void StartListening(){
		if(!SequencerManager.isRunning)
			SequencerManager.StartSequencer(AlsaIOType.Input,AlsaIOMode.NonBlocking);
		CheckForNote(client_name,midiPortNumber);
		listening = true;
	}
	public void StopListening(){
		if(!isListening)
			return;
		SequencerManager.sequencer.StopListening();
		SequencerManager.sequencer.DeleteSimplePort(appPort);
		appPort = 0;
		listening = false;
	}
	void CheckForNote(string n, int portnum){
		if(n.Replace(" ","") == "" || portnum < 0){
			return;
		}
		appPort = SequencerManager.sequencer.CreateSimplePort (string.Format("{0}-listen",n), AlsaPortCapabilities.Write | AlsaPortCapabilities.NoExport, AlsaPortType.Application | AlsaPortType.MidiGeneric);
		var lp = -1;
		var buffer = new byte[3];
		var cinfo = new AlsaClientInfo{Client = -1};
		while(SequencerManager.sequencer.QueryNextClient(cinfo))
			if(cinfo.Name.Contains(n))
				lp = cinfo.Client;
		SequencerManager.sequencer.ConnectFrom(appPort,lp,portnum);	
		SequencerManager.sequencer.StartListening(appPort,buffer,(d,s,l) => {
			ChannelMessage cm = new ChannelMessage(d,s,l);
			if(OnMessageRecieved != null)
				OnMessageRecieved(cm);
		});
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
}