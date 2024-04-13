using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;
    public int currentHealth, maxHealth;
    public float invinsibleLength = 2;
    private float invinceCounter;

    public Sprite[] healthBarImages;


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCounter > 0)
        {
            invinceCounter -= Time.deltaTime;

           
                for(int i = 0 ; i < PlayerController.instance.playerPices.Length; i++)
                {
                    if(Mathf.Floor(invinceCounter * 5f) % 2 == 0)
                    {
                        PlayerController.instance.playerPices[i].SetActive(true);
                    }
                    else
                    {
                        PlayerController.instance.playerPices[i].SetActive(false);
                    }

                    if(invinceCounter <= 0)
                    {
                       PlayerController.instance.playerPices[i].SetActive(true); 
                    }
                }    
            
        }
    }

    public void Hurt(int damage)
    {
        if  (invinceCounter <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.KncockBack();
                invinceCounter = invinsibleLength;

               
            }   

            UpdateUI();   
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void  AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch(currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;

        }
    }

    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}