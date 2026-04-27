using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 100;

    [Header("Score Settings")]
    public int wrongClickPenalty = 5;
    public float scoreDecreaseInterval = 1f;
    public int scoreDecreaseAmount = 1;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private float timer;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scoreDecreaseInterval)
        {
            timer = 0f;
            AddScore(-scoreDecreaseAmount);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CheckWrongClick();
        }
    }

    void CheckWrongClick()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider == null)
        {
            AddScore(-wrongClickPenalty);
            return;
        }

        if (hit.collider.GetComponent<Dot>() == null)
        {
            AddScore(-wrongClickPenalty);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score < 0)
        {
            score = 0;
        }

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "For Testing:" + score;
        }
    }
}