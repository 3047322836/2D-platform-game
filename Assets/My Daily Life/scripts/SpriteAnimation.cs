using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public float fps = 10;
    public SpriteAnimations[] animations;
    private Sprite[] frames;
    private int currentFrame;
    private int currentAnimation = -1;
    private float timer;
    private SpriteRenderer sr;
    [System.Serializable] public class SpriteAnimations
    {
        public string name;
        public Sprite[] frames;//array of sprite fames
    }
    public int Animation
    {
        set
        {
            SetAnimation(value);
        }
        get => currentAnimation;
    }


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Animation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float frameDuration = 1 / fps;
        if (timer >= frameDuration)
        {
            timer = 0;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
            if (frames.Length > 0)
            {
                sr.sprite = frames[currentFrame];
            }
            else
            {
                Debug.LogWarning(animations[currentAnimation].name + " is missing animation frames");
            }
           
        }
    }

    public void SetAnimation(int index)
    {
        if (index == currentAnimation)
        {
            return;
        }
        currentAnimation = index;
        frames = animations[index].frames;
        currentFrame = 0;
    }
}
