using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public GameObject gC;
    public Rigidbody2D enemyRigidBody;
    [Header("Enemy momving conditions")]
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public Transform wallAhead;
    public bool lookingForward = true;
    public float movementSpeed;
    public AudioSource deadSound;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics2D.BoxCast(
           transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f,
           1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded)
        {
            if (lookingForward)
            {
                enemyRigidBody.velocity = new Vector2(-movementSpeed, 0.0f);
            }

            if (!lookingForward)
            {
                enemyRigidBody.velocity = new Vector2(movementSpeed, 0.0f);
            }

            if (!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 3.0f, 3.0f);
                lookingForward = !lookingForward;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            deadSound.Play();
            Destroy(this.gameObject);
            gC.GetComponent<GameController>().Score += 100;

        }
    }
}
