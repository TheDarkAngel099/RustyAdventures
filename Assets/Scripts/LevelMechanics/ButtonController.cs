using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{


    public bool isPressed;
    public Transform button, buttonDown;
    private Vector3 buttonUP;
    public bool isOnOff;


    // Start is called before the first frame update
    void Start()
    {
        buttonUP = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            if(isOnOff)
            {
                if (isPressed)
                {
                    button.position = buttonUP;
                    isPressed = false;
                }
                else
                {
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }
            else
            {  
                if(!isPressed)
                {
                    button.position = buttonDown.position;
                    isPressed = true;
                }

            }
           
        }
    }
}
