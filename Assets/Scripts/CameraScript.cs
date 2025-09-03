using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField, Range(0, 500)] float speed = 10f;
    [SerializeField] CinemachineCamera cam;
    
    private Vector2 moveInput;
    private Vector2 velocity;

    void Start()
    {
        CinemachineConfiner2D confiner = cam.GetComponent<CinemachineConfiner2D>();
        BoxCollider2D boxCollider = confiner.BoundingShape2D as BoxCollider2D;

        Debug.Log("Camera confiner: " + confiner);
        Debug.Log("Camera collider: " + boxCollider);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        velocity = moveInput * speed;
        cam.transform.Translate(velocity * Time.deltaTime);
    }

    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        cam.transform.Translate(move * speed * Time.deltaTime);
    }
}
