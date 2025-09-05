using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField, Range(0, 500)] float cameraSpeed = 10f;
    [SerializeField, Range(0, 500)] float crossHairSpeed = 200f;
    [SerializeField] CinemachineCamera cam;
    [SerializeField] private crosshair crosshairScript;
    [SerializeField] private GameObject mask;
    [SerializeField] private sheetManager sheetManager;
    [SerializeField] private CinemachineVolumeSettings volume;
    [SerializeField] private Vignette vignette;
    [SerializeField] private FicheUI fiche;

    private Vector2 moveInput;
    private Vector2 velocity;

    private Vector2 minPosition = new Vector2(-300, -160);
    private Vector2 maxPosition = new Vector2(300, 160);

    private float zoomInput;
    private bool isZoomed;

    private GameObject crosshair;
    private Vector2 moveCrosshairInput;
    private Vector2 velocityCrosshair;

    [SerializeField] Timers timer;
    public UnityEvent OnGoodGuess;
    public UnityEvent OnBadGuess;
    public UnityEvent OnCoin;

    public bool IsZoomed { get => isZoomed; set => isZoomed = value; }

    void Start()
    {
        foreach (VolumeComponent volumeComponent in volume.Profile.components)
        {
            if (volumeComponent.name == "Vignette") vignette = volumeComponent as Vignette;
        }
            CinemachineConfiner2D confiner = cam.GetComponent<CinemachineConfiner2D>();
        BoxCollider2D boxCollider = confiner.BoundingShape2D as BoxCollider2D;
        crosshair = GameObject.Find("Crosshair");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        velocity = moveInput * cameraSpeed;
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
        else if (!isZoomed && zoomInput < 0)
        {
            isZoomed = true;
            crosshair.gameObject.SetActive(false);
            mask.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Zooming());
        }

    }
    IEnumerator Zooming()
    {
        cam.transform.position = new Vector3(0, 0, -10);
        float value = cam.Lens.OrthographicSize;
        Vector3 currentPos = cam.transform.position;
        float timer = 0;
        if (isZoomed)
        {
            while (timer < 3)
            {
                vignette.intensity.value = Mathf.Lerp(.68f, .3f, timer / 3);
                cam.Lens.OrthographicSize = Mathf.Lerp(cam.Lens.OrthographicSize, 1600, timer / 3);
                if (cam.Lens.OrthographicSize > 1590)
                {
                    cam.Lens.OrthographicSize = 1600;
                }
                Debug.Log(transform.position);
                timer += Time.deltaTime;
                yield return null;
            }

        }
        else
        {
            while (timer < 3)
            {
                vignette.intensity.value = Mathf.Lerp(.3f, .68f,timer / 3) ;
                cam.Lens.OrthographicSize = Mathf.Lerp(value, 175, timer / 3);
                if (cam.Lens.OrthographicSize < 180)
                {
                    cam.Lens.OrthographicSize = 175;
                }
                timer += Time.deltaTime;
                yield return null;
            }
            mask.SetActive(true);
        }
        yield return null;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && !isZoomed)
        {
            if (crosshairScript.profs.Count > 0 && crosshairScript.profs[0].tag == "Coin")
            {
                OnCoin.Invoke();
                timer.Timer += 5;
                Destroy(crosshairScript.profs[0].gameObject);
                return;
            }

            if (crosshairScript.profs.Count < 1 || !crosshairScript.profs.Find(delegate (GameObject x)
             {
                 return x.name == sheetManager.CurrentSheet.target.name;
             }))
            {
                OnBadGuess.Invoke();
                timer.Timer -= 5;
            }
            else
            {
                OnGoodGuess.Invoke();
                sheetManager.ChangeSheet();
            }
            /*  foreach (GameObject prof in crosshairScript.profs)
              {
                  Destroy(prof);
              }
            crosshairScript.profs.Clear();*/
        }
    }

    public void OnMoveCrosshair(InputAction.CallbackContext context)
    {
        if (!isZoomed)
        {
            moveCrosshairInput = context.ReadValue<Vector2>();
            velocityCrosshair = moveCrosshairInput * crossHairSpeed;
            crosshair.transform.Translate(velocityCrosshair * Time.deltaTime);
        }
    }
    public void Dezoom()
    {
        isZoomed = true;
        crosshair.gameObject.SetActive(false);
        mask.SetActive(false);
        fiche.Undisplay();
        StopAllCoroutines();
        StartCoroutine(Zooming());
    }
    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        if (!isZoomed)
        {
            cam.transform.Translate(move * cameraSpeed * Time.deltaTime);
            Vector2 crossshairMovement = new Vector2(moveCrosshairInput.x, moveCrosshairInput.y);
            crosshair.transform.Translate(crossshairMovement * crossHairSpeed * Time.deltaTime);
        }


    }
}
