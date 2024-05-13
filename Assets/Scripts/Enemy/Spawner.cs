using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float seconds;
    private bool swpan = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if (!swpan)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(prefab);
            swpan = true;
           // yield return new WaitForSeconds(seconds);
        }
    }
}
