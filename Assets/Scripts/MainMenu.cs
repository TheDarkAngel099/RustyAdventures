using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string level1 , levelSelect;
    public GameObject continueButton;
    public string[] levels;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Continue"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            ResetProgress();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(level1);
        PlayerPrefs.SetInt("Continue", 0);
        PlayerPrefs.SetString("CurrentLevel" , level1);
        ResetProgress();

    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {   
        Application.Quit();
        Debug.Log("QuitGame");

    }

    public void ResetProgress()
    {
        for (int i=0  ; i< levels.Length; i++)
        {
            PlayerPrefs.SetInt(levels[i] + "_unlocked", 0);
        }
    }
}
