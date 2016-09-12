using UnityEngine;
using System.Collections;

public class Trap_Script : MonoBehaviour {
    bool triggered = false;
    public Sprite sprit;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(triggered)
            {
                FindObjectOfType<Move_Object>().StopMotion();
                FindObjectOfType<RunTime_Controls>().End_Level(false);
            }
            else
            {
                triggered = true;
                this.GetComponent<SpriteRenderer>().sprite = sprit;
            }
        }
    }
}
