using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private float speedGame;


    private float positionX;


    // Update is called once per frame
    void Update()
    {

        positionX =  transform.position.x;

        positionX+= Time.deltaTime*speedGame;

        transform.position = new Vector2(positionX, transform.position.y); 


        if(positionX< 0){
            Destroy(transform.gameObject);
        }
        
    }
}
