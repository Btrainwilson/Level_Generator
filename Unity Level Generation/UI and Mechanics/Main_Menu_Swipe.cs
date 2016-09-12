using UnityEngine;
using System.Collections;

public class Main_Menu_Swipe : MonoBehaviour
{
    public void slide(int Direction)
    {
        if (Direction == 3)
        {
            FindObjectOfType<UI_Functions>().Move_Smaller_Canvas();
        }
    }
}
