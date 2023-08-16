using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public bool is_game_started = false;
    public float speed = 10f;
    public float smooth = 1f;
    private float RealSpeed;
    public int length = 10;
    public GameObject pointPrefab;
    private List<Vector3> positions = new List<Vector3>();
    private GameObject[] points;

    public void StartGame()
    {
        RealSpeed = -speed;

        points = new GameObject[length];
        StartCoroutine(SpawnPoints());
    }

    IEnumerator SpawnPoints()
    {
        for (int i = 0; i < length; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.03f);
        }
    }
    void FixedUpdate()
    {
        if (!is_game_started) return;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetKey(KeyCode.Space))
        {
            if(RealSpeed < speed)
            {
                RealSpeed += Time.deltaTime * smooth;
            }
        }
        else
        {
            if (RealSpeed > -speed)
            {
                RealSpeed -= Time.deltaTime * smooth;
            }
        }
        transform.Translate(Vector3.up * RealSpeed * Time.deltaTime);
        Debug.Log(RealSpeed);

        positions.Insert(0, transform.position);
        if (positions.Count > length)
        {
            positions.RemoveAt(positions.Count - 1);
        }

        // обновление позиций остальных точек
        for (int i = 0; i < points.Length; i++)
        {
            if (i < positions.Count)
            {
                if (points[i] != null)
                {
                    points[i].transform.position = positions[i] + new Vector3(i * -0.25f, 0, 0);
                    float size = Mathf.Lerp(0.6f, 0.1f, (float)i / (points.Length - 1));
                    points[i].transform.localScale = new Vector3(size, size, size);
                }
            }

            SpriteRenderer spriteRenderer;
            if (points[i] != null)
            {
                points[i].TryGetComponent<SpriteRenderer>(out spriteRenderer);
                if (spriteRenderer != null)
                {
                    float alpha = Mathf.Lerp(1f, 0.1f, (float)i / (points.Length - 1));
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            game.Lose();
        }else if (collision.tag == "Score")
        {
            Destroy(collision.gameObject);
            game.PlusScores();
        }
    }
}
