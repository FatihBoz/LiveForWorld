using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public float WaitingTimeBetweenSpeeches;
    public Image StartingPanel;
    public TextMeshProUGUI subtitle;
    public GameObject panel;

    void Start()
    {
        StartCoroutine(WaitForStartingScene());
        
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

    private IEnumerator WaitForStartingScene()
    {
        StartingPanel.DOFade(0f, 1f);
        yield return new WaitForSeconds(2f);
        StartingPanel.gameObject.SetActive(false);
        StartCoroutine(StartTutorial());
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
