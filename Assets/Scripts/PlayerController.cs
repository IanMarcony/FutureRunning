using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask whatIsGround;

    
    [SerializeField]
    private int jumpForce;

    public bool grounded;
    public bool isJumping;
    private Rigidbody2D rigidbodyPlayer;
    private Animator animatorController;
    private int idAnimation;

    private GameController gameController;

    public BoxCollider2D boxRun;
    public BoxCollider2D boxJump;


    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();        
        gameController = FindObjectOfType<GameController>() as GameController;
        boxRun.enabled=true;
        boxJump.enabled=false;
    }

    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")&&grounded&&!isJumping){
            rigidbodyPlayer.AddForce(new Vector2(0, jumpForce));
            idAnimation = 1;
        }
        
        if(grounded){
            isJumping = false;
            boxRun.enabled = true;
            boxJump.enabled = false;
            idAnimation = 0;
        }else{
            idAnimation = 1;
            boxRun.enabled = false;
            boxJump.enabled = true;
        }
        animatorController.SetInteger("id_animation", idAnimation);
        animatorController.SetBool("Grounded", grounded);
    }

    public void jumpPlayer(){
        if(grounded){
            rigidbodyPlayer.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag=="barrier"){
            print("Bateu");
            gameController.gameOver();
        }

        if(other.gameObject.tag=="coletavel"){
            print("Coletou");
            gameController.colectKey();
            Destroy(other.gameObject);
        }
    }
}
