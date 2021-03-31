using UnityEngine;

// Responcible for storing data reguarding the current state of the game.
public class GameState : MonoBehaviour
{
    public int score {get; private set;}
    public int highScore {get; private set;}
    public int lives {get; private set;}
    public int dotsEaten {get; private set;}

    public int baseLineSpeed;
    public int level {get; private set;}

    public float time;
    public bool timerOn = false;

    private const int initialLives = 3;

    void Awake()
    {
        score = 0;
        highScore = 0;
        time = 0;
        dotsEaten = 0;
        lives = initialLives;
    }

    void Update()
    {
        if (timerOn) time += Time.deltaTime;
    }

    public void AddScore(int score)
    {
        this.score += score;
        UpdateHighScore();
    }

    public void ResetScore() 
    {
        score = 0;
    }

    void UpdateHighScore()
    {
        if (score > highScore)
        {
            this.highScore = score;
        }
    }

    public void ResetTimer() 
    {
        time = 0;
        timerOn = false;
    }

    public void ResetDotsEaten() 
    {
        dotsEaten = 0;
    }

    public void AddDotsEaten() 
    {
        dotsEaten ++;
    }

    public void LifeLost() 
    {
        lives --;
    }

    public void IncreaseLevel()
    {
        level ++;
    }
}