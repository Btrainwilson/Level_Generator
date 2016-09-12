using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Functions : MonoBehaviour {
    bool move_smaller_canvas = false;
    public float seconds_for_change = 4;
    float speed;
    public RectTransform canv;
    Vector2 delta_pos;
    public int threshold;
	// Use this for initialization
	void Start () {
        FindObjectOfType<Text>().text = "Screen Size:" + Screen.width.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(canv.anchoredPosition);
	if(move_smaller_canvas)
        {
            delta_pos =  new Vector2(canv.anchoredPosition.x - speed * Time.deltaTime, canv.anchoredPosition.y);
            //delta_pos = speed * Time.deltaTime + canv.GetComponent<RectTransform>().rect.x;
            canv.anchoredPosition = delta_pos;
            if (canv.anchoredPosition.x < threshold)
            {
                move_smaller_canvas = false;
                canv.anchoredPosition = new Vector2(threshold,canv.anchoredPosition.y);
            }
                

        }
	}
    public void Move_Smaller_Canvas()
    {
        threshold = (int)canv.anchoredPosition.x - 800;//Screen.width;
        //threshold = 800;
        move_smaller_canvas = true;
        speed = 400.0f / seconds_for_change;
    }
    public void Load_level(int level_id)
    {
        PlayerPrefs.SetInt("Current_Level_ID", level_id);
        //Debug.Log(level_id);
        PlayerPrefs.SetString("Current_level", FindObjectOfType<Generate_Level_DataHolder>().levels[level_id]);
        PlayerPrefs.Save();
        Application.LoadLevel("Demo_Scene");

    }
}
