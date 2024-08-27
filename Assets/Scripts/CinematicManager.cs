using UnityEngine.Video;
using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject[] Panels;

    void Start()
    {
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        int playerCounter = -1;
        VideoPlayer currentPlayer;


        while(playerCounter < Panels.Length )
        {
            text.gameObject.SetActive(false);
            playerCounter++;
            GameObject rawImage = Panels[playerCounter].transform.GetChild(0).gameObject;
            currentPlayer = rawImage.GetComponent<VideoPlayer>();
            currentPlayer.Play();
            Image kapatici = Panels[playerCounter].transform.GetChild(1).GetComponent<Image>();


            StartCoroutine(Delay(1f,playerCounter,kapatici));

            yield return new WaitForSeconds(4.5f);

            kapatici.DOFade(1f, 0.75f);
            yield return new WaitForSeconds(1f);
            Panels[playerCounter].SetActive(false);
    
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

    
}