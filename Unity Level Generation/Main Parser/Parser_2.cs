using UnityEngine;
using System.Collections;
/*      Block Parser Version 1.1

        -Blake Wilson
        Unity Script for generating levels based on a legible input string
        Parser to make level generation cheaper on Hard Disk space.
*/
public class Parser_2 : MonoBehaviour {
    public GameObject[] Blocks;
    public int row = 0;
    public int column = 0;
    public int Theme;
	// Use this for initialization
	void Awake () {
        string generatelevel;
        //generatelevel = "3T|6|6|7W|2O|1S{1}|1O|2W|1C|3O|2W|4O|2W|1R{1}|1B|1O|1B|7W|";
        generatelevel = PlayerPrefs.GetString("Current_level");
        //Debug.Log(PlayerPrefs.GetInt("Current_Level_ID"));
        //generatelevel = FindObjectOfType<Generate_Level_DataHolder>().levels[PlayerPrefs.GetInt("Current_Level_ID")];
        FindObjectOfType<Generate_Level_DataHolder>().Load();                                                               //Initializes all level data
        Parse_Generate(generatelevel);                          //Calls parser generation
    }
	

    void Parse_Generate(string ParseString, int option = 0)
    {
        int sig_counter = 0;                        //Significant stage in parsing (0 = theme, 1 = row length, 2 = column length, 3 = body)
        char[] parsed = ParseString.ToCharArray();  //Full string to parse
        string multiplier = "";                      //Multiplier for amount of blocks to generate given a block in a string
        int row_bound = 0;                          //Rows in grid/map
        int column_bound = 0;                       //Columns in grid
        string theme;                               //Theme of level
        string current_parse ="";                   //Current chunk being analyzed
        char[] body_char_array;                     //Char array for body
        int channel = 0;                            //Current channel value for assignment
        int current_block = 0;                      //Current block out of row x column matrix 


        for(int i = 0; i < parsed.Length;i++)
        {
            if(parsed[i] != '|')
            {
                current_parse += parsed[i];
                //Debug.Log("Current Parse:" + current_parse);
            }
            else
            {
                switch (sig_counter)
                {
                    case 0:
                        theme = current_parse;
                        int.TryParse(theme, out Theme);
                        sig_counter++;
                        break;

                    case 1:
                        int.TryParse(current_parse, out row_bound);
                        row = row_bound;
                        sig_counter++;
                        break;

                    case 2:
                        int.TryParse(current_parse, out column_bound);
                        column = column_bound;
                        
                        sig_counter++;
                        break;

                    case 3:
                        body_char_array = current_parse.ToCharArray();
                        //Debug.Log(current_parse);
                        for (int j = 0; j < current_parse.Length; j++)
                        {

                            if (char.IsDigit(body_char_array[j]))
                            {
                                multiplier += body_char_array[j];
                            }
                            else if (body_char_array[j] == 'W')          //Wall Block
                            {
                                Generate(ref current_block, row_bound, multiplier, 0, -1);
                            }
                            else if (body_char_array[j] == 'O')          //Open Block to travel through
                            {
                                Generate(ref current_block, row_bound, multiplier, 1, -1);
                            }
                            else if (body_char_array[j] == 'C')          //Main Character block
                            {
                                Generate(ref current_block, row_bound, multiplier, 2, -1);
                            }

                            else if (body_char_array[j] == 'S')          //Sender Block
                            {
                                Debug.Log(body_char_array[j + 2]);
                                if (char.IsDigit(body_char_array[body_char_array.Length - 3]))
                                {
                                    char[] str = { 'r', 'r' };
                                    string strs;
                                    str[0] = body_char_array[j + 1];
                                    str[1] = body_char_array[body_char_array.Length - 1];
                                    strs = str.ToString();
                                    int.TryParse(strs, out channel);
                                }
                                else
                                {
                                    char[] str = { 'r' };
                                    string strs;
                                    str[0] = body_char_array[j + 2];
                                    strs = str[0].ToString();
                                    Debug.Log("strs:" + strs);
                                    int.TryParse(strs, out channel);
                                }
                                Debug.Log("sChannel:" + channel.ToString());
                                Generate(ref current_block, row_bound, multiplier, 3, channel);

                            }
                            else if (body_char_array[j] == 'R')          //Receiver Block
                            {
                                Debug.Log(body_char_array.ToString());
                                if (char.IsDigit(body_char_array[body_char_array.Length - 3]))
                                {
                                    char[] str = { 'r', 'r' };
                                    string strs;
                                    str[0] = body_char_array[j + 1];
                                    str[1] = body_char_array[body_char_array.Length - 1];
                                    strs = str.ToString();
                                    int.TryParse(strs, out channel);
                                }
                                else
                                {
                                    char[] str = { 'r' };
                                    string strs;
                                    str[0] = body_char_array[j + 2];
                                    strs = str[0].ToString();
                                    Debug.Log("strsr:" + strs);
                                    int.TryParse(strs, out channel);
                                }
                                Debug.Log("rChannel:" + channel.ToString());
                                Generate(ref current_block, row_bound, multiplier, 4, channel);
                            }
                            else if (body_char_array[j] == 'B')           //Block path block
                            {
                                Generate(ref current_block, row_bound, multiplier, 6, -1);
                            }
                            else if (body_char_array[j] == 'E')          //End Level Block 
                            {
                                Generate(ref current_block, row_bound, multiplier, 7, -1);
                            }
                            else if (body_char_array[j] == 'T')          //Star block 
                            {
                                Generate(ref current_block, row_bound, multiplier, 8, -1);
                            }
                            else if(body_char_array[j] == 'M')
                            {
                                Generate(ref current_block, row_bound, multiplier, 9, -1);
                            }

                        }
                        break;
                        
                }
                current_parse = "";
                }
            //Debug.Log(current_parse);
            multiplier = "";
        }
        Generate(ref current_block, column_bound, "1",5, -1);
    }
    void Generate(ref int current_block, int row, string multiplier, int block_index, int channel)
    {
        int upper_limit;
        int.TryParse(multiplier, out upper_limit);
        for (int g = 1; g <= upper_limit; g++)
        {
            Vector3 position = new Vector3((current_block / row) * 2, 0, -(current_block % row) * 2);
            GameObject block;
            block = Instantiate(Blocks[block_index]);
            block.transform.position = position;

                if (block_index == 3)                               //Checks for sender
                {
                    block.GetComponent<Sender>().channel = channel;
                    block.GetComponentInChildren<Renderer>().material.color = FindObjectOfType<Generate_Level_DataHolder>().Hues[channel - 1];
                    Debug.Log(channel - 1);
                }
                else if(block_index == 2)                           //Checks for main character
                {
                    block.GetComponent<Move_Object>().row_count = row;
                    block.GetComponent<Move_Object>().column_count = column;
                    GameObject block2;
                    block2 = Instantiate(Blocks[1]);
                    block2.transform.position = position;
                    block.GetComponentInChildren<Animator>().runtimeAnimatorController = FindObjectOfType<Generate_Level_DataHolder>().Animators[Theme * 7];
                }
                else if(block_index == 4)                           //Checks for receiver
                {
                    block.GetComponent<Receiver>().channel = channel;
                    block.GetComponentInChildren<Renderer>().material.color = FindObjectOfType<Generate_Level_DataHolder>().Hues[channel - 1];
                    Debug.Log(channel - 1);
                }
                else if(block_index == 5)                           //Checks for camera
                {
                    Vector3 position_cam = new Vector3(((current_block - 1) % row), 2, -((current_block / row) - 1));
                    block.transform.position = position_cam;
                    block.GetComponent<Camera>().orthographicSize = row * 2;
                }
                else if(block_index == 9)                           //Checks for Moveable Block
                {
                GameObject block2;
                block2 = Instantiate(Blocks[1]);
                block2.transform.position = position;
                }
            string decode = "\nBlock: " + current_block.ToString() + "Index:" + block_index.ToString() + " Mult: " + multiplier;
            //Debug.Log(decode);
            current_block ++;
        }

    }
}
