using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAntagonist : Enemy
{
    public float speed;
    public bool MoveRight;

    private BoxCollider2D myFeet;
    private Animator myAnim;
    private bool isGround;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }


    }


    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            if (MoveRight)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                MoveRight = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                MoveRight = true;
            }
        }
    }


}
