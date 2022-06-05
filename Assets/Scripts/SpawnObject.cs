using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    [SerializeField]
    private GameObject  objectSpwan;

    [SerializeField]
    private float rateSpwan;

    private float currentTime;


    void Start() {
        currentTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        currentTime+= Time.deltaTime;

        if(currentTime>=rateSpwan){
            currentTime =  0;
            int numRandom = new System.Random().Next(1, 100);   
                
            if(numRandom<25.0f){
                Instantiate(objectSpwan);
            }
        }
               
        
    }
}
