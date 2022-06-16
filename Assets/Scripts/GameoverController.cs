using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverController : MonoBehaviour
{

    [Header("UI Settings")]
    public  TMP_Text scoreNumber;
    public  TMP_Text scoreRecordNumber;

    [Header("Audio Settings")]

    public AudioClip soundSelect;
    public AudioSource audioEffectSource;


    // Start is called before the first frame update
    void Start()
    {
        int scoreSaved = PlayerPrefs.GetInt("score");
        int scoreRecordSaved = PlayerPrefs.GetInt("score_record");
        scoreNumber.text =  scoreSaved.ToString();
        scoreRecordNumber.text =  scoreRecordSaved.ToString();
    }

    public void backToHome(){
        Time.timeScale = 1;
        audioEffectSource.PlayOneShot(soundSelect);
        LoaderScene loaderScene = FindObjectOfType<LoaderScene>() as LoaderScene;
        loaderScene.LoadScene(0);
    }
}
