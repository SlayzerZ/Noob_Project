using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    // Start is called before the first frame update
    public void Settings(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; 
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(50);
        }
    }

    public void end()
    {
        Destroy(gameObject);
    }
}
