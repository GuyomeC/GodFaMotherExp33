using UnityEngine;

public class Prof : MonoBehaviour
{
    [SerializeField] GameObject _prof1;
    [SerializeField] GameObject _prof2;

    void Start()
    {
        BoxCollider2D boxColliderProf1 = _prof1.GetComponent<BoxCollider2D>();
        BoxCollider2D boxColliderProf2 = _prof2.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Prof triggered");
        if (collision != null)
        {
            Debug.Log("Prof triggered by Player");
        }
    }


    void Update()
    {
        
    }
}
