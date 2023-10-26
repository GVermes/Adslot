using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    [SerializeField] AudioClip explosion;
    [SerializeField][Range(0f, 1f)] float explosionVolume = 1f;
    [SerializeField] ParticleSystem explisionVFX;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("bullet"))
        {
            ShakeCamera();
            PlayVFX();
            AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position, explosionVolume);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void PlayVFX()
    {
        ParticleSystem instance = Instantiate(explisionVFX, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
