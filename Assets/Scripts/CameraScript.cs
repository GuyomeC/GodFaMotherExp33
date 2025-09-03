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
    [SerializeField] private List<GameObject> profs = new List<GameObject>();

    private Vector2 moveInput;
    private Vector2 velocity;

    private float zoomInput;
    private bool isZoomed;


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
    public void OnZoom(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<float>();
        if (zoomInput > 0)
        {
            isZoomed = false;
           
        }
        else
        { 
            isZoomed = true;
        }
        StopAllCoroutines();
        StartCoroutine(Zooming());
    }
    IEnumerator Zooming()
    {
        float value = cam.Lens.OrthographicSize;
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
