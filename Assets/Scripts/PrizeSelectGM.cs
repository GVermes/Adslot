using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizeSelectGM : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Button shuffleButton;
    [SerializeField] GameObject firstText;
    [SerializeField] GameObject secondText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shuffle;
    [SerializeField][Range(0f, 1f)] float shuffleVolume = 1f;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioClip end;
    [SerializeField][Range(0f, 1f)] float endVolume = 1f;
    [SerializeField] ParticleSystem vfx;

    private int selected = 0;
    private Color green = new Color32(53, 195, 79, 255);
    private Color yellow = new Color32(255, 251, 47, 255);
    private Color blue = new Color32(0, 109, 176, 255);
    private Color red = new Color32(251, 21, 29, 255);
    private Color pink = new Color32(238, 35, 255, 255);
    private bool played = false;

    void Start()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnSelect(button));
        }

        shuffleButton.onClick.AddListener(() => OnSelect(shuffleButton));

    }

    void OnSelect(Button clickedButton)
    {
        if (selected == 0)
        {
            firstText.SetActive(false);
            secondText.SetActive(true);

            //first button clicked disable
            clickedButton.interactable = false;
            clickedButton.GetComponent<Image>().color = red;

            selected += 1;
        }
        else if (selected == 1)
        {
            //second button clicked duplicated
            foreach (var button in buttons)
            {
                if (button.interactable == false && !button.GetComponentInChildren<TextMeshProUGUI>().text.Contains("Mystery"))
                {
                    var text = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
                    clickedButton.GetComponent<Image>().color = green;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = text;
                    button.GetComponent<Image>().color = green;
                    button.interactable = true;
                }
            }

            secondText.SetActive(false);
            shuffleButton.interactable = true;

            selected += 1;
        }
        if (clickedButton.name.Equals("Shuffle"))
        {
            audioSource.PlayOneShot(shuffle, shuffleVolume);
            shuffleButton.gameObject.SetActive(false);

            List<string> labels = new List<string>();

            foreach (var button in buttons)
            {
                labels.Add(button.GetComponentInChildren<TextMeshProUGUI>().text);
            }

            //re-color buttons
            buttons[0].GetComponent<Image>().color = red;
            buttons[1].GetComponent<Image>().color = blue;
            buttons[2].GetComponent<Image>().color = green;
            buttons[3].GetComponent<Image>().color = yellow;
            buttons[4].GetComponent<Image>().color = pink;
            buttons[5].GetComponent<Image>().color = Color.white;

            var shuffledLabels = labels.OrderBy(a => Random.Range(0, 100)).ToList();

            for (int i = 0; i < shuffledLabels.Count; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = shuffledLabels[i];
                //disable texts
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }

            foreach (var button in buttons)
            {
                button.interactable = true;
                button.GetComponentInChildren<AudioSource>().clip = end;
            }

            selected += 1;
        }
        if (selected == 3 && buttons.Contains(clickedButton))
        {
            clickedButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            clickedButton.GetComponent<Transform>().localScale = new Vector3(3, 8, 3);
            vfx.gameObject.SetActive(true);
            vfx.Play();

            selected += 1;
        }
        if (selected == 4 && buttons.Any(b => b.GetComponentInChildren<TextMeshProUGUI>().enabled == true))
        {
            StartCoroutine(EndSFX());
        }
    }

    IEnumerator EndSFX()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        yield return new WaitForSeconds(13f);

        foreach (var button in buttons)
        {
            button.GetComponentInChildren<AudioSource>().clip = null;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
