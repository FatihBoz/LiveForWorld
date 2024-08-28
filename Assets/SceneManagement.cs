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
        StartCoroutine(Delay(2f));
    }

    private void Scene_OnPlayerDeath()
    {
        PlayerDeadScreen.SetActive(true);
        StartCoroutine(Delay(2f));        
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
        ChangeScene();
    }
}
