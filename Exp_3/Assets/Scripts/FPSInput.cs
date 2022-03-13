using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(CharacterController)))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 3.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;
    private Vector3 jumpVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_charController.isGrounded && jumpVelocity.y < 0.0f)
        {
            jumpVelocity.y = 0.0f;
        }
        
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        /*
        if (_charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -jumpSpeed * gravity);
        }

        jumpVelocity.y += jumpSpeed * gravity * Time.deltaTime;
        movement += jumpVelocity;
        */
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}   
