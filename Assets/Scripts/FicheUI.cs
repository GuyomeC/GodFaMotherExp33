using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FicheUI : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _desc;
    [SerializeField] Image _image;

    public void UpdateSheet(string name,Sprite img,string desc)
    {
        _name.text = name;
        _desc.text = desc;
        _image.sprite = img;
    }
}
