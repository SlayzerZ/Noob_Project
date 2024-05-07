using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    private Transform target;
    private int destPoints = 0;
    public int damage = 10;
    public bool isMove = false;
    public bool isDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        if (waypoints != null)
        {
            target = waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Vector3 dir = target.position - transform.position;
         //   Debug.Log(dir.normalized);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                destPoints = (destPoints + 1) % waypoints.Length;
                target = waypoints[destPoints];
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDamage)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        DontDestroyOnLoad(collision.gameObject);
    }
}
