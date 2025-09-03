using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField, Range(0,500)] float speed = 10f;
    
    private Vector2 moveInput;
    private Vector2 velocity;

    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Camera triggered");
        if (collision != null)
        {
            Debug.Log("Camera triggered by Player");
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        velocity = moveInput * speed;
        transform.Translate(velocity * Time.deltaTime);
    }

    void Update()
    {
        
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
