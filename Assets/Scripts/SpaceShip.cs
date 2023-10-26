using System.Collections;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform reload;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] AudioClip shoot;
    [SerializeField][Range(0f, 1f)] float shootVolume = 1f;

    private Transform ship;
    private bool turn = false;

    private void Start()
    {
        ship = gameObject.GetComponent<Transform>();
        StartCoroutine(Shoot());
    }
    void Update()
    {
        if (ship.position.x <= maxX && !turn)
        {
            ship.transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (ship.position.x >= maxX)
            {
                turn = true;
            }
        }
        else if (ship.position.x >= minX && turn)
        {
            ship.transform.Translate(Vector3.right * -speed * Time.deltaTime);

            if (ship.position.x <= minX)
            {
                turn = false;
            }
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));
            Instantiate(bullet, reload.position, reload.rotation, transform);
            AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(Random.Range(1f, 10f));
        }
    }
}
