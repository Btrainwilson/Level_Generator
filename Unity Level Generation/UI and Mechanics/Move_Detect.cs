using UnityEngine;
using System.Collections;

public class Move_Detect : Input_Controller {
    float[] Touch_info; //Constant holder for touch info tracking
    GameObject pass_Object;
    public bool Main_menu = false;
    void Start()
    {
        if (!Main_menu)
        {
            pass_Object = FindObjectOfType<Move_Object>().gameObject;
            this.GetComponent<BoxCollider>().size = new Vector3(FindObjectOfType<Camera>().orthographicSize + (2 * (FindObjectOfType<Camera>().orthographicSize / 10)),0.12f, FindObjectOfType<Camera>().orthographicSize + (2 * (FindObjectOfType<Camera>().orthographicSize / 3)));
        }
            
        else
            pass_Object = FindObjectOfType<Main_Menu_Swipe>().gameObject;
    }
	public void Begin_Touch(float[] Touch_inf)
    {
        if(Touch_inf[0] == 0 || Touch_inf[0] == -1) //if the touch is the first touch or if the touch_info is not being used (denotes -1 for i) then
            Touch_info = Touch_inf; //Set touch tracking info if the first touch began
    }
    public void End_Touch(float[] Touch_inf)
    {
        if(Touch_inf[0] == Touch_info[0])
        {
            if (!Main_menu)
                pass_Object.GetComponent<Move_Object>().Move(Direction(Touch_info, Touch_inf));
            else
                pass_Object.GetComponent<Main_Menu_Swipe>().slide(Direction(Touch_info, Touch_inf));
            Touch_info[0] = -1;
            //pass_Object.GetComponent<Move_Object>().Direction = Direction(Touch_info, Touch_inf);
        }

    }
    int Direction(float[] PosI,float[] PosF) //Returns int value for direction. 1 = Right, 2 = Up, 3 = Left, 4 = Down
    {
        //pass_Object.GetComponent<TextMesh>().text = "Delta_x: " + (PosF[1] - PosI[1]).ToString() + "\nDelta_Y:  " + (PosF[3] - PosI[3]).ToString();
        //float Y_X_Ratio;
        //Y_X_Ratio = ((PosF[2] - PosI[2]) / ((PosF[1] - PosI[1]))); //Ratio between x and y
        if (Mathf.Abs(PosF[3] - PosI[3]) > Mathf.Abs((PosF[1] - PosI[1])))
        {
            if(PosF[3] - PosI[3] < 0)
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            if(PosF[1] - PosI[1] < 0)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }
        

    }
}
