using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SonicController : PlayerController
{
    public ControlMapSonic mapSonic;
    public Transform camerad;
    [HideInInspector] public bool ss;
    private bool ground = false;
    private bool HasJump = false;
    private bool aDroite = false;
    private float Posty;
    private float timeOffset;
    private Vector2 zero = Vector2.zero;

    public SonicController(string Name,Animator anim, float jumpForce, float speed, float laserLength, BoxCollider2D bCol2d, Collider2D currentPlatform,
        bool IsClimbing, Mouvement movement, int Jump, Rigidbody2D rd, float jumpForce2, AudioClip jumpClip) 
        : base(Name,anim, jumpForce, speed, laserLength, bCol2d, currentPlatform, IsClimbing, movement, Jump, rd, jumpForce2, jumpClip)
    {
    }


    public void handleAccrocher()
    {
       // isAccrocher = true;
    }

    protected override void Awake()
    {
        base.Awake();
        mapSonic = new ControlMapSonic();
        if (ss)
        {
            mapSonic.SuperSonic.Enable();
        } else
        {
            mapSonic.Platform.Enable();
        }
        mapSonic.Platform.Jump.performed += JumpAction;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Posty = camerad.GetComponent<CameraController>().getPostY();
        timeOffset = camerad.GetComponent<CameraController>().getTime();
    }

    public IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        HasJump = false;
    }

    private void FixedUpdate()
    {
       Ground();
       CloudPlatformCheck();
    }

    // Update is called once per frame
    void Update()
    {
        SonicD();
        anim.SetBool("Climbing", IsClimbing);
    }
    void changerDirection()
    {
        aDroite = !aDroite;
	    Vector3 theScale = transform.localScale;
	    theScale.x *= -1;
	    transform.localScale = theScale;
    }
    void SonicD()
    {
        float x = movement.Movement.Movement.ReadValue<Vector2>().x;
        float y = movement.Movement.Movement.ReadValue<Vector2>().y;
        if (!IsClimbing)
        {
            float velocity = x * speed;
            if (!ss)
            {
                rd.AddForce(new Vector2(velocity, 0)*Time.deltaTime);
            }
            else
            {
                rd.velocity = Vector2.SmoothDamp(rd.velocity, new Vector2(velocity / 200, (y * speed) /200),ref zero,0.05f);
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(rd.velocity.x));
        if (x < 0 && !aDroite)
        {
            changerDirection();
        }
        if (x > 0 && aDroite)
        {
            changerDirection();
        }
        if (y < 0 && ground && x == 0) 
        { 
            camerad.GetComponent<CameraController>().postOffset.y = 2.5f;
            camerad.GetComponent<CameraController>().timeOffset = 2;
            anim.SetBool("LookUp", false);
            anim.SetBool("LookDown", true);
        } else if (y > 0 && ground)
        {
            camerad.GetComponent<CameraController>().postOffset.y = 5.5f;
            camerad.GetComponent<CameraController>().timeOffset = 2;
            if (!ss)
            {
                anim.SetBool("LookUp", true);
            } else
            {
                anim.SetBool("LookUp", false);
            }
            anim.SetBool("LookDown", false);
        } else
        {
            camerad.GetComponent<CameraController>().postOffset.y = Posty;
            camerad.GetComponent<CameraController>().timeOffset = timeOffset;
            anim.SetBool("LookUp", false);
            anim.SetBool("LookDown", false);
        }
    }

    void Ground()
    {
        ground = Grounded();
        anim.SetBool("Ground",ground);
        anim.SetBool("HasJump",HasJump);
        if (ground && HasJump)
        {
            StartCoroutine(Wait(2f));
        }
        if (ground)
        {
            Jump = 0;
        }
       
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
        Jump += 1;
        if (ground)
        {
            AudioManager.Instance.playAtPoint(jumpClip,transform.position);
            HasJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (Jump == 1 && ground == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce2));
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(checkSol.position,rayonSol);
    }*/
}
