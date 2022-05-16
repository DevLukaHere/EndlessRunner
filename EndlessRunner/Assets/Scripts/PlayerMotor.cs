using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 6.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 20.0f;

    private float animationDuration;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animationDuration = Time.time + 3.0f;
        controller = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            return;
        }

        if(Time.time < animationDuration) //blocks player move during start animation 
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if(controller.isGrounded)
        {
            verticalVelocity = -0.5f;    
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //X
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //Y
        moveVector.y = verticalVelocity;
        //Z
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }

    public void AddSpeed()
    {
        speed += 4.0f;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
