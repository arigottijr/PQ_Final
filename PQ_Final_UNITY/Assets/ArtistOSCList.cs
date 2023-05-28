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
    public TextMeshPro NextArtist1;
    public TextMeshPro NextArtist2;
    public TextMeshPro NextArtist3;
    public TextMeshPro NextArtist4;
    public TextMeshPro NextArtist5;
    public TextMeshPro NextArtist6;
    public TextMeshPro NextArtist7;
    public TextMeshPro NextArtist8;
    public TextMeshPro NextArtist9;
    public TextMeshPro NextArtist10;

    public TextAsset textAssetData;

    public int[] oscPlaylist = new int[11];

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
        NextArtist1.text = myOSCList.artistosc[1].artistName;
        NextArtist2.text = myOSCList.artistosc[2].artistName;
        NextArtist3.text = myOSCList.artistosc[3].artistName;
        NextArtist4.text = myOSCList.artistosc[4].artistName;
        NextArtist5.text = myOSCList.artistosc[5].artistName;
        NextArtist6.text = myOSCList.artistosc[6].artistName;
        NextArtist7.text = myOSCList.artistosc[7].artistName;
        NextArtist8.text = myOSCList.artistosc[8].artistName;
        NextArtist9.text = myOSCList.artistosc[9].artistName;
        NextArtist10.text = myOSCList.artistosc[10].artistName;

        for (int  i = 0; i < oscPlaylist.Length; i++)
        {
            oscPlaylist[i] = int.Parse(myOSCList.artistosc[i].oscNumber);
        }


    }

    public void ClickUpdatePlaylist(int artist)
    {
        NowPlaying.text = myOSCList.artistosc[artist].artistName;
        oscPlaylist[0] = int.Parse(myOSCList.artistosc[artist].oscNumber);

    }

    void OSCRequest()
    {
        
    }

}
