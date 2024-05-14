using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public new GameObject DestroyObject;
    public float bounce;
    public float rmana;
    public AudioClip death;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.playAtPoint(death,transform.position);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce);
            Destroy(DestroyObject);
            SpecialAttack.Instance.RegenMana(rmana);
        }
    }
}
