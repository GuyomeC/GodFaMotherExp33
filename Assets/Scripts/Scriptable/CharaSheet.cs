using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaSheet", menuName = "Scriptable Objects/CharaSheet")]
public class CharaSheet : ScriptableObject
{
    [SerializeField]public  List<Sheet> sheets;
}

