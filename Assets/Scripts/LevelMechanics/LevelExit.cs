using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelExit : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Hit");
            StartCoroutine(GameManager.instance.LevelEndCo());

        }
    }
}
