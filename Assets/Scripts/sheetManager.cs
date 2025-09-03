using System.Collections.Generic;
using UnityEngine;

public class sheetManager : MonoBehaviour
{
    [SerializeField] CharaSheet _sheets;
    [SerializeField] int howManyCats;
    List<Sheet> listSheet = new List<Sheet>();
    [SerializeField] FicheUI _ficheUI;
    Sheet _currentSheet;
    void Start()
    {
        foreach(Sheet s in _sheets.sheets)
        {
            listSheet.Add(s);
        }
        ChangeSheet();
    }

   void ChangeSheet()
    {
        if (howManyCats > 0)
        {
            int rand = Random.Range(0, listSheet.Count);
            _currentSheet = listSheet[rand];
            _ficheUI.UpdateSheet(_currentSheet.name, _currentSheet.visu, _currentSheet.desc);
            listSheet.Remove(listSheet[rand]);
            howManyCats--;
        }
        else
        {
            //endgame;
        }
    }
}
