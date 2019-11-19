using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D rBody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "FireBall1")
        {
            speed = 1;
        }
        if(this.gameObject.tag == "FireBall2")
        {
            speed = 2;
        }
        rBody = GetComponent<Rigidbody2D>(); // Detecting this gameobject's rigid body to move the object with speed.
        rBody.velocity = transform.right * -speed;
    }
}
