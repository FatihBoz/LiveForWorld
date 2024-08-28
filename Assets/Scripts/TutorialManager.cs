using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float WaitingTimeBetweenSpeeches;
    public TextMeshProUGUI subtitle;
    public GameObject panel;

    void Start()
    {

        StartCoroutine(StartTutorial());
    }

    private IEnumerator WaitSpeechToEnd(string text)
    {
        panel.SetActive(true);
        subtitle.text = text;
        yield return new WaitForSeconds(WaitingTimeBetweenSpeeches);
        panel.SetActive(false);
    }

    private IEnumerator StartTutorial()
    {
        StartCoroutine(WaitSpeechToEnd(TutorialTextManager.Instance.StartingText1));
        yield return new WaitForSeconds(WaitingTimeBetweenSpeeches+2);
        StartCoroutine(WaitSpeechToEnd(TutorialTextManager.Instance.StartingText2));
        yield return new WaitForSeconds(WaitingTimeBetweenSpeeches+2);
        StartCoroutine(WaitSpeechToEnd(TutorialTextManager.Instance.StartingText3));
    }

    private void OnEnable()
    {
        Building.OnBuildingHealthChanged += Tutorial_OnBuildingHealthChanged;
    }

    private void Tutorial_OnBuildingHealthChanged()
    {
        StartCoroutine(WaitSpeechToEnd(TutorialTextManager.Instance.BuildingRepairText));
    }

    private void OnDisable()
    {
        Building.OnBuildingHealthChanged -= Tutorial_OnBuildingHealthChanged;
    }

}
