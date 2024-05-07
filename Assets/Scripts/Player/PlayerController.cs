using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public Animator anim;
    public float jumpForce = 0;
    public float speed = 8f;
   // public float rayonSol = 0.1f;
    public Transform checkSol;
   // public LayerMask Sol;
    public float laserLength = 0.025f;
    protected BoxCollider2D bCol2d;
    protected Collider2D currentPlatform;

    public PlayerController(Animator anim, float jumpForce, float speed, Transform checkSol, float laserLength, BoxCollider2D bCol2d, Collider2D currentPlatform)
    {
        this.anim = anim;
        this.jumpForce = jumpForce;
        this.speed = speed;
        this.checkSol = checkSol;
        this.laserLength = laserLength;
        this.bCol2d = bCol2d;
        this.currentPlatform = currentPlatform;
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        bCol2d = GetComponent<BoxCollider2D>();
    }
    public float offset;
    public virtual bool Grounded()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        //Start point of the laser
        Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (boxCollider2D.bounds.extents.y + 0.05f));
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
}
