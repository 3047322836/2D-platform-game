using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private bool canOpen;
    private bool isOpened;
    private Animator anim;

    public static int CurrentKeyQuantity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        CurrentKeyQuantity = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentKeyQuantity != 0)
        {
            if(canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
             other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }

}
