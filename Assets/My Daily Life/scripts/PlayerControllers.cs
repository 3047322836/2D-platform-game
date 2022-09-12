using System.Collections;
using System.Collections.Generic;
using NonStandard;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    //private CapsuleCollider2D myBody;
    private bool isWall;
    private bool isGround;
    private bool canDoubleJump;

    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        //myBody = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Jump();
        //Attack();
        CheckGrounded();
        SwitchAnimation();

        Lines.Make("velocity").Arrow(transform.position + Vector3.back, transform.position + (Vector3) rb.velocity, Color.cyan);
        //if(wall != Vector2.zero)
        //{
        //    dotProduct = Vector2.Dot(wall, rb.velocity);
        //    Lines.Make("normal").Arrow(transform.position, transform.position - (Vector3) wall * dotProduct, Color.red);
        //    rb.velocity += wall * dotProduct;

        //}
        //wall = Vector2.zero;
    }
    public float dotProduct;
    Vector2 wall;
    public void OnCollisionStay2D(Collision2D collision)
    {
        Vector2[] normals = System.Array.ConvertAll(collision.contacts, contact => contact.normal);
        //Debug.Log(string.Join(", ", normals));
        for (int i = 0; i < collision.contactCount; i++)
        {

            //Vector3 n = collision.contacts[i].normal;
            //Vector3 p = collision.contacts[i].point;
            //Debug.Log(dotProduct);
            wall = collision.contacts[i].normal;
        }
    }

    //void CheckWall()
    //{
    //    isWall = myBody.IsTouchingLayers(LayerMask.GetMask("Ground"));
    //}

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isGround);
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if(rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, rb.velocity.y);
        rb.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                rb.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    rb.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if(rb.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
        
        if (myAnim.GetBool("DoubleJump"))
        {
            if(rb.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }
}
