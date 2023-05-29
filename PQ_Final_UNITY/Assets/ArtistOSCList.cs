using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using TMPro;
using System.Linq;

namespace extOSC.Examples{

public class ArtistOSCList : MonoBehaviour
{

    [Header("OSC Settings")]
	private OSCTransmitter Transmitter;
    private OSCReceiver Receiver;
	private GameObject OSCManager;
    private string iPadAddress;
    public string Address = "/workspace/show/cue/";

    public TextMeshPro NowPlaying;
    public TextAsset textAssetData;
    public int currentViewedArtist;
    public int nextOSCFired;

    public string tempString;
    public int tempOSC;

    public List<string> playlisttext = new List<string>();
    public List<TextMeshPro> playlisttmp = new List<TextMeshPro>();
    public List<int> playlistosc = new List<int>();

    [System.Serializable]
    public class ArtistOSC
    {
        public string artistName;
        public string oscNumber;
    }

    [System.Serializable]
    public class TotalArtistOSCList
    {
        public ArtistOSC[] artistosc;
    }

    public TotalArtistOSCList myOSCList = new TotalArtistOSCList();

    // Start is called before the first frame update       
    void Start()
    {
        OSCManager = GameObject.Find("OSC Manager");
        iPadAddress = GameObject.Find("iPadAddress").GetComponent<TextMeshProUGUI>().text;
        Debug.Log(iPadAddress);
		Transmitter = OSCManager.GetComponent<OSCTransmitter>();
        Receiver = OSCManager.GetComponent<OSCReceiver>();	
        var receiverAddress = "/iPad/" + iPadAddress;
        Receiver.Bind(receiverAddress, MessageReceived);
        Debug.Log(receiverAddress);

        AssetDatabase.Refresh();

        ReadOSCCSV();
        PlaylistInitialize();
        


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OSCRequest();
        }
     
        //an if statement checking to see if Qlab has sent an OSC
        //once OSC arrives, fire the OSC request function below

    }

    private void Send(string address, OSCValue value)
		{
			var message = new OSCMessage(address, value);
			Transmitter.Send(message);
		}

    public void SendSelection(string Selection)
		{
			var cueAddress = Address +Selection +"/start";
			Debug.Log(cueAddress);
            // it doesn't really matter what the osc message is, qlab only looks at the address info
			// and takes any message as a "GO"
			Send(cueAddress, OSCValue.String(Selection));
		}


    void ReadOSCCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 7;
        myOSCList.artistosc = new ArtistOSC[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myOSCList.artistosc[i] = new ArtistOSC();
            myOSCList.artistosc[i].artistName = data[7 * i + 1];
            myOSCList.artistosc[i].oscNumber = data[7 * i + 5];

        }
    }


    void PlaylistInitialize()
    {
        NowPlaying.text = myOSCList.artistosc[0].artistName;
        SendSelection("0"); // send the first artist in the list, who will be displayed as "now playing"
        nextOSCFired = int.Parse(myOSCList.artistosc[0].oscNumber);

        for (int i = 0; i < playlistosc.Count; i++)
        {
            playlistosc[i] = int.Parse(myOSCList.artistosc[i].oscNumber);
        }

        for (int i = 0; i < playlisttext.Count; i++)
        {
            playlisttext[i] = myOSCList.artistosc[i].artistName;
        }

        for (int i = 0; i < playlisttmp.Count; i++)
        {
            playlisttmp[i].text = playlisttext[i];
        }
    }

    public void ClickUpdatePlaylist()
    {
        playlisttext.Insert(0, myOSCList.artistosc[currentViewedArtist].artistName);
        playlisttext = playlisttext.Distinct().ToList();
  
        playlistosc.Insert(0, int.Parse(myOSCList.artistosc[currentViewedArtist].oscNumber));
        playlistosc = playlistosc.Distinct().ToList();

       // nextOSCFired = playlistosc[0];

        for  (int i = 0; i < playlisttmp.Count; i++)
        {
            playlisttmp[i].text = playlisttext[i];
        }
       
        if (playlisttext.Count > 28)
        {
            playlisttext.RemoveAt(playlisttext.Count - 1);
        }

        if (playlistosc.Count > 10)
        {
            playlistosc.RemoveAt(playlistosc.Count - 1);
        }
       


    }

    void OSCRequest()
    {
       
        nextOSCFired = playlistosc[0];
        //create OSC from nextOSC fired
        //fire OSC
        tempOSC = playlistosc[0];
        playlistosc.RemoveAt(0);
        playlistosc.Add(tempOSC);


        NowPlaying.text = playlisttext[0];
        tempString = playlisttext[0];
        playlisttext.RemoveAt(0);
        playlisttext.Add(tempString);

        for (int i = 0; i < playlisttmp.Count; i++)
        {
            playlisttmp[i].text = playlisttext[i];
        }

        

        //comments for steps
        //when OSC request is activate above
        //create an OSC string built from the nextOSCFired variable
        //also updates the nowPlaying.text with the nextOSCfired
        //and updates all lists one up.
 
        
    }

//this acts as an event listener function and triggers whenver a message is received on the
// correct port and address.
    protected void MessageReceived(OSCMessage message)
		{
			Debug.Log(message);
            Debug.Log("received");

            //here is the section to send the correct next selection and update the playlist
            SendSelection("0");
		}

}
}
