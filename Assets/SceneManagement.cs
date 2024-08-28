using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject PlayerDeadScreen;
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        PlayerProperties.OnPlayerDeath += Scene_OnPlayerDeath;
    }

    private void Scene_OnPlayerDeath()
    {
        PlayerDeadScreen.SetActive(true);
        StartCoroutine(Delay());
    }

    private void OnDisable()
    {
        PlayerProperties.OnPlayerDeath -= Scene_OnPlayerDeath;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        ChangeScene();
    }
}
