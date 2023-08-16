using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public Player player;
    public TextMeshPro ScoresText;
    public TextMeshPro StartText;
    private bool is_game_started = false;
    private int scores = 0;
    public ObstacleSpawner obstacleSpawner;
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update() 
    {
        if (is_game_started) return;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Space))
        {
            StartText.gameObject.SetActive(false);
            is_game_started = true;
            player.is_game_started = true;
            Time.timeScale = 1f;
            player.StartGame();
            StartCoroutine(DoSpawn());
        }
    }

    public IEnumerator DoSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        obstacleSpawner.StartSpawn();
    }

    // Update is called once per frame
    public void Lose()
    {
        SceneManager.LoadScene(0);
    }

    public void PlusScores()
    {
        scores++;
        ScoresText.text = scores.ToString();
    }
}
