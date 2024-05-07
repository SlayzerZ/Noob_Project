using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public new GameObject DestroyObject;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("lol");
        if (collision.CompareTag("Player"))
        {
            Destroy(DestroyObject);
        }
    }
}
