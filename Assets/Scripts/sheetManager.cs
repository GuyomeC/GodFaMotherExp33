using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class sheetManager : MonoBehaviour
{
    [SerializeField] CharaSheet _sheets;
    [SerializeField] int howManyCats;
    List<Sheet> listSheet = new List<Sheet>();
    [SerializeField] FicheUI _ficheUI;
    [SerializeField] private GameObject _goodEndPanel;
    [SerializeField] Sheet _currentSheet;
    [SerializeField] private PlayerInput input;
    [SerializeField] private CameraScript cam;

    AudioManager AudioManager;

    public Sheet CurrentSheet { get => _currentSheet;}

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        foreach(Sheet s in _sheets.sheets)
        {
            listSheet.Add(s);
        }
        ChangeSheet();
    }

   public void ChangeSheet()
    {
        if (howManyCats > 0)
        {
            int rand = Random.Range(0, listSheet.Count);
            _currentSheet = listSheet[rand];
            _ficheUI.UpdateSheet(CurrentSheet.name, CurrentSheet.visu, CurrentSheet.desc);
            listSheet.Remove(listSheet[rand]);
            howManyCats--;
        }
        else
        {
            _goodEndPanel.SetActive(true) ;
            AudioManager?.PlaySFX(AudioManager.winSound);
            AudioManager.musicSource.volume -= AudioManager.musicVolumeChange;
            cam.Dezoom();
            input.DeactivateInput();
        }
    }
}
