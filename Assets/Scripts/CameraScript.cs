using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField, Range(0, 500)] float speed = 10f;
    [SerializeField] CinemachineCamera cam;
    [SerializeField] private List<GameObject> profs = new List<GameObject>();

    private Vector2 moveInput;
    private Vector2 velocity;

    void Start()
    {
        CinemachineConfiner2D confiner = cam.GetComponent<CinemachineConfiner2D>();
        BoxCollider2D boxCollider = confiner.BoundingShape2D as BoxCollider2D;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            profs.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            profs.Remove(collision.gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        velocity = moveInput * speed;
        cam.transform.Translate(velocity * Time.deltaTime);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            foreach (GameObject prof in profs)
            {
                Destroy(prof);
            }
            profs.Clear();
        }
    }

    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        cam.transform.Translate(move * speed * Time.deltaTime);
    }
}
