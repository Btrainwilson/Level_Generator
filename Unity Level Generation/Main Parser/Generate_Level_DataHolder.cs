using UnityEngine;
using System.Collections;

public class Generate_Level_DataHolder : MonoBehaviour {

    public GameObject[] objects;
    public string[] levels;
    public Material[] ThemeMaterials;
    public Color32[] Hues;
    public Sprite[] Themes;
    public RuntimeAnimatorController[] Animators;   
    /*
        Theme Array Offset
        Example Theme:0
        Theme * 7 = Character Animator      Animator: 0
        Theme * 7 + 1 = Sender Animator     Animator: 1
        Theme * 7 + 2 = Receiver Animator   Animator: 2
        Theme * 7 + 3 = 2-Way Animator      Animator: 3
        Theme * 7 + 4 = Movable Block       Animator: 4 
        Theme * 7 + 5 = Trap Block          Animator: 5           
        Theme * 7 + 6 = Switch Block        Animator: 6
    */
    void Awake()
    {
        Load();
    }
    public void Load()
    {
        //Animators are 
        Hues = new Color32[9];
        Hues[0] = new Color32(204, 204, 204, 255);
        Hues[1] = new Color32(99, 182, 201, 255);
        Hues[2] = new Color32(126, 197, 126, 255);
        levels = new string[100];
        //Easy Levels
        levels[0] = "0|6|6|3W|1E|3W|1C|3O|2W|3O|1B|2W|1O|1B|2O|2W|4O|7W|";
        levels[1] = "0|6|6|1W|1C|3O|1W|2O|1B|20O|1W|1O|1W|2O|1E|1O|";
        levels[2] = "0|7|10|7W|2O|1B|3O|2W|3O|1B|1O|1W|3O|1B|2O|2W|3O|1B|1O|2W|1B|4O|1W|7O|1W|4O|1B|2W|1C|1O|1B|2O|1E|7W|";
        levels[3] = "0|9|11|3W|6O|1W|3O|1B|4O|1W|5O|1B|2O|1E|8O|1W|8O|1C|1O|1B|6O|1W|8O|1W|8O|1W|6O|1B|1O|1W|4O|1B|3O|3W|1B|5O|";
        levels[4] = "0|11|11|12W|1O|1B|7O|2W|7O|1B|1O|2W|2O|1B|6O|2W|1B|7O|1B|1W|1E|9O|2W|1B|8O|2W|3O|1B|5O|2W|6O|1B|3O|1W|9O|1C|11W|";
        levels[5] = "0|10|10|2O|6W|1O|1W|4O|1B|4O|2W|3O|1C|1O|1B|2O|2W|1B|7O|2W|8O|2W|3O|1B|4O|2W|6O|1B|1O|1W|1O|1B|7O|1W|1O|1E|1O|1B|3O|1B|1O|1W|1O|7W|1O|1W|";
        levels[6] = "0|10|11|1O|1B|1E|10O|1B|15O|1B|14O|1B|12O|1B|7O|1B|6O|1B|13O|1B|3O|1B|13O|1C|3O|1B|1O|";
        levels[7] = "0|13|13|14W|3O|1B|7O|2W|6O|2B|3O|2W|1O|1B|8O|1B|2W|1B|8O|1B|1O|2W|1E|4O|1B|5O|2W|1B|1O|1B|8O|2W|11O|2W|1O|1B|9O|2W|8O|1B|2O|2W|2O|1B|1O|1B|5O|1C|2W|10O|1B|14W|";
        levels[8] = "";
        levels[9] = "0|6|13|1W|2O|1C|1O|2W|4O|2W|3O|1B|2W|1B|3O|2W|4O|2W|3O|1B|2W|4O|2W|4O|2W|4O|2W|4O|2W|2O|1B|1O|2W|4O|2W|1O|1E|2O|1W|"; //Pokemon Level

        //Intermediate *No Special* Levels
        levels[10] = "";
        levels[11] = "0|11|12|12W|1O|1B|7O|2W|6O|1B|2O|2W|4O|1B|4O|2W|1B|1O|1B|2O|1B|3O|2W|7O|2B|2W|1O|1B|8O|1W|9O|1E|1W|3O|1B|3O|2B|2W|6O|1B|2O|1C|1W|4O|1B|4O|12W|";
        levels[12] = "0|10|13|2O|1W|1O|6W|4O|1B|1E|1B|2O|1W|3O|1B|5O|2W|1B|6O|1B|2W|3O|1B|4O|2W|7O|1B|2W|3O|1B|4O|2W|6O|1B|1O|2W|1O|1B|6O|2W|8O|1W|4O|1B|4O|1W|1C|1O|1B|4O|1B|1O|6W|2O|3W|";
        levels[13] = "";
        levels[14] = "";
        levels[15] = "";
        levels[16] = "0|15|15|10W|1C|5W|7O|1B|2O|1B|2O|2W|4O|1B|5O|1B|2O|2W|1B|12O|2W|6O|1B|7O|1W|9O|1B|3O|2W|2O|1B|8O|1B|1O|2W|7O|1B|5O|2W|3O|1B|9O|2W|1O|1B|11O|2W|13O|2W|9O|1E|3O|2W|2O|1B|10O|2W|1B|5O|1B|6O|16W|";
        levels[17] = "";
        levels[18] = "";
        levels[19] = "0|13|13|14W|3O|1B|6O|2B|1W|2O|1B|8O|1B|1W|6O|1B|4O|1B|1W|1O|1B|7O|1B|1O|1B|1W|1B|4O|1B|5O|1B|1W|8O|1B|2O|1B|1W|3O|1B|7O|1B|1W|1O|1B|7O|1B|1O|1B|1W|11O|1B|1W|11O|1B|1W|4O|1B|2O|1B|3O|1C|6B|2E|4B|1O|";  //Pokemon Level Baby!!

        //Easy Teleporter Levels
        levels[20] = "0|6|6|7W|2B|1R{1}|1O|2W|1C|3O|2W|4O|2W|1S{1}|1B|1E|1B|7W|";
        levels[21] = "0|10|10|2W|2E|2W|1O|4W|3O|1B|1O|1R{1}|2O|1W|4O|1B|3O|1C|2W|2O|1S{1}|6O|1W|1O|1W|6O|2W|8O|2W|1O|1B|6O|2W|7O|1B|2W|2O|1B|5O|4W|2O|5W|";
        levels[22] = "0|6|10|1B|1O|5B|4O|1S{1}|1B|1R{2}|2O|1C|1B|5O|2B|4O|1B|6O|2B|3O|1B|1R{1}|1E|2O|1S{2}|2B|4O|7B|";
        levels[23] = "0|6|10|3W|1S{3}|2W|1C|4O|1W|2O|1B|2O|2W|4O|2W|1S{2}|1R{1}|2O|2W|3O|1B|2W|4O|1W|1R{2}|1B|3O|1W|1S{1}|4O|3W|1E|1R{3}|2W|";
        levels[24] = "0|10|12|11W|3O|1S{2}|4O|2W|6O|1B|1O|1C|1W|1O|1B|6O|2W|3O|1R{1}|4O|2W|4O|1B|3O|2W|7O|1B|2W|3O|1B|4O|2W|6O|1B|1O|2W|6O|1E|1O|2W|2O|1B|1R{2}|1S{1}|3O|11W|";
        levels[25] = "";
        levels[26] = "";
        levels[27] = "0|10|10|1E|2O|1R{1}|2W|3O|2W|7O|1B|1W|1R{2}|1O|1S{2}|6O|1W|4O|1S{1}|4O|1W|1O|1B|4O|1B|1O|1B|1W|3O|1B|5O|2W|5O|1B|2O|2W|8O|2W|3O|1S{1}|4O|2W|1C|7O|1W|";
        levels[28] = "0|13|13|13W|1S{1}|1O|1B|7O|1B|1O|1W|1B|6O|1B|4O|1W|3O|1B|8O|1W|1R{1}|1B|10O|1W|9O|1B|2O|1W|4O|2B|1O|1C|3O|1B|1W|3O|1B|8O|1W|5O|1E|6O|1W|5O|1B|1S{1}|5O|1W|2O|1B|9O|1W|1B|7O|1B|3O|1W|8O|5W|";
        levels[29] = "";

        //Easy Trap Levels
        levels[30] = "";
        levels[31] = "";
        levels[32] = "";
        levels[33] = "";
        levels[34] = "";
        levels[35] = "";
        levels[36] = "";
        levels[37] = "";
        levels[38] = "";
        levels[39] = "";

        //Intermediate Special Levels
        levels[40] = "";
        levels[41] = "";
        levels[42] = "";
        levels[43] = "";
        levels[44] = "";
        levels[45] = "";
        levels[46] = "";
        levels[47] = "";
        levels[48] = "";
        levels[49] = "";

        //Easy Moveable Block Levels
        levels[50] = "3T|6|6|7W|1C|4O|1W|2O|1R{1}|2O|1W|1M|3O|1E|1W|1S{1}|4O|6W|";
        levels[51] = "0|12|12|13W|5O|1B|4O|2W|4O|1C|5O|2W|10O|2W|1O|1B|6O|1B|1O|2W|4O|1M|1E|4O|2W|10O|2W|10O|2W|4O|1T|5O|2W|10O|2W|10O|13W|";

    }


}
