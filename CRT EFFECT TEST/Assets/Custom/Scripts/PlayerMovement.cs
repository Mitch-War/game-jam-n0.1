using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector3 direction;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Components")]
    public Rigidbody rb;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float fallMultiplier = 5;


    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.4f;

    public bool isDead;
    private int dashAmount = 1;
    void Start()
    {

    }

    void Update()
    {
        if (!isDead)
        {
            //Shoots a raycast to see if character is touching ground.
            onGround = Physics.Raycast(transform.position, Vector3.down, groundLength, groundLayer);
            if (onGround)
                dashAmount = 1;

            if (Input.GetButtonDown("Jump"))
            {
                jumpTimer = Time.time + jumpDelay;
            }

            direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        }
        
    }

    //Executes all physics based stuff
    void FixedUpdate()
    {
        if (!isDead)
        {
            MoveCharacter(direction.x);

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {

                rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }

            if (jumpTimer > Time.time && onGround)
            {
                Jump();
            }

            ModifyPhysics();
        }
        
    }

    //Moves the character
    void MoveCharacter(float horizontal) 
    {
        rb.AddForce(Vector3.left * horizontal * moveSpeed);
    }

    //Makes the character feel less slippy by increasing drag when needed.
    void ModifyPhysics() 
    {
        if (!isDead)
        {
            bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

            if (onGround)
            {
                if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
                {
                    rb.drag = linearDrag;
                }
                else
                {
                    rb.drag = 0f;
                }

                rb.useGravity = false;
            }
            else
            {
                rb.useGravity = true;
                rb.drag = linearDrag;

                if (rb.velocity.y < 0)
                {
                    rb.AddForce(Physics.gravity * fallMultiplier);
                }
                else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
                {
                    rb.AddForce(Physics.gravity * (fallMultiplier / 2));
                }
            }


        }

    }

    void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        jumpTimer = 0;
    }

    //Just for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

    public void Dash(KeyCode key)
    {
        if (!isDead && dashAmount == 1)
        {
            if (key == KeyCode.D)
                rb.AddForce(-1000, 0, 0);

            if (key == KeyCode.A)
                rb.AddForce(1000, 0, 0);
            dashAmount--;
        }
    }
    public void OnDied()
    {
        Instantiate(Resources.Load<Transform>("BulletEffect"), transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
