using UnityEngine;

public class Prof : MonoBehaviour
{
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log(collision);
        }
    }

    void Update()
    {
        
    }
}
