using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    public List<GameObject> profs = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            profs.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null)
        {
            profs.Clear();
        }
    }
}
