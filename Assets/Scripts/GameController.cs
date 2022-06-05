using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Advertisements;
using TMPro;



public enum GameState {
	PAUSE,
	GAMEPLAY,
	GAMEOVER
}


public class GameController : MonoBehaviour
{

    [Header ("Settings general")]

    public float speedGame;

	public GameState currentState;



    [Header ("Movement Background")]

    private float curXBG;
    public MeshRenderer meshRendererBG;

    [Header ("Movement Floor")]

    private float curXFloor;

    public float speedFloor=1;
    public TilemapRenderer tilemapRendererFloor;

    public TMP_Text numberTxt;
    public TMP_Text numberCardTxt;
    private int numberScore;
    private int numberCards;
    private float currentTime;
    public float rateTime;

	public string[] spriteSheetName;
	public int idPersonagem;
	public int idPersonagemAtual;

    public GameObject panelOptions;

    public AudioClip[] soundsBackground;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Banner.Hide();
        currentTime = 0;
        numberScore = 0;
        idPersonagem =  PlayerPrefs.GetInt("idPersonagem");
        numberCards = PlayerPrefs.GetInt("numberCards");
        numberCardTxt.text = numberCards.ToString();
        numberTxt.text = numberScore.ToString();
        panelOptions.SetActive(false);

        int numRandom = new System.Random().Next(0, soundsBackground.Length-1);   


        audioSource.clip = soundsBackground[numRandom];
        audioSource.Play();

    }

     void FixedUpdate()
    {

        //Movement Background
        curXBG+= Time.deltaTime*speedGame;

        meshRendererBG.material.SetTextureOffset("_MainTex", new Vector2(curXBG,0)) ;

        
        //Movement Floor
        curXFloor+= Time.deltaTime*speedGame*speedFloor;

        tilemapRendererFloor.material.SetTextureOffset("_MainTex", new Vector2(curXFloor,0)) ;

        currentTime+=Time.deltaTime;

        if(currentTime>=rateTime){
            currentTime = 0;
            numberScore+=1;
            numberTxt.text=numberScore.ToString();
        }
       
    }

    // Update is called once per frame
    void Update()
    {


        
    }


    public void changeState (GameState newState) {
		currentState = newState;
        switch (currentState)
        {
			case GameState.GAMEPLAY: Time.timeScale = 1;
				break;
			case GameState.PAUSE:
				Time.timeScale = 0;
				break;
			case GameState.GAMEOVER:
				Time.timeScale = 0;
				break;
			default: Time.timeScale = 0;
				break;
		}
	}

    public void gameOver(){
        changeState(GameState.GAMEOVER);
        PlayerPrefs.SetInt("score", numberScore);
        PlayerPrefs.SetInt("numberCards", numberCards);
        if( numberScore> PlayerPrefs.GetInt("score_record")){
            PlayerPrefs.SetInt("score_record", numberScore);
        }
        LoaderScene loaderScene = FindObjectOfType<LoaderScene>() as LoaderScene;
        loaderScene.LoadScene(2);
    }

    public void pauseGame(){
        changeState(GameState.PAUSE);
        panelOptions.SetActive(true);
    }

    public void continueGame(){
        changeState(GameState.GAMEPLAY);
        panelOptions.SetActive(false);
    }

    public void colectKey(){
        numberCards+=1;
        numberCardTxt.text = numberCards.ToString();
    }
}
