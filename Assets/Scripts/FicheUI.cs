using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class FicheUI : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _desc;
    [SerializeField] Image _image;
    [SerializeField] Image _bg;
    [SerializeField] InputActionReference inputActionReference;

    public void UpdateSheet(string name,Sprite img,string desc)
    {
        _name.gameObject.SetActive(true);
        _desc.gameObject.SetActive(true);
        _image.gameObject.SetActive(true);
        _name.text = name;
        _desc.text = desc;
        _image.sprite = img;
    }
    public void DisplaySheet()
    {
        if (_name.gameObject.activeInHierarchy)
        {
            _name.gameObject.SetActive(false);
            _desc.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
            _bg.gameObject.SetActive(false);
        }
        else
        {
            _name.gameObject.SetActive(true);
            _desc.gameObject.SetActive(true);
            _image.gameObject.SetActive(true);
            _bg.gameObject.SetActive(true);
        }
    }
    public void OnSheetDisplay(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            DisplaySheet();
        }
    }
}
