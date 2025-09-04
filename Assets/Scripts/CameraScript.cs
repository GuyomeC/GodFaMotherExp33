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
        if (!isZoomed)
        {
            cam.transform.Translate(velocity * Time.deltaTime);
        }
    }
    public void OnZoom(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<float>();
        if (zoomInput > 0 && isZoomed)
        {
            isZoomed = false;
            crosshair.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Zooming());
        }
        else if(!isZoomed&&zoomInput<0)
        { 
            isZoomed = true;
            crosshair.gameObject.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Zooming());
        }

    }
    IEnumerator Zooming()
    {
        float value = cam.Lens.OrthographicSize;
        Vector3 currentPos = cam.transform.position;
        float timer = 0;
        if (isZoomed)
        {
            while (timer < 3)
            {
                cam.Lens.OrthographicSize = Mathf.Lerp(cam.Lens.OrthographicSize, 535, timer/3);
                if (cam.Lens.OrthographicSize > 530)
                {
                    cam.Lens.OrthographicSize = 535;
                }
                cam.transform.position=Vector3.Lerp(currentPos, new Vector3(0, 0, -10), timer / 3);
                Debug.Log(transform.position);
                timer += Time.deltaTime;
                yield return null;
            }

        }
        else
        {
            while (timer < 3)
            {
                cam.Lens.OrthographicSize = Mathf.Lerp(value, 125, timer/3);
                if (cam.Lens.OrthographicSize < 130)
                {
                    cam.Lens.OrthographicSize = 125;
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed&&!isZoomed)
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
        if (!isZoomed)
        {
            moveCrosshairInput = context.ReadValue<Vector2>();
            velocityCrosshair = moveCrosshairInput * speed;
            crosshair.transform.Translate(velocityCrosshair * Time.deltaTime);
        }
    }

    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        if (!isZoomed)
        {
            cam.transform.Translate(move * speed * Time.deltaTime);
            Vector2 crossshairMovement = new Vector2(moveCrosshairInput.x, moveCrosshairInput.y);
            crosshair.transform.Translate(crossshairMovement * speed * Time.deltaTime);
        }

        
    }
}
