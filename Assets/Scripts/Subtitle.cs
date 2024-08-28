using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    [TextArea]
    [SerializeField] string subtitle;

    public string GetTitle() => subtitle;
}
