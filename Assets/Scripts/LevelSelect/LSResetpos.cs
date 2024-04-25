using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LSResetpos : MonoBehaviour
{
    public static LSResetpos instance;
    public Vector3 respawnPosition;

    void Awake()
    {
        instance = this;
    }
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
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = respawnPosition;
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
