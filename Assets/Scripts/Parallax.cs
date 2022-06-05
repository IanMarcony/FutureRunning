using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length;
    private float startPos;

    private Transform cam;

    [SerializeField]
    private float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos=  transform.position.x;
        length =  GetComponent<SpriteRenderer>().bounds.size.x;
        cam  = Camera.main.transform;       
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.position.x * parallaxEffect;        

        transform.position =  new Vector3(distance+startPos, transform.position.y, transform.position.z);
    }
}

