using System;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RectTransform PlayerIndicator;
    public float yOffset = 40f;
    public RectTransform IndicatorParent;

    private Transform target;
    private RectTransform indic;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        indic = Instantiate(PlayerIndicator, IndicatorParent);
    }

    private void FixedUpdate()
    {
        transform.position = target.position + new Vector3(0, yOffset, 0);
        Vector3 viewportPosition = GetComponent<Camera>().WorldToViewportPoint(transform.position);

        indic.anchoredPosition = viewportPosition;
    }
}