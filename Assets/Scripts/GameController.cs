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
    private float curXBG;
    private float curXFloor;
    private int numberScore;
    private int numberCards;
    private float currentTime;

    [Header ("Settings general")]

    public float speedGame;

	public GameState currentState;


    [Header ("Movement Background")]

    public MeshRenderer meshRendererBG;

    [Header ("Movement Floor")]

    public float speedFloor=1;
    public TilemapRenderer tilemapRendererFloor;


    [Header("UI Settings")]

    public TMP_Text numberTxt;
    public TMP_Text numberCardTxt;
    public float rateTime;
    public GameObject panelOptions;
    public GameObject panelHUD;

    [Header("Characteres Database")]
	public string[] spriteSheetName;
	public int idPersonagem;
	public int idPersonagemAtual;

    [Header("Audios Settings")]
    public AudioClip[] soundsBackground;
    public AudioSource audioSource;
    public AudioSource audiosEffectsSource;
    public AudioClip soundSelect;
    public AudioClip soundDied;
    public AudioClip soundCollect;


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
        panelHUD.SetActive(true);
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
        audiosEffectsSource.PlayOneShot(soundDied);
        if( numberScore> PlayerPrefs.GetInt("score_record")){
            PlayerPrefs.SetInt("score_record", numberScore);
        }
        panelHUD.SetActive(false);
        LoaderScene loaderScene = FindObjectOfType<LoaderScene>() as LoaderScene;
        loaderScene.LoadScene(2);
    }

    public void exitGame(){
        changeState(GameState.GAMEOVER);
        panelHUD.SetActive(false);
        audiosEffectsSource.PlayOneShot(soundSelect);
        LoaderScene loaderScene = FindObjectOfType<LoaderScene>() as LoaderScene;
        loaderScene.LoadScene(0);
    }

    public void pauseGame(){
        changeState(GameState.PAUSE);
        audiosEffectsSource.PlayOneShot(soundSelect);
        panelOptions.SetActive(true);
    }

    public void continueGame(){
        changeState(GameState.GAMEPLAY);
        audiosEffectsSource.PlayOneShot(soundSelect);
        panelOptions.SetActive(false);
    }

    public void colectKey(){
        audiosEffectsSource.PlayOneShot(soundCollect);
        numberCards+=1;
        numberCardTxt.text = numberCards.ToString();
    }
}
