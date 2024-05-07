using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public new GameObject DestroyObject;
    public float bounce;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("lol");
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            Destroy(DestroyObject);
        }
    }
}
