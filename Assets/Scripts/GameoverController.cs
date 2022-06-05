using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameoverController : MonoBehaviour
{

    public  TMP_Text scoreNumber;
    public  TMP_Text scoreRecordNumber;


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
        SceneManager.LoadScene("Home");
    }
}
