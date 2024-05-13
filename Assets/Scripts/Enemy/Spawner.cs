using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private IEnumerator Spawn()
    {

        while(true)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(prefab,transform.position,Quaternion.identity);

        }
    }
}
