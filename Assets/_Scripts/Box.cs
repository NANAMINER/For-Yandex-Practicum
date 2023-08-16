using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float speed = 5f;
    public float VerticalSpeed = 5f;
    public float verticalRange = 5f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
        StartCoroutine(DestroyAfterSeconds(5));
        StartCoroutine(ChangeVector());
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        transform.Translate(Vector3.up * VerticalSpeed * Time.deltaTime);
    }

    public IEnumerator ChangeVector()
    {
        yield return new WaitForSeconds(1f);
        VerticalSpeed = -VerticalSpeed;
        StartCoroutine(ChangeVector());
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

