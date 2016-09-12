using UnityEngine;
using System.Collections;

public class PC_Port_Test : MonoBehaviour {
    int direct = 0;
    GameObject pass_Object;
    public bool PC_Port = false;
    // Use this for initialization
    void Start () {
        pass_Object = FindObjectOfType<Move_Object>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PC_Port)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direct = 2;
                pass_Object.GetComponent<Move_Object>().Move(direct);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direct = 4;
                pass_Object.GetComponent<Move_Object>().Move(direct);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direct = 3;
                pass_Object.GetComponent<Move_Object>().Move(direct);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direct = 1;
                pass_Object.GetComponent<Move_Object>().Move(direct);
            }
            
        }
    }
}
