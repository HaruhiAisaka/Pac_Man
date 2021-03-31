using UnityEngine;

public class Pellet : MonoBehaviour, IEdible
{
    public Sprite sprite;
    public bool currentlyEdible {get; set;}

    public bool hasBeenEaten {get; protected set;}

    public int score {get; set;}

    public GameState gameState;

    void Start()
    {
        currentlyEdible = true;
        hasBeenEaten = false;
        score = 10;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
        gameState = FindObjectOfType<GameState>();
    }

    public virtual int EatenScore()
    {
        return score;
    }

    public virtual void EatenAction()
    {
        currentlyEdible = false;
        hasBeenEaten = true;
        GetComponent<SpriteRenderer>().enabled = false;
        gameState.AddDotsEaten();
    }

    public void Reset() 
    {
        currentlyEdible = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
        hasBeenEaten = false;
    }
}