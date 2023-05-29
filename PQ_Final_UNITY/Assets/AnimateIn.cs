using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using TMPro;

public class AnimateIn : MonoBehaviour
{
    public float speed;
    public Vector2 p;
    public bool ComingIn = false;

    public TextAsset textAssetData;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI displayCategoryandName;
    public TextMeshProUGUI displayHome;
    public TextMeshProUGUI displayDesc;
    public TextMeshProUGUI displayCreds;

    public GameObject closeMenu;
    public GameObject playthisNext;

    public ArtistOSCList oscScript;

    public Sprite[] artistImages;

    [System.Serializable]
    public class Artist
    {
        public string projectName;
        public string artistName;
        public string homeArtist;
        public string artistDesc;
        public string artistCredits;
        public string artistOSC;
        public string artistCat;
    }

    [System.Serializable]
    public class ArtistList
    {
        public Artist[] artist;
    }

    public ArtistList myArtistList = new ArtistList();

    


    // Start is called before the first frame update
    void Start()
    {
        AssetDatabase.Refresh();

        ReadCSV();

        //displayName.text = myArtistList.artist[selectedArtist].projectName;
        //displayCategory.text = myArtistList.artist[selectedArtist].categoryName;
        //displayHome.text = myArtistList.artist[selectedArtist].homeArtist;
        //displayDesc.text = myArtistList.artist[selectedArtist].artistDesc;





    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ComingIn)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            p = transform.position;
            if (p.y > 0)
            {
                ComingIn = false;
                closeMenu.SetActive(true);
                playthisNext.SetActive(true);


            }
        }
    

    }

    public void ComeIn(int artistNumber)
    {
        displayName.text = myArtistList.artist[artistNumber].projectName;
        displayCategoryandName.text = myArtistList.artist[artistNumber].artistCat+" by "+myArtistList.artist[artistNumber].artistName;
        Debug.Log(displayCategoryandName.text);
        displayHome.text = myArtistList.artist[artistNumber].homeArtist;
        displayDesc.text = myArtistList.artist[artistNumber].artistDesc;
        displayCreds.text = myArtistList.artist[artistNumber].artistCredits;

        oscScript.currentViewedArtist = artistNumber;

        ComingIn = true;
        
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 7;
        myArtistList.artist = new Artist[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myArtistList.artist[i] = new Artist();
            myArtistList.artist[i].projectName = data[7 * i];
            myArtistList.artist[i].artistName = data[7 * i + 1];
            myArtistList.artist[i].homeArtist = data[7 * i + 2];
            myArtistList.artist[i].artistDesc = data[7 * i + 3];
            myArtistList.artist[i].artistCredits = data[7 * i + 4];
            myArtistList.artist[i].artistOSC = data[7 * i + 5];
            myArtistList.artist[i].artistCat = data[7 * i + 6];



        }

    }

}
