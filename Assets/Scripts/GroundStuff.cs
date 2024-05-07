using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundStuff : MonoBehaviour
{
    public float positionx;
    public float positiony;
    public float positionz;
    void FixedUpdate()
    {
        Vector3 POsition = new Vector3(transform.position.x + positionx,transform.position.y + positiony,transform.position.z + positionz);
        //Length of the ray
        float laserLength = 50f;

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(POsition, Vector2.right, laserLength);

        //If the collider of the object hit is not NUll
        if (hit.collider != null)
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + hit.collider.tag);
        }

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(POsition, Vector2.right * laserLength, Color.red);
    }
}
