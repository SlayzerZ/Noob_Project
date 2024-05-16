using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Animator fadeSys;
    private void Awake()
    {
        fadeSys = GameObject.FindGameObjectWithTag("FadeSys").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSys.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = LevelManager.Instance.respawnPoint;
    }
}
