using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using TMPro;
using UnityEngine.UIElements;
using System.Text;


namespace extOSC.Examples{

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
    public GameObject displayImage;

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

    // [System.Serializable]
    // public class ArtistList
    // {
    //     public Artist[] artist;
    // }

    // public ArtistList myArtistList = new ArtistList();
    public Artist[] myArtistList;

//CSV reader that handles commas, new lines and etc from 
//https://github.com/frozax/fgCSVReader
    public class fgCSVReader
{
    public delegate void ReadLineDelegate(int line_index, List<string> line);

    public static void LoadFromFile(string file_name, ReadLineDelegate line_reader)
    {
        LoadFromString(File.ReadAllText(file_name), line_reader);
    }

    public static void LoadFromString(string file_contents, ReadLineDelegate line_reader)
    {
        int file_length = file_contents.Length;

        // read char by char and when a , or \n, perform appropriate action
        int cur_file_index = 0; // index in the file
        List<string> cur_line = new List<string>(); // current line of data
        int cur_line_number = 0;
        StringBuilder cur_item = new StringBuilder("");
        bool inside_quotes = false; // managing quotes
        while (cur_file_index < file_length)
        {
            char c = file_contents[cur_file_index++];

            switch (c)
            {
            case '"':
                if (!inside_quotes)
                {
                    inside_quotes = true;
                }
                else
                {
                    if (cur_file_index == file_length)
                    {
                        // end of file
                        inside_quotes = false;
                        goto case '\n';
                    }
                    else if (file_contents[cur_file_index] == '"')
                    {
                        // double quote, save one
                        cur_item.Append("\"");
                        cur_file_index++;
                    }
                    else
                    {
                        // leaving quotes section
                        inside_quotes = false;
                    }
                }
                break;
            case '\r':
                // ignore it completely
                break;
            case ',':
                goto case '\n';
            case '\n':
                if (inside_quotes)
                {
                    // inside quotes, this characters must be included
                    cur_item.Append(c);
                }
                else
                {
                    // end of current item
                    cur_line.Add(cur_item.ToString());
                    cur_item.Length = 0;
                    if (c == '\n' || cur_file_index == file_length)
                    {
                        // also end of line, call line reader
                        line_reader(cur_line_number++, cur_line);
                        cur_line.Clear();
                    }
                }
                break;
            default:
                // other cases, add char
                cur_item.Append(c);
                break;
            }
        }
    }
}

    


    // Start is called before the first frame update
    void Start()
    {
        //AssetDatabase.Refresh();

        //Debug.Log(textAssetData.text);

        myArtistList = new Artist[28];
        fgCSVReader.LoadFromString(textAssetData.text, new fgCSVReader.ReadLineDelegate(ReadLine));
        
        //ReadCSV();

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
        displayName.text = myArtistList[artistNumber].projectName;
        displayCategoryandName.text = myArtistList[artistNumber].artistCat+"  by "+myArtistList[artistNumber].artistName;
        Debug.Log(displayCategoryandName.text);
        displayHome.text = myArtistList[artistNumber].homeArtist;
        displayDesc.text = myArtistList[artistNumber].artistDesc;
        displayCreds.text = myArtistList[artistNumber].artistCredits;
        displayImage.GetComponent<UnityEngine.UI.Image>().sprite = artistImages[artistNumber];

        oscScript.currentViewedArtist = artistNumber;

        ComingIn = true;
        
    }

    void ReadCSV()
    {
        //Atti's old code
        //string[] data = textAssetData.text.Split(new string[] { "," , "\n" }, StringSplitOptions.None);
       // int tableSize = data.Length / 7;

    //    int tableSize = 28;
    //     myArtistList.artist = new Artist[tableSize];

        //Atti's old code
        // for (int i = 0; i < tableSize; i++)
        // {
        //     myArtistList.artist[i] = new Artist();
        //     myArtistList.artist[i].projectName = data[7 * i];
        //     myArtistList.artist[i].artistName = data[7 * i + 1];
        //     myArtistList.artist[i].homeArtist = data[7 * i + 2];
        //     myArtistList.artist[i].artistDesc = data[7 * i + 3];
        //     myArtistList.artist[i].artistCredits = data[7 * i + 4];
        //     myArtistList.artist[i].artistOSC = data[7 * i + 5];
        //     myArtistList.artist[i].artistCat = data[7 * i + 6];
        //     //getting rid of extra weird character at end of data row
        //     myArtistList.artist[i].artistCat = myArtistList.artist[i].artistCat.Substring(0, (myArtistList.artist[i].artistCat).Length - 1);



        // }

        

    }

    void ReadLine(int line_index, List<string> line)
    {
        string[] data = new string[line.Count];
        //Debug.Log("\n==> Line " + line_index.ToString() + " column " + line.Count.ToString());
        for (int i = 0; i < line.Count; i++)
        {
           data[i]= line[i].ToString();
        }

        myArtistList[line_index] = new Artist();
        myArtistList[line_index].projectName = data[0];
        myArtistList[line_index].artistName = data[1];
        myArtistList[line_index].homeArtist = data[2];
        myArtistList[line_index].artistDesc = data[3];
        myArtistList[line_index].artistCredits = data[4];
        myArtistList[line_index].artistOSC = data[5];
        myArtistList[line_index].artistCat = data[6];
        //getting rid of extra weird character at end of data row
        //myArtistList.artist[i].artistCat = myArtistList.artist[i].artistCat.Substring(0, (myArtistList.artist[i].artistCat).Length - 1);

    }

}



}

