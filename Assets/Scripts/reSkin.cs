using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reSkin : MonoBehaviour
{
    private GameController controller;
    private SpriteRenderer sRender;
    public bool isPlayer;
    public Sprite[] spritesAttack;
    public Sprite[] spritesIdle;
    public Sprite[] spritesRun;
    public Sprite[] spritesJump;

    public string spriteSheetName;
    public string loadedSpriteSheetName;

    private Dictionary<string,Sprite> spriteSheet;

    void Start()
    {
        controller = FindObjectOfType(typeof(GameController)) as GameController;// busca o script
       if(isPlayer)spriteSheetName = controller.spriteSheetName[controller.idPersonagem];
        sRender = GetComponent<SpriteRenderer>();
        loadSpriteSheet();
    }

    void LateUpdate()
    {
        if (isPlayer) 
        {
                
            if (controller.idPersonagem != controller.idPersonagemAtual)
            {
                spriteSheetName = controller.spriteSheetName[controller.idPersonagem];
                controller.idPersonagemAtual = controller.idPersonagem;
            }
        } 

        if (loadedSpriteSheetName!=spriteSheetName)
        {

            loadSpriteSheet();
        }
        sRender.sprite = spriteSheet[sRender.sprite.name];
    }

    private void loadSpriteSheet()
    {
        spritesJump = Resources.LoadAll<Sprite>(spriteSheetName+"/jump");
        spritesAttack = Resources.LoadAll<Sprite>(spriteSheetName+"/attack1");
        spritesIdle = Resources.LoadAll<Sprite>(spriteSheetName+"/idle");
        spritesRun = Resources.LoadAll<Sprite>(spriteSheetName+"/run");
        var totalSprite = spritesJump.Concat(spritesAttack).Concat(spritesIdle).Concat(spritesRun);

        spriteSheet = totalSprite.ToDictionary(x=> x.name, x=>x);

        loadedSpriteSheetName = spriteSheetName;
    }
}
