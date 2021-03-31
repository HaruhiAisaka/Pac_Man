using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameState gameState;
    public NumberDisplay score, highScore, level;
    public LivesDisplay lives;
    public SpriteRenderer player1Text, readyText;

    void Update() 
    {
        UpdateScore();
        UpdateHighScore();
    }

    public void UpdateScore()
    {
        this.score.UpdateNumber(gameState.score);
    }

    public void UpdateHighScore()
    {
        this.highScore.UpdateNumber(gameState.highScore);
    }

    public void UpdateLevel()
    {
        this.level.UpdateNumber(gameState.level);
    }

    public void UpdateLives(int lives)
    {
        this.lives.UpdateLives(lives);
    }

    public void ShowPlayer1Text()
    {
        player1Text.enabled = true;
    }

    public void HidePlayer1Text()
    {
        player1Text.enabled = false;
    }

    public void ShowReadyText()
    {
        readyText.enabled = true;
    }

    public void HideReadyText()
    {
        readyText.enabled = false;
    }
}