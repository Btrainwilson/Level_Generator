using UnityEngine;
using System.Collections;

public class Sender : MonoBehaviour {
    public int channel;
    Receiver receive;
    Vector3 Velocity_Check;
	// Use this for initialization
	void Start () {
        Receiver[] receiverlist;
        receiverlist = FindObjectsOfType<Receiver>();
        for(int i = 0; i < receiverlist.Length; i++)
        {
            if (receiverlist[i].channel == channel)
            {
                receive = receiverlist[i];
                break;
            }
        }
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Velocity_Check = other.GetComponent<Rigidbody>().velocity;
            StartCoroutine(SlowTeleport(other.gameObject));
        }
        else if( other.gameObject.tag == "Teleportable")
        {
            Velocity_Check = other.GetComponent<Moveable_Block>().Current_Velocity;
            StartCoroutine(SlowTeleport(other.gameObject));
        }
    }
    IEnumerator SlowTeleport(GameObject player)
    {

        yield return new WaitForSeconds(.05f);
        player.transform.position = receive.transform.position;
        if(player.tag == "Teleportable")
        {
            player.GetComponent<Moveable_Block>().Current_Velocity = Velocity_Check;
        }

    }
}
