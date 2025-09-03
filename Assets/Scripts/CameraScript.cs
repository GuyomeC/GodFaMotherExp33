using System;
using System.Collections;
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
    [SerializeField] private crosshair crosshairScript;

    private Vector2 moveInput;
    private Vector2 velocity;

    private Vector2 minPosition = new Vector2(-300,-160);
    private Vector2 maxPosition = new Vector2(300, 160);

    private float zoomInput;
    private bool isZoomed;

    private GameObject crosshair;
    private Vector2 moveCrosshairInput;
    private Vector2 velocityCrosshair;


    void Start()
    {
        CinemachineConfiner2D confiner = cam.GetComponent<CinemachineConfiner2D>();
        BoxCollider2D boxCollider = confiner.BoundingShape2D as BoxCollider2D;
        crosshair = GameObject.Find("Crosshair");
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

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            foreach (GameObject prof in crosshairScript.profs)
            {
                Destroy(prof);
            }
            crosshairScript.profs.Clear();
        }
    }

    public void OnMoveCrosshair(InputAction.CallbackContext context)
    {
        moveCrosshairInput = context.ReadValue<Vector2>();
        velocityCrosshair = moveCrosshairInput * speed;
        crosshair.transform.Translate(velocityCrosshair * Time.deltaTime);
    }

    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        cam.transform.Translate(move * speed * Time.deltaTime);

        Vector2 crossshairMovement = new Vector2(moveCrosshairInput.x, moveCrosshairInput.y);
        crosshair.transform.Translate(crossshairMovement * speed * Time.deltaTime);
    }
}
