using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public GameObject loadScenePanel;
    public Slider slider;
    public TMP_Text percent;

    void Start() {
        loadScenePanel.SetActive(false);
    }

    // Start is called before the first frame update
    public void LoadScene(int levelId){
        StartCoroutine(LoadAsynchronously(levelId));
    }

    IEnumerator LoadAsynchronously(int levelId){

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelId);       
        loadScenePanel.SetActive(true);        

        

        while(!asyncOperation.isDone){
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f);
            slider.value = progress;
            percent.text = (progress*100.0f).ToString("0.00")+"%";
            yield return null;
        }

    }
}
