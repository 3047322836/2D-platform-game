using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Vector3 updir = Vector3.up;
    public Vector3 rightdir = Vector3.right;
    private SpriteAnimation sa;

    private Rigidbody2D rb;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moving = true;
            transform.position += updir * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moving = true;
            transform.position -= updir * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moving = true;
            transform.position += rightdir * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moving = true;
            transform.position -= rightdir * Time.deltaTime;
        }

        if (moving)
        {
            sa.Animation = 1;
        }
        else
        {
            sa.Animation = 0;
        }

    }

}





////int jumpCount; //num jumped
//bool jumpPress = false; //pressed key?
//bool moving = false;

//   
//    
//    if (Input.GetButtonDown("Jump"))
//    {
//        Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
//        rb.velocity = Vector2.up * jumpVe;
//        jumpPress = true;
//    }


