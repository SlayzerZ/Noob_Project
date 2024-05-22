using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public string Name;
    public Animator anim;
    public float jumpForce = 0;
    public float jumpForce2 = 0;
    public float speed = 8f;
    public float laserLength = 0.025f;
    [HideInInspector] public BoxCollider2D bCol2d;
    protected Collider2D currentPlatform;
    [HideInInspector] public bool IsClimbing = false;
    public Mouvement movement;
    [HideInInspector] public int Jump = 0;
    [HideInInspector] public Rigidbody2D rd;
    public AudioClip jumpClip;

    protected PlayerController(string Name,Animator anim, float jumpForce, float speed, float laserLength, BoxCollider2D bCol2d, Collider2D currentPlatform, bool isClimbing, Mouvement movement, int jump, Rigidbody2D rd, float jumpForce2, AudioClip jumpClip)
    {
        this.Name = Name;
        this.anim = anim;
        this.jumpForce = jumpForce;
        this.speed = speed;
        this.laserLength = laserLength;
        this.bCol2d = bCol2d;
        this.currentPlatform = currentPlatform;
        IsClimbing = isClimbing;
        this.movement = movement;
        Jump = jump;
        this.rd = rd;
        this.jumpForce2 = jumpForce2;
        this.jumpClip = jumpClip;
    }

    public static PlayerController Instance;

    protected virtual void Awake()
    {
        movement = new Mouvement();
        movement.Movement.Enable();
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de PlayerController.");
            return;
        }
        Instance = this;
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        bCol2d = GetComponent<BoxCollider2D>();
        rd = GetComponent<Rigidbody2D>();
        if (!File.Exists(SaveData.Instance.savePath()))
        {
            SaveData.Instance.CreateDefaultSaveFile(Name);
        } else
        {
            SaveData.Instance.loadData();
        }
    }
    //public float offset;
    public virtual bool Grounded()
    {
        //Start point of the laser
        Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
        //Hit only the objects of Floor layer
        int layerMask = LayerMask.GetMask("Ground","Platform");
        //Check if the laser hit something
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask);
        //The color of the ray for debug purpose
        Color rayColor = Color.red;
        //If the object is not null
        if (hit.collider != null)
        {
            //Change the color of the ray for debug purpose
            rayColor = Color.green;
          //  transform.position = new Vector2(transform.position.x, hit.point.y+ offset);
        }
        else
        {
            //Change the color of the ray for debug purpose
            rayColor = Color.red;
        
        }
        //Draw the ray for debug purpose
        Debug.DrawRay(startPosition, Vector2.down * laserLength, rayColor);
        //If the ray hits the floor return true, false otherwise
        return hit.collider != null;
    }

    public bool CloudPlatformCheck()
    {
        //Laser length
        float laserLength = 1.0f;
        //Left ray start X
        float left = transform.position.x - (bCol2d.size.x * transform.localScale.x / 2.0f) + (bCol2d.offset.x * transform.localScale.x) + 0.1f;
        //Right ray start X
        float right = transform.position.x + (bCol2d.size.x * transform.localScale.x / 2.0f) + (bCol2d.offset.x * transform.localScale.x) - 0.1f;
        //Hit only the objects of Platform layer
        int layerMask = LayerMask.GetMask("Platform");
        //Left ray start point
        Vector2 startPositionLeft = new Vector2(left, transform.position.y - (bCol2d.bounds.extents.y + 0.05f));
        //Check if the left laser hit something
        RaycastHit2D hitLeft = Physics2D.Raycast(startPositionLeft, Vector2.down, laserLength, layerMask);
        //Right ray start point
        Vector2 startPositionRight = new Vector2(right, transform.position.y - (bCol2d.bounds.extents.y + 0.05f));
        //Check if the right laser hit something
        RaycastHit2D hitRight = Physics2D.Raycast(startPositionRight, Vector2.down, laserLength, layerMask);
        //The color of the ray for debug purpose
        Color rayColor = Color.red;

        Collider2D col2DHit = null;
        //If one of the lasers hit a cloud platform
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            //Get the object hit collider
            col2DHit = hitLeft.collider != null ? hitLeft.collider : hitRight.collider;
            //Change the color of the ray for debug purpose
            rayColor = Color.green;
            //If the cloud platform collider is trigger
            if (col2DHit.isTrigger)
            {
                //Store the platform to reset later
                currentPlatform = col2DHit;
                //Disable trigger behaviour of collider
                currentPlatform.isTrigger = false;
            }
        }
        else
        {
            //Change the color of the ray for debug purpose
            rayColor = Color.red;
            //If we stored previously a platform
            if (currentPlatform != null)
            {
                //Reset the platform properties
                currentPlatform.isTrigger = true;
                currentPlatform = null;
            }
        }

        //Draw the ray for debug purpose
        Debug.DrawRay(startPositionLeft, Vector2.down * laserLength, rayColor);
        Debug.DrawRay(startPositionRight, Vector2.down * laserLength, rayColor);
        //If the ray hits a platform returns true, false otherwise
        return col2DHit != null;
    }
    public float getX()
    {
        float x = movement.Movement.Movement.ReadValue<Vector2>().x;
        return x;
    }

    public float getY()
    {
        float y = movement.Movement.Movement.ReadValue<Vector2>().y;
        return y;
    }
    public void setVy(float x,float y)
    {
       rd.velocity = new Vector2(x,y);
       //rd.isKinematic = true;
    }

    public void DoubleSpeed(float seconds)
    {
        StopCoroutine(StopDoubleSpeed(seconds));
        speed *= 2;
        StartCoroutine(StopDoubleSpeed(seconds));
    }

    protected IEnumerator StopDoubleSpeed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        speed /= 2;
    }
}
