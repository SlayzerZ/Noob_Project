using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmyController : PlayerController
{
    public ControlMapAmy mapAmy;
    public CameraController camerad;
    private bool ground = false;
    private bool HasJump = false;
    private bool aDroite = false;
    private float Posty;
    private float timeOffset;
    private float speed2;
    private Vector2 zero = Vector2.zero;
    public AmyController(string Name, Animator anim, float jumpForce, float speed, float laserLength, BoxCollider2D bCol2d, Collider2D currentPlatform, bool isClimbing, Mouvement movement, int jump, Rigidbody2D rd, float jumpForce2, AudioClip jumpClip) : base(Name, anim, jumpForce, speed, laserLength, bCol2d, currentPlatform, isClimbing, movement, jump, rd, jumpForce2, jumpClip)
    {
    }

    protected override void Awake()
    {
        base.Awake();
        mapAmy = new ControlMapAmy();
        mapAmy.Platform.Enable();
        mapAmy.Platform.Jump.performed += JumpAction;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Posty = camerad.getPostY();
        timeOffset = camerad.getTime();
        speed2 = speed;
    }

    private void FixedUpdate()
    {
        AmyD();
        Ground();
        CloudPlatformCheck();
    }

    void Update()
    {
        
        anim.SetBool("Climbing", IsClimbing);
    }

    private void JumpAction(InputAction.CallbackContext obj)
    {
        Jump += 1;
        if (ground)
        {
            AudioManager.Instance.playAtPoint(jumpClip, transform.position);
            HasJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (Jump == 1 && ground == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce2));
        }
    }

    void Ground()
    {
        ground = Grounded();
        anim.SetBool("Ground", ground);
        anim.SetBool("HasJump", HasJump);
        if (ground && HasJump)
        {
           StartCoroutine(Wait(2f));
        }
        if (ground)
        {
            Jump = 0;
        }

    }

    void changerDirection()
    {
        aDroite = !aDroite;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void AmyD()
    {
        float x = movement.Movement.Movement.ReadValue<Vector2>().x;
        float y = movement.Movement.Movement.ReadValue<Vector2>().y;
        if (!IsClimbing || y == 0)
        {
            float velocity = x * speed;
            rd.AddForce(new Vector2(velocity, 0) * Time.deltaTime);
        }
       // Debug.Log(rd.velocity.x);
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
           // speed = 0;
            camerad.postOffset.y = 2.5f;
            camerad.timeOffset = 2;
            anim.SetBool("LookUp", false);
            anim.SetBool("LookDown", true);
        }
        else if (y > 0 && ground && x == 0)
        {
           // speed = 0;
            camerad.postOffset.y = 5.5f;
            camerad.timeOffset = 2;
            anim.SetBool("LookUp", true);
            anim.SetBool("LookDown", false);
        }
        else
        {
          //  speed = speed2;
            camerad.postOffset.y = Posty;
            camerad.timeOffset = timeOffset;
            anim.SetBool("LookUp", false);
            anim.SetBool("LookDown", false);
        }
    }
    public IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        HasJump = false;
    }
}
