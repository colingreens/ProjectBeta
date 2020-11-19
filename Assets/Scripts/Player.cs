using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _moveSpeed = 9f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.2f;
    [SerializeField]
    private float _glideSpeed = 0.001f;
   
    
    [SerializeField]
    private float _directionY;

    private float _gravityModifier = 1f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        

        Vector3 direction = new Vector3(horizontalInput, 0);

        if (_controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _gravityModifier = 1f;
                _directionY = _jumpSpeed;
            }
        }
        else
        {            
            if (Input.GetButton("Jump") && _directionY <= .1f)
            {
                _gravityModifier = _glideSpeed;
            }
            else if (!Input.GetButton("Jump"))
            {
                _gravityModifier = 1f;
            }
            {
                //_gravityModifier = 1f;
            }
        } 

        _directionY -= _gravity * _gravityModifier * Time.deltaTime;
        direction.y = _directionY;

        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }
}
