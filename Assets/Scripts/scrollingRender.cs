using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingRender : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer bg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bg.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
