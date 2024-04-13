using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack;
    public bool fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    public Text coinText;

    public GameObject pausedScreen;
    public GameObject optionsScreen;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;



    void Awake()
    {
        instance  = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed* Time.deltaTime));

            if(blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed* Time.deltaTime));

            if(blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }


    public void Resume()
    {
        GameManager.instance.PauseUnpause();

    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void ColseOptions()
    {
        optionsScreen.SetActive(false);
    }
    public void LevelSelect()
    {

    }

    public void MainMenu()
    {

    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();

    }
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();

    }
}
