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
    public bool ss;
    private bool ground = false;
    private bool HasJump = false;
    private bool aDroite = false;
    private bool IsGamePause = false;
    private float Posty;
    private float timeOffset;

    public SonicController(Animator anim, float jumpForce, float speed, float laserLength, BoxCollider2D bCol2d, Collider2D currentPlatform,
        bool IsClimbing, Mouvement movement, int Jump, Rigidbody2D rd, float jumpForce2) 
        : base(anim, jumpForce, speed, laserLength, bCol2d, currentPlatform, IsClimbing, movement, Jump, rd, jumpForce2)
    {
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
       // mapSonic.Platform.Pause.performed += HandlePause;
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
            rd.AddForce(new Vector2(velocity, 0)*Time.deltaTime);
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

    private void HandlePause(InputAction.CallbackContext obj)
    {
        if (IsGamePause)
        {
            IsGamePause = false;
            Time.timeScale = 1;

        }
        else
        {
            IsGamePause = true;
            Time.timeScale = 0f;
        }
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
        Jump += 1;
        if (ground)
        {
            HasJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (Jump == 1 && ground == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce2));
        }
      //  Debug.Log("HasJump : " + HasJump);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(checkSol.position,rayonSol);
    }*/
}
