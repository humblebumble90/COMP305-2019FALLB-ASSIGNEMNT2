using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    public GameObject gC;
    public PlayerAnimState playerAnimState;

    [Header("Object Properties")]
    public Animator playerAnimator;
    public Rigidbody2D playerRigidBody;

    [Header("Physics Related")]
    public float moveForce;
    public float jumpForce;
    public Vector2 maximumVelocity = new Vector2(20.0f, 30.0f);

    [Header("Ground Check")]
    public bool isGrounded;
    public Transform groundTarget;

    [Header("Attack Rate")]
    private float punchTime = 0.0f;
    public float punchRate;

    [Header("Sound")]
    public AudioSource punchSound;
    public AudioSource jumpSound;
    public AudioSource deadSound;



    //public AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimState = PlayerAnimState.IDLE;
        isGrounded = false;

        punchSound.volume = 0.5f;
        jumpSound.volume = 0.5f;
        deadSound.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        punchTime += Time.deltaTime;
        if (punchTime > 0.5f)
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        // Idle State
        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimState = PlayerAnimState.IDLE;
            playerAnimator.SetInteger("AnimState", (int) PlayerAnimState.IDLE);
        }


        // Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.WALK;
                playerAnimator.SetInteger("AnimState", (int) PlayerAnimState.WALK);
                playerRigidBody.AddForce(Vector2.right * moveForce);
            }
        }

        // Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-3.0f, 3.0f, 3.0f);
            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.WALK;
                playerAnimator.SetInteger("AnimState", (int) PlayerAnimState.WALK);
                playerRigidBody.AddForce(Vector2.left * moveForce);
            }
        }

        // Jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            playerAnimState = PlayerAnimState.JUMP;
            playerAnimator.SetInteger("AnimState", (int) PlayerAnimState.JUMP);
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpSound.Play();
        }

        playerRigidBody.velocity = new Vector2(
            Mathf.Clamp(playerRigidBody.velocity.x, -maximumVelocity.x, maximumVelocity.x),
            Mathf.Clamp(playerRigidBody.velocity.y, -maximumVelocity.y, maximumVelocity.y)
        );

        //Punch
        if (Input.GetButton("Fire1"))
        {

            if (punchTime > punchRate)
            {
                playerAnimState = PlayerAnimState.PUNCH;
                playerAnimator.SetInteger("AnimState", (int)(playerAnimState = PlayerAnimState.PUNCH));
                punchTime = 0f;
                this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                punchSound.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            gC.GetComponent<GameController>().Hp -= 10;
            Debug.Log("Hp decreased.");
            Debug.Log(gC.GetComponent<GameController>().Hp);
            if (gC.GetComponent<GameController>().Hp <= 0
    && gC.GetComponent<GameController>().Lives == 0)
            {
                deadSound.Play();
                Destroy(this.gameObject);
            }
        }
        if(other.gameObject.CompareTag("DeathPlane")
            && gC.GetComponent<GameController>().Lives == 0)
        {
            deadSound.Play();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.CompareTag("FireBall1"))
        {
            Destroy(trigger.gameObject);
            gC.GetComponent<GameController>().Hp -= 20;
            Debug.Log("Hp decreased.");
            Debug.Log(gC.GetComponent<GameController>().Hp);
            if(gC.GetComponent<GameController>().Hp <= 0
                && gC.GetComponent<GameController>().Lives == 0)
            {
                deadSound.Play();
                Destroy(this.gameObject);
            }

        }

        if(trigger.gameObject.CompareTag("FireBall2"))
        {
            Destroy(trigger.gameObject);
            gC.GetComponent<GameController>().Hp -= 30;
            Debug.Log("Hp decreased.");
            Debug.Log(gC.GetComponent<GameController>().Hp);
            if (gC.GetComponent<GameController>().Hp <= 0
    && gC.GetComponent<GameController>().Lives == 0)
            {
                deadSound.Play();
                Destroy(this.gameObject);
            }
        }
    }
}
