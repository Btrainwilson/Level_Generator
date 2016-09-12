using UnityEngine;
using System.Collections;

public class Move_Object : MonoBehaviour {
    public int Direction = 0;
    public float speed;
    public float threshold;
    public float velocityThreshold;
    bool can_move = true;
    public TextMesh[] Texts;
    public int row_count;
    public int column_count;
    public Animator player;
    
    void Start()
    {
        player = FindObjectOfType<Animator>();
        player.SetInteger("Direction", 1);
        player.SetBool("Idle", true);

    }
    void Update()
    {
        if(this.transform.position.x > (column_count * 2) + 5 || this.transform.position.x < -5 || this.transform.position.z > 5 || this.transform.position.z < -((row_count * 2) +5))
        {
            FindObjectOfType<RunTime_Controls>().End_Level(false);
        }
        
        if (this.GetComponent<Rigidbody>().velocity.magnitude > velocityThreshold)
            can_move = false;
        else
            can_move = true;
            
        if (Direction != 0 && can_move ) {
            this.GetComponent<Rigidbody>().drag = 0;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().AddForce(rayselect(Direction).direction * speed, ForceMode.VelocityChange);
            Direction = 0;
            can_move = false;
        }
        
    }
    public void StopMotion()
    {
        //player = 
        player.SetBool("Idle",true);
        //Debug.Log("Send: IDLE TRUE");
        Direction = 0;
        Vector3 thistrans = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(thistrans.x), thistrans.y, Mathf.Round(thistrans.z));
        can_move = true;
        this.GetComponent<Rigidbody>().drag = 15;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void Move(int direction)
    {
        //player = FindObjectOfType<Animator>();
        
        

        if (can_move) {
            player.SetInteger("Direction", direction);
            Debug.Log("Send: Direction: " + player.GetInteger("Direction"));
            player.SetBool("Idle", false);
            Debug.Log("Send: IDLE FALSE");
            FindObjectOfType<TextMesh>().text = "Direction: " + direction.ToString();
            
            
            
            Direction = direction;
            
            RaycastHit Rayhit;
            Ray ray = rayselect(direction);
            Physics.Raycast(ray, out Rayhit, 100);
            
    }

    }
    Ray rayselect(int Direction)
    {
        Vector3 pos = this.transform.position;

        switch (Direction)   //Directions are 1 = R, 2 = U, 3 = L, 4 = D
        {
            
            case 1:
                return new Ray(pos, new Vector3(1, 0, 0));
            case 2:
                return new Ray(pos, new Vector3(0, 0, 1));
            case 3:
                return new Ray(pos, new Vector3(-1, 0, 0));
            case 4:
                return new Ray(pos, new Vector3(0, 0, -1));
            default:
                return new Ray(pos, new Vector3(0, 0, -1));

        }
    }
    Vector3 CalcVector(Vector3 StopLoc, RaycastHit RayInfo)
    {
        
        //Reference object types and tags later to determine whether the player should stop inside an object or on the boundary
        if (Direction == 1)
        {
            return new Vector3(RayInfo.point.x - this.GetComponent<Renderer>().bounds.size.x, RayInfo.point.y, RayInfo.point.z);
            
        }
        else if (Direction == 2)
        {
            return new Vector3(RayInfo.point.x , RayInfo.point.y , RayInfo.point.z - this.GetComponent<Renderer>().bounds.size.z);

        }
        else if (Direction == 3)
        {
            return new Vector3(RayInfo.point.x + this.GetComponent<Renderer>().bounds.size.x, RayInfo.point.y, RayInfo.point.z);

        }
        else if (Direction == 4)
        {
            return new Vector3(RayInfo.point.x, RayInfo.point.y , RayInfo.point.z + this.GetComponent<Renderer>().bounds.size.z);

        }
        else
        {
            return RayInfo.point;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Teleportable")
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce(this.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
            Vector3 vel_check = this.GetComponent<Rigidbody>().velocity;
            Vector3 vel_pass = new Vector3(Mathf.Round(vel_check.x), Mathf.Round(vel_check.y), Mathf.Round(vel_check.z));
            other.gameObject.GetComponent<Moveable_Block>().Current_Velocity = vel_pass * speed;
            //Debug.Log("Report");
            //Debug.Log(this.GetComponent<Rigidbody>().velocity);
            //Debug.Log(other.rigidbody.velocity);
            //Debug.Log(other.gameObject);
        }
        StopMotion();
    }
    
}
