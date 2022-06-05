using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[]  platforms;

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
                
            if(numRandom<50.0f){
                Instantiate(platforms[0]);
            }else{
                Instantiate(platforms[1]);
            }
        }
               
        
    }
}
