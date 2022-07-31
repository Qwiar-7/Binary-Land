using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] heros;
    public GameObject[] enemies;
    public GameObject[] webTraps;
    public static GameController instance;
    public int countdownTime = 900;
    public GameObject timer;
    public GameObject score;
    public GameObject enemyDropPrefab;
    public GameObject winPanel;
    public GameObject losePanel;
    Text timerText;
    Text scoreText;
    int currentScore = 0;

    private void Awake()
    {
        instance = this;
        timerText = timer.GetComponent<Text>();
        scoreText = score.GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine("CountdownToLose");
    }

    IEnumerator CountdownToLose()
    {
        while (countdownTime > 0)
        {
            timerText.text = "Time: " + countdownTime.ToString();

            yield return new WaitForSeconds(0.1f);

            countdownTime--;
        }

        Lose();
    }

    public void Lose()
    {
        StopCoroutine("CountdownToLose");
        currentScore = 0;
        scoreText.text = "Score: " + currentScore.ToString();
        foreach (var hero in heros)
        {
            hero.GetComponent<Hero>().isStuck = true;
        }
        losePanel.SetActive(true);
    }
    public void Win()
    {
        StopCoroutine("CountdownToLose");
        AddScore(countdownTime);
        winPanel.SetActive(true);
        foreach (var hero in heros)
        {
            hero.GetComponent<Hero>().isStuck = true;
        }
    }

    public void AddScore(int receivedScore)
    {
        if (scoreText != null)
        {
            currentScore += receivedScore;
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void CheckWeb()
    {
        int i = 0;
        foreach (var web in webTraps)
        {
            if (web.GetComponent<Web>().catchedHero != null)
                i++;
        }
        if (i >= 2)
            Lose();
    }

    public void SpawnEnemyDrop()
    {
        foreach (var web in webTraps)
        {
            if (!web.activeInHierarchy)
            {
                GameObject drop = Instantiate(enemyDropPrefab);
                drop.transform.position = web.transform.position;
                return;
            }
        }
    }
}
