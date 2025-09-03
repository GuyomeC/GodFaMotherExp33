using System;
using System.Collections;
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

    private float zoomInput;
    private bool isZoomed;


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
    public void OnZoom(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<float>();
        if (zoomInput > 0)
        {
            isZoomed = false;
            StopAllCoroutines();
            StartCoroutine(Zooming());
        }
        else
        { 
            isZoomed = true;
            StopAllCoroutines();
            StartCoroutine(Zooming());
        }
    }
    IEnumerator Zooming()
    {
        float value = cam.Lens.OrthographicSize;
        float timer = 0;
        if (isZoomed)
        {
            while (timer < 1)
            {
                cam.Lens.OrthographicSize = Mathf.Lerp(value, 545, timer);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timer < 1)
            {
                cam.Lens.OrthographicSize = Mathf.Lerp(value, 125, timer);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        cam.transform.Translate(move * speed * Time.deltaTime);
    }
}
