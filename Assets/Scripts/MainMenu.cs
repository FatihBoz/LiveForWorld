using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<TextMeshProUGUI> ButtonTexts;
    public GameObject ButtonPanel;
    public GameObject BackgroundImage;

    private void OnEnable()
    {
        CinematicManager.OnCinematicFinished += MainMenu_OnCinematicFinished;
    }

    private void MainMenu_OnCinematicFinished()
    {
        BackgroundImage.SetActive(true);
        ButtonPanel.GetComponent<Image>().DOFade(.5f, 1f);
        foreach (var button in ButtonTexts)
        {
            button.DOFade(1f,2f);
        }
    }

    private void OnDisable()
    {
        CinematicManager.OnCinematicFinished -= MainMenu_OnCinematicFinished;
    }
}
