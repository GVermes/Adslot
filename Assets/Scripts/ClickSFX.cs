using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSFX : MonoBehaviour
{
    public AudioSource SFX;

    public void PlaySFX()
    {
        SFX.Play();
    }
}
