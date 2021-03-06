using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class HomeGameController : MonoBehaviour
{
    [Header ("Settings general")]

    public float speedGame;

    private float curXBG;
    public MeshRenderer  meshRendererBG;

    [Header("UI Settings")]
    public TMP_Text numberCardTxt;
    public TMP_Text numberCardPayTxt;


    [Header("Shop Settings")]
    public int[] valuesToPay;
    public GameObject[] buttonsToPay;

    public GameObject panelMenu;
    public int numberCards;

    private ButtonsBought buttonsBought;

    [Header("Audios Settings")]
    public AudioSource audiosEffectSource;
    public AudioClip soundBuy;
    public AudioClip soundSelect;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale =1;
        panelMenu.SetActive(false);
        numberCards = PlayerPrefs.GetInt("numberCards");
        numberCardTxt.text = numberCards.ToString();
        numberCardPayTxt.text = numberCards.ToString();

        string jsonButtons = PlayerPrefs.GetString("ButtonsToPay");
        
        if(jsonButtons!=""){                

            buttonsBought = JsonUtility.FromJson<ButtonsBought>(jsonButtons);
            
            foreach (var item in buttonsBought.buttons)
            {
                buttonsToPay[item.position].GetComponent<Image>().color =  new Color(255,255,255);         
                buttonsToPay[item.position].GetComponent<SettingsButton>().isBought = true;
            }

        }else{
            buttonsBought = new ButtonsBought();
        }


    }
      void FixedUpdate()
    {
        //Movement Background
        curXBG+= Time.deltaTime*speedGame;

        meshRendererBG.material.SetTextureOffset("_MainTex", new Vector2(curXBG,0)) ;
       
    }

    public void playClickButton(AudioClip audioClip){
        audiosEffectSource.PlayOneShot(audioClip);
    }

    public void openMenu(){
        panelMenu.SetActive(true);
    }

    public void exitMenu(){
        panelMenu.SetActive(false);
    }

    public void buyCharacter(int position){
       if(buttonsToPay[position].GetComponent<SettingsButton>().isFirstButton)return;
       if(buttonsToPay[position].GetComponent<SettingsButton>().isBought)return;

       if(numberCards>=valuesToPay[position])
       {
           numberCards-= valuesToPay[position];
           PlayerPrefs.SetInt("numberCards", numberCards);

           numberCardTxt.text = numberCards.ToString();
           numberCardPayTxt.text = numberCards.ToString();

           buttonsToPay[position].GetComponent<Image>().color =  new Color(255,255,255); 

        

           buttonsToPay[position].GetComponent<SettingsButton>().isBought = true;

           audiosEffectSource.PlayOneShot(soundBuy);

           buttonsBought.buttons.Add(new ButtonToPay(position));

           PlayerPrefs.SetString("ButtonsToPay", JsonUtility.ToJson(buttonsBought));


       }
    }

    public void selectCharacter(int position){
        if(!buttonsToPay[position].GetComponent<SettingsButton>().isBought&&position>0)return;
        
        audiosEffectSource.PlayOneShot(soundSelect);
        PlayerPrefs.SetInt("idPersonagem", position);
    }


    [System.Serializable]
    public class ButtonsBought{
        public List<ButtonToPay> buttons = new List<ButtonToPay>();
    }

    
    [System.Serializable]
    public class ButtonToPay{
        public int position;

        public ButtonToPay(int position){
            this.position = position;
        }
    }
}
