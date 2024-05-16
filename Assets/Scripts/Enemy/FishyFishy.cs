using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyFishy : Enemy
{
    public Transform target;
    public GameObject objToDestroy;
    public FishyFishy(float speed, SpriteRenderer spriteRenderer, int damage) : base(speed, spriteRenderer, damage)
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(objToDestroy);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            transform.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            transform.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }
}
