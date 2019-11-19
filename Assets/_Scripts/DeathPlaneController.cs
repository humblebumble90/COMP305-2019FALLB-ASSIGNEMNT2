using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform activeCheckPoint;
    public GameObject Player;
    public GameObject gC;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = activeCheckPoint.position;
            gC.GetComponent<GameController>().Lives -= 1;
        }
    }
}
