using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class newCSVReader : MonoBehaviour
{
    
    public TextAsset CSVData;
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
        Debug.Log(file_contents.Length);
        Debug.Log(file_contents);
        

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
         fgCSVReader.LoadFromString(CSVData.text, new fgCSVReader.ReadLineDelegate(ReadLineTest));
         //Debug.Log(fgCSVReader.file_length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void ReadLineTest(int line_index, List<string> line)
    {
        //Console.Out.WriteLine("\n==> Line {0}, {1} column(s)", line_index, line.Count);
        Debug.Log("\n==> Line " + line_index.ToString() + " column " + line.Count.ToString());
        for (int i = 0; i < line.Count; i++)
        {
            //Console.Out.WriteLine("Cell {0}: *{1}*", i, line[i]);
            Debug.Log("Cell  " + i.ToString() + ": " + line[i].ToString());
        }
    }
}
