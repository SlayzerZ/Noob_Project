using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float timeOffset;
    public Vector3 postOffset;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + postOffset, ref velocity, timeOffset);
        if (transform.position.y >= 6.99)
        {
            transform.position = new Vector3(player.position.x, 6.99f, transform.position.z);
        }
    }

    public float getPostY()
    {
        return postOffset.y;
    }
    public float getTime() { return timeOffset; }
}
