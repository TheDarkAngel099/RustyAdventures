using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maximumHealth = 1;
    private float currentHealth;
    public int deathsound;
    public GameObject deathEffect, itemToDrop;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0 )
        {
            AudioManager.instance.PlaySFX(deathsound);
            PlayerController.instance.Bounce();
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position + new Vector3(0f,1.2f,0f), transform.rotation);    
            Instantiate(itemToDrop, transform.position + new Vector3(0.5f,0.2f,0f), transform.rotation); 

        }

    }
}
