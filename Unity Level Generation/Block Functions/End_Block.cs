using UnityEngine;
using System.Collections;

public class End_Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(SlowStop(other.gameObject));
        }
    }

    IEnumerator SlowStop(GameObject player)
    {
        yield return new WaitForSeconds(.1f);
        player.transform.position = this.transform.position;
        player.GetComponent<Move_Object>().StopMotion();
        FindObjectOfType<RunTime_Controls>().End_Level(true);
    }

}
