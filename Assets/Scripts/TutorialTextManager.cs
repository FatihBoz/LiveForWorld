using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour
{
    public static TutorialTextManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("OYUN BA�LANGICI")]
    [TextArea] public string StartingText1;
    [TextArea] public string StartingText2;
    [TextArea] public string StartingText3;

    [Header("D��MAN KANI TOPLA")]
    [TextArea] public string EnemyBloodText;

    [Header("YAPI ONARIMI")]
    [TextArea] public string BuildingRepairText;
}
