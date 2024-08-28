using UnityEngine.Video;
using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CinematicManager : MonoBehaviour
{
    public static Action OnCinematicFinished;
    public GameObject CinematicObject;
    public TextMeshProUGUI text;
    public GameObject[] Panels;

    private static bool startCinematic;
    void Start()
    {

        if (!startCinematic)
        {
            StartCoroutine(PlayCinematic());
            startCinematic = true;
        }

    }

    private IEnumerator PlayCinematic()
    {
        int playerCounter = -1;
        VideoPlayer currentPlayer;


        while(playerCounter < Panels.Length-1)
        {
            print(playerCounter);
            text.gameObject.SetActive(false);
            playerCounter++;
            GameObject rawImage = Panels[playerCounter].transform.GetChild(0).gameObject;
            currentPlayer = rawImage.GetComponent<VideoPlayer>();
            currentPlayer.Play();
            Image kapatici = Panels[playerCounter].transform.GetChild(1).GetComponent<Image>();


            StartCoroutine(Delay(1f,playerCounter,kapatici));

            yield return new WaitForSeconds(4.5f);

            if (!(playerCounter == Panels.Length - 1))
            {
                kapatici.DOFade(1f, 0.75f);
            }

            yield return new WaitForSeconds(1f);
            Panels[playerCounter].SetActive(false);

            if(playerCounter == Panels.Length - 1)
            {
                OnCinematicFinished?.Invoke();
            }
    
        }
        


    }

    private IEnumerator Delay(float delayAmount,int playerCounter,Image kapatici)
    {
        yield return new WaitForSeconds(delayAmount);
        kapatici.DOFade(0f, 0.75f);
        text.gameObject.SetActive(true);
        Subtitle subtitle = Panels[playerCounter].GetComponent<Subtitle>();
        text.text = subtitle.GetTitle();
    }

    private void OnEnable()
    {
        WaveSpawn.Wave5CountdownEnded += Cinematic_Wave5Ended;
    }

    private void Cinematic_Wave5Ended()
    {
        CinematicObject.SetActive(true);
        StartCoroutine(PlayCinematic());
    }

    private void OnDisable()
    {
        WaveSpawn.Wave5CountdownEnded -= Cinematic_Wave5Ended;
    }


}