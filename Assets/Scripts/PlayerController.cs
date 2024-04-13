using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed ; // The speed the player will move.
    public float jumpForce;
    public float bounceForce;
    public float gravityScale = 5f;
    private Vector3 moveDirection;
    public CharacterController charController;
    Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator anim;

    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float kncockBackCounter;
    public Vector2 knockBackPower;

    public static PlayerController instance;
    public GameObject[] playerPices;

    
    void Awake() 
    {
        instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(! isKnocking)
        {

        float yStore = moveDirection.y; 
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if(charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump")) 
            {
                moveDirection.y = jumpForce;
            }

        }
        else 
        {
            moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        
        charController.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
             transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y , 0f);
             Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f , moveDirection.z));
             playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation,rotateSpeed* Time.deltaTime );

        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z) );
        anim.SetBool("Grounded", charController.isGrounded);

        }

        if(isKnocking)
        {
            kncockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y; 
            moveDirection = (playerModel.transform.forward * -knockBackPower.x); 
            moveDirection.y = yStore;

            if(charController.isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime *gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

           

            if (kncockBackCounter <= 0 )
            {
                isKnocking = false;
            }
            
            anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z) );
            anim.SetBool("Grounded", charController.isGrounded);
        }

       
    }

    public void KncockBack()
    {
        isKnocking = true;
        kncockBackCounter = knockBackLength;
        Debug.Log("knocked");

        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection* Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);

    }


}
