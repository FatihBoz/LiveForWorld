using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public List<TextMeshProUGUI> ButtonTexts;
    public GameObject ButtonPanel;
    public GameObject BackgroundImage;
    public Image EndingPanel;

    private void OnEnable()
    {
        CinematicManager.OnCinematicFinished += MainMenu_OnCinematicFinished;
    }

    private void MainMenu_OnCinematicFinished()
    {
        BackgroundImage.SetActive(true);
        ButtonPanel.GetComponent<Image>().DOFade(.45f, 1f);
        foreach (var button in ButtonTexts)
        {
            button.DOFade(1f,2f);
        }
    }

    private void OnDisable()
    {
        CinematicManager.OnCinematicFinished -= MainMenu_OnCinematicFinished;
    }

    public void ChangeScene(string sceneName)
    {
        EndingPanel.gameObject.SetActive(true);
        EndingPanel.DOFade(1f, 1f);
        StartCoroutine(WaitToFade(sceneName));
    }

    private IEnumerator WaitToFade(string name)
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(name);
    }

}
