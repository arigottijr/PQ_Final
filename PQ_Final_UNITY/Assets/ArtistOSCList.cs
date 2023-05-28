using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using TMPro;

public class ArtistOSCList : MonoBehaviour
{
    public TextMeshPro NowPlaying;

    public TextAsset textAssetData;

    public int currentViewedArtist;

    public int nextOSCFired;

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
        AssetDatabase.Refresh();

        ReadOSCCSV();
        PlaylistInitialize();



    }

    private void Update()
    {
        //comments for stuff to do

        //an if statement checking to see if Qlab has sent an OSC
        //once OSC arrives, fire the OSC request function below
        
        
    }

    void ReadOSCCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 6;
        myOSCList.artistosc = new ArtistOSC[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myOSCList.artistosc[i] = new ArtistOSC();
            myOSCList.artistosc[i].artistName = data[6 * i];
            myOSCList.artistosc[i].oscNumber = data[6 * i + 5];

        }
    }

    void PlaylistInitialize()
    {
        NowPlaying.text = myOSCList.artistosc[0].artistName;
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
  
        playlistosc.Insert(0, int.Parse(myOSCList.artistosc[currentViewedArtist].oscNumber));
        nextOSCFired = playlistosc[0];

        for  (int i = 0; i < playlisttmp.Count; i++)
        {
            playlisttmp[i].text = playlisttext[i];
        }
       
        playlisttext.RemoveAt(playlisttext.Count - 1);
        playlistosc.RemoveAt(playlistosc.Count - 1);
        



        





    }

    void OSCRequest()
    {
        //comments for steps
        //when OSC request is activate above
        //create an OSC string built from the nextOSCFired variable
        //also updates the nowPlaying.text with the nextOSCfired
        //and updates all lists one up.
 
        
    }

}
