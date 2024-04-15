using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathEffect;
    public Vector3 respawnPosition ;

    public int currentCoins;
    public int levelEndMusic = 8;

    public string nextLevelToLoad;

    void Awake()
    {
        instance = this;
    }

    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.instance.transform.position;


        AddCoins(0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();
        
    }
    public IEnumerator RespawnCo()
    {
        Debug.Log("Respawning");
        //yield return new WaitForSeconds(2f);
        PlayerController.instance.gameObject.SetActive(false);  
        CameraController.instance.theCNBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f,1f,0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(1f);

        HealthManager.instance.ResetHealth();
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.gameObject.transform.position = respawnPosition; 
        CameraController.instance.theCNBrain.enabled = true;
        

        PlayerController.instance.gameObject.SetActive(true);

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = ""  + currentCoins;
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pausedScreen.activeInHierarchy)
        {
            UIManager.instance.pausedScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pausedScreen.SetActive(true);
            UIManager.instance.optionsScreen.SetActive(false);


            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }


    public IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;
        yield return new WaitForSeconds(4f);
        Debug.Log("Level Ended");

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        SceneManager.LoadScene(nextLevelToLoad);

    }
}
