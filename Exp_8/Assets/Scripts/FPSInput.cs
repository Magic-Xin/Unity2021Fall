using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent((typeof(CharacterController)))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float runSpeed = 6.0f;
    public float jumpSpeed = 3.0f;
    public float terminalVelocity = -20.0f;
    public float gravity = -9.8f;
    public float minFall = -9.8f;

    private CharacterController _charController;
    private Animator _animator;
    
    private ControllerColliderHit _contact;
    private float _vertSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        } else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = moveSpeed;
        }

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        _animator.SetFloat("Speed", movement.sqrMagnitude);
        
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
		
        if (hitGround) {
            if (Input.GetButtonDown("Jump")) {
                _vertSpeed = jumpSpeed;
            } else {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        } else {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity) {
                _vertSpeed = terminalVelocity;
            }
            if (_contact != null ) {
                _animator.SetBool("Jumping", true);
            }
			
            if (_charController.isGrounded) {
                if (Vector3.Dot(movement, _contact.normal) < 0) {
                    movement = _contact.normal * moveSpeed;
                } else {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }
        movement.y += _vertSpeed;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit) {
        _contact = hit;
    }
}