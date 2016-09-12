using UnityEngine;
using System.Collections;

public class RunTime_Controls : MonoBehaviour {
    public GameObject canv;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
	}
    /*
    Used for ending the level and incrementing to the next level
    */
    public void End_Level(bool success)
    {
        int max_level = PlayerPrefs.GetInt("Max_Level");
        int level_increment = 0;
        if (success)
        {
            if(PlayerPrefs.GetInt("Current_Level_ID") == max_level)
            {
                PlayerPrefs.SetInt("Max_Level", max_level + 1);
            }
            level_increment++;
            Restart_Level(level_increment);
        }
        else
        {
            canv.SetActive(true);
        }
        
    }
    public void Restart_Level(int level_increment)
    {
        PlayerPrefs.SetInt("Current_Level_ID", PlayerPrefs.GetInt("Current_Level_ID") + level_increment);
        PlayerPrefs.SetString("Current_level", GetComponent<Generate_Level_DataHolder>().levels[PlayerPrefs.GetInt("Current_Level_ID")]);
        Application.LoadLevel("Demo_Scene");
    }
}
