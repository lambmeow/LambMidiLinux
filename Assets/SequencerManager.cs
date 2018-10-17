using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlsaSharp;
public static class SequencerManager {
	static bool _running;
	public static AlsaSequencer sequencer; 
    public static bool isRunning
    {
        get
        {
            return _running;
        }

        set
        {
            _running = value;
        }
    }
	
	public static void StartSequencer(AlsaIOType type, AlsaIOMode mode, string drivername = "default"){
		sequencer = new AlsaSequencer(type,mode,drivername);
		isRunning = true;
	}
	public static void Stop(){
		if(!isRunning){
			return;
		}
		sequencer.Dispose();
		sequencer = null;
		isRunning = false;
	}

}
