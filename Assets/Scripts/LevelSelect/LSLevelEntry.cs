using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public String levelName, levelToCheck, displayName;
    private bool canLoadLevel ;
    private bool levelUnlocked, loadingLevel;
    public GameObject mapPointActive, mapPointInactive;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
        {
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            levelUnlocked = true;
        }
        else
        {
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            levelUnlocked = false;
        }

        if(PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            PlayerController.instance.transform.position = transform.position;
            LSResetpos.instance.respawnPosition = transform.position;
        }

    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked && !loadingLevel)
        {
            StartCoroutine(LevelLoadCo());
            loadingLevel = true;

            
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = true;
            LSUIManager.instance.lNamePanel.SetActive(true);
            LSUIManager.instance.lNameText.text = displayName;

            if(PlayerPrefs.HasKey(levelName + "_coins"))
            {
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
            }
            else
            {
                LSUIManager.instance.coinsText.text = "???";
            }
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
            LSUIManager.instance.lNamePanel.SetActive(false);
           
        }
    }

    public IEnumerator LevelLoadCo()
    {
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;
        
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel" , levelName);
    }
}
