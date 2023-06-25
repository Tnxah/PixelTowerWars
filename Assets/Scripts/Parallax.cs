using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    private GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;

        startPos = transform.position.x;

        length = GetComponent<Renderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var temp = (cam.transform.position.x * (1 - parallaxEffect));
        var dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
