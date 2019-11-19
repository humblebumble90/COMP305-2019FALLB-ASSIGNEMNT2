using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FireBall1")
            || other.gameObject.CompareTag("FireBall2"))
        {
            Destroy(other.gameObject);
        }
    }
}
