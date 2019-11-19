using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public GameObject gC;

    [Header("Attack Setting")]
    private float myTime;
    public GameObject fire;
    public GameObject fireSpawn;
    public float fireRate;

    [Header("Sound")]
    public AudioSource deadSound;

    
    // Start is called before the first frame update
    void Start()
    {
        deadSound.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        Fire();
    }

    void Fire()
    {
        if(myTime > fireRate)
        {
            Instantiate(fire, fireSpawn.transform.position, fireSpawn.transform.rotation);
            myTime = 0.0f;
            Debug.Log("Enemy2 fired");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deadSound.Play();
            Destroy(this.gameObject);
            gC.GetComponent<GameController>().Score += 200;
        }
    }
}
