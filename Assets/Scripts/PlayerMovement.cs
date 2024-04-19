using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    CharacterController controller;
    public float gravity = -9.81f; //m/s/s
    Vector3 velocity;
    public bool isGrounded;
    public LayerMask groundLayers;
    public float groundDistance = 0.1f;
    public float jumpHeight = 5f;
    private Animator animator;
  //  private Transform cam;
    private Vector3 horizontalVel;
   
    private float horizontalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
       // cam = transform.GetChild(0);

    }
    void CheckIfGrounded()
    {
 //Check if player is Grounded
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayers, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; //To force player to the ground

        }
        else { 
            velocity.y +=  gravity * Time.deltaTime; //Change the velocity in y axis

        }
    }
    // Update is called once per frame
    void Update()
    {   
       CheckIfGrounded();
        //Move player: 

        //Get player's Input
        float hmove = Input.GetAxis("Horizontal");
        float vmove = Input.GetAxis("Vertical");

        Vector3 move = transform.right * hmove + transform.forward * vmove; //In order to move in the direction that player faces  

         if (isGrounded && Input.GetButtonDown("Jump")) {
            velocity.y += Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        //Account for player's input
        controller.Move(move * Time.deltaTime * moveSpeed);
        //Account for gravity
        controller.Move(velocity * 0.5f* Time.deltaTime); //g*t^2*0.5

        horizontalVel = new Vector3(move.x, 0f, move.z);
        horizontalSpeed = horizontalVel.magnitude;
      //  animator.SetFloat("velocity", horizontalSpeed);
    }
    public float GetHorizontalSpeed()
    {
        return horizontalSpeed;
    }
    
}
