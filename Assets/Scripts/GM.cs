using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GM : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    [SerializeField] Button continueButton;
    [SerializeField] Button exitButton;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip tie;
    [SerializeField] AudioClip winner;
    [SerializeField][Range(0f, 1f)] float clipVolume = 0.1f;
    [SerializeField][Range(0f, 1f)] float winnerClipVolume = 1f;
    [SerializeField] ParticleSystem vfx;

    private int scoreTextSize = 40;
    private int leaderScoreTextSize = 45;
    private int conflictTextSize = 47;
    private Color yellow = new Color32(255, 251, 47, 255);
    private Color red = new Color32(251, 21, 29, 255);
    private Color winnerColor = new Color32(251, 255, 138, 255);

    void Update()
    {
        List<int> scoreNumberList = new List<int>();

        foreach (var player in players)
        {
            //scoreText to Number for each player
            var scoreText = player.GetComponentInChildren<TextMeshProUGUI>().text;
            var scoreNumber = Int16.Parse(scoreText);
            scoreNumberList.Add(scoreNumber);

            //change scoreText color and size back to normal for each player
            var text = player.GetComponentInChildren<TextMeshProUGUI>();
            text.color = Color.white;
            text.fontSize = scoreTextSize;
        }
        
        if (scoreNumberList.Max() <= 0)
        {
            continueButton.interactable = false;
        }
        else if (scoreNumberList.Max() > 0)
        {
            //find the leaders and add to list
            var leaders = players.Where(p => Int16.Parse(p.GetComponentInChildren<TextMeshProUGUI>().text) == scoreNumberList.Max()).ToList();

            foreach (var leader in leaders)
            {
                //change the leader scoreText color and size
                var text = leader.GetComponentInChildren<TextMeshProUGUI>();
                text.color = yellow;
                text.fontSize = leaderScoreTextSize;
                continueButton.interactable = true;

                //if more than 1 leaders change scoreText color and size
                if (leaders.Count > 1)
                {
                    //audioSource.PlayOneShot(tie, clipVolume);
                    continueButton.interactable = false;
                    text.color = red;
                    text.fontSize = conflictTextSize;
                }
            }
        }
    }

    public void LoadNextScene()
    {
        if (continueButton.interactable)
        {
            StartCoroutine(NextSceneAnim());
        }
    }

    IEnumerator NextSceneAnim()
    {
        audioSource.PlayOneShot(winner, winnerClipVolume);
        PlayVFX();
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void PlayVFX()
    {
        var mvp = players.Where(p => (p.GetComponentInChildren<TextMeshProUGUI>().color) == yellow).First();

        mvp.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        mvp.GetComponent<Image>().color = winnerColor;
        var textsOfWinner = mvp.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        foreach (var text in textsOfWinner)
        {
            text.color = winnerColor;
        }

        ParticleSystem instance = Instantiate(vfx, transform.position, Quaternion.identity);
        ParticleSystem instance2 = Instantiate(vfx, transform.position + new Vector3(5,0,0), Quaternion.identity);
        ParticleSystem instance3 = Instantiate(vfx, transform.position + new Vector3(-5, 0, 0), Quaternion.identity);
        ParticleSystem instance4 = Instantiate(vfx, transform.position + new Vector3(-8, 2, 0), Quaternion.identity);
        ParticleSystem instance5 = Instantiate(vfx, transform.position + new Vector3(-8, -2, 0), Quaternion.identity);
        ParticleSystem instance6 = Instantiate(vfx, transform.position + new Vector3(8, 2, 0), Quaternion.identity);
        ParticleSystem instance7 = Instantiate(vfx, transform.position + new Vector3(8, -2, 0), Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        Destroy(instance2.gameObject, instance2.main.duration + instance2.main.startLifetime.constantMax);
        Destroy(instance3.gameObject, instance3.main.duration + instance3.main.startLifetime.constantMax);
        Destroy(instance4.gameObject, instance4.main.duration + instance4.main.startLifetime.constantMax);
        Destroy(instance5.gameObject, instance5.main.duration + instance5.main.startLifetime.constantMax);
        Destroy(instance6.gameObject, instance6.main.duration + instance6.main.startLifetime.constantMax);
        Destroy(instance7.gameObject, instance7.main.duration + instance7.main.startLifetime.constantMax);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
