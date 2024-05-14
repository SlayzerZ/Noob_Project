using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon : Enemy
{
    public AudioClip screaming;
    public Mon(float speed, SpriteRenderer spriteRenderer, int damage) : base(speed, spriteRenderer, damage)
    {
    }
    private void Start()
    {
        StartCoroutine(Scream());
    }

    private IEnumerator Scream()
    {
        yield return new WaitForSeconds(screaming.length);
       // AudioManager.Instance.playAtPoint(screaming,transform.position);
    }
}
