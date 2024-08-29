using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject SpaceshipCollapsedScreen;
    public GameObject PlayerDeadScreen;
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        PlayerProperties.OnPlayerDeath += Scene_OnPlayerDeath;
        Main.OnSpaceshipCollapsed += Scene_OnSpaceShipCollapsed;
        CinematicManager.OnEndingCinematicFinished += Scene_OnEndingCinematicFinished;
    }

    private void Scene_OnSpaceShipCollapsed()
    {
        SpaceshipCollapsedScreen.SetActive(true);
        CollapseAudioSources();
        StartCoroutine(Delay(2f));
        ChangeScene();
    }

    private void Scene_OnPlayerDeath()
    {
        PlayerDeadScreen.SetActive(true);
        CollapseAudioSources();
        
        StartCoroutine(Delay(2f));     

    }

    private void CollapseAudioSources()
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }

    private void OnDisable()
    {
        PlayerProperties.OnPlayerDeath -= Scene_OnPlayerDeath;
        Main.OnSpaceshipCollapsed -= Scene_OnSpaceShipCollapsed;
        CinematicManager.OnEndingCinematicFinished -= Scene_OnEndingCinematicFinished;
    }

    private void Scene_OnEndingCinematicFinished()
    {
        StartCoroutine(Delay(0));
    }

    private IEnumerator Delay(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Application.Quit(); 
    }
}
