using UnityEngine;

public class SuperPellet : Pellet
{
    public Ghosts ghosts;
    public Animator animator;
    void Start()
    {
        currentlyEdible = true;
        hasBeenEaten = false;
        score = 50;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
        ghosts = FindObjectOfType<Ghosts>();
        gameState = FindObjectOfType<GameState>();
    }

    public override int EatenScore()
    {
        return score;
    }

    public override void EatenAction()
    {
        base.EatenAction();
        GetComponent<Animator>().enabled = false;
        ChangeGhostsToEdible();
    }
    
    private void ChangeGhostsToEdible()
    {
        ghosts.FrightenGhosts();
    }

    public void SetPelletToFlicker()
    {
        animator.SetBool("FLICKER", true);
    }

    public void SetPelletStill()
    {
        animator.SetBool("FLICKER", false);
    }
}