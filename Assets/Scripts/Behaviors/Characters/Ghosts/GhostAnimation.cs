using UnityEngine;
using System.Collections;

public class GhostAnimation : CharacterAnimation
{
    private Ghost ghost;
    public Sprite[] ghostScoreSprites;
    public SpriteRenderer scoreShower;

    protected void Awake()
    {
        scoreShower.enabled = false;
        ghost = GetComponent<Ghost>();
    }

    public void PlayChaseOrScatter() 
    {
        animator.SetTrigger("CHASE_SCATTER");
    }

    public void PlayFrightened()
    {
        animator.SetTrigger("FRIGHTENED");
    }

    public void PlayFrightenedNearEndAnimation()
    {
        animator.SetTrigger("FRIGHTENED_NEAREND");
    }

    public void PlayEaten()
    {
        animator.SetTrigger("EATEN");
    }

    public void ShowSpriteForScore(int score)
    {
        switch (score) 
        {
            case 200:
                scoreShower.sprite = ghostScoreSprites[0];
                break;
            
            case 400:
                scoreShower.sprite = ghostScoreSprites[1];
                break;

            case 800: 
                scoreShower.sprite = ghostScoreSprites[2];
                break;
            
            case 1600:
                scoreShower.sprite = ghostScoreSprites[3];
                break;

            default:
                scoreShower.sprite = ghostScoreSprites[0];
                break;
        }
    }
}