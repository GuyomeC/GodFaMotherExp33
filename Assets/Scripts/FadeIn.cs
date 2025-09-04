using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image Image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Image.color.a < 1)
        {
        }
    }
}
