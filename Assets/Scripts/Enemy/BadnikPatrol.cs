using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadnikPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer spriteRenderer;
    private Transform target;
    private int destPoints = 0;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime,Space.World);
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoints = (destPoints + 1) % waypoints.Length;
            target = waypoints[destPoints];
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
