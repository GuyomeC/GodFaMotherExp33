using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] Rigidbody2D rb;
    [SerializeField, Range(0,100)] float speed = 10f;

    public event Action StartCameraMove;
    public event Action StopCameraMove;

    void Start()
    {
        
    }

    private void OnDestroy()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Camera triggered");
        if (collision != null && collision.CompareTag("Player"))
        {
            Debug.Log("Camera triggered by Player");
        }
    }
    void Update()
    {
        
    }
}
