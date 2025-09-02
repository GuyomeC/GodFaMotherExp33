using UnityEngine;

public class Prof : MonoBehaviour
{
    [SerializeField] GameObject go;

    void Start()
    {
        BoxCollider2D boxCollider = go.GetComponent<BoxCollider2D>();
        if(boxCollider != null)
        {
            Debug.Log("BoxCollider2D found on the GameObject.");
        }
        else
        {
            Debug.Log("BoxCollider2D not found on the GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Prof triggered");
        if (collision != null && collision.CompareTag("Player"))
        {
            Debug.Log("Prof triggered by Player");
        }
    }


    void Update()
    {
        
    }
}
