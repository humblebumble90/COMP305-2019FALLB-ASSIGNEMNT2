using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    private float myTime1;
    private float myTime2;
    public GameObject gC;
    public Rigidbody2D rB;

    private int bossHP;
    [Header("Attack Setting")]
    public GameObject fire;
    public GameObject fireSpawn1;
    public GameObject fireSpawn2;
    public float fireRate1;
    public float fireRate2;

    [Header("Speed")]
    public float speed;

    [Header("Sound")]
    public AudioSource deadSound;


    // Start is called before the first frame update
    void Start()
    {
        bossHP = 20;
        deadSound.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        myTime1 += Time.deltaTime;
        myTime2 += Time.deltaTime;
        Fire();
        Move();
    }

    void Fire()
    {
        if(myTime1 > fireRate1)
        {
                Instantiate(fire, fireSpawn1.transform.position, fireSpawn1.transform.rotation);
                myTime1 = 0.0f;    
        }

        if(myTime2>fireRate2)
        {
            Instantiate(fire, fireSpawn2.transform.position, fireSpawn2.transform.rotation);
            myTime2 = 0.0f;
        }
    }

    private void Move()
    {
            if (rB.transform.position.y <= -3)
            {
                rB.velocity = new Vector2(0.0f, speed);
            }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bossHP -= 1;
            if(bossHP <= 0)
            {
                deadSound.Play();
                Destroy(this.gameObject);
            }
        }
    }
}


