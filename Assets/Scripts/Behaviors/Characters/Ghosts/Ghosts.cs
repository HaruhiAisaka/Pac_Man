using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public Ghost[] ghosts;
    bool nearEndAnimationPlaying = false;
    public bool ghostFrightened = false;
    float frightenedTime;
    float frightenedTimeMax;
    float nearFrightenedTime = 2f;
    int ghostsEaten = 0;

    const int GHOST_BASE_SCORE = 200;

    public MainController mainController;
    public Tile ghostHouseExitTile1;
    public Tile ghostHouseExitTile2;

    void Awake()
    {
        frightenedTimeMax = 6f;
        GhostMovement.SetGhostHouseExitTiles(ghostHouseExitTile1, ghostHouseExitTile2);
    }

    void Update()
    {
        if (ghostFrightened)
        {
            frightenedTime += Time.deltaTime;
            if (ghostsEaten > ghosts.Length)
            {
                StateOfVarsWhenNoGhostsAreFrightened();
            }
        }
        if (frightenedTime >= frightenedTimeMax - nearFrightenedTime && !nearEndAnimationPlaying && ghostFrightened)
        {
            PlayFrightenedNearEndAnimations();
        }
        else if (frightenedTime >= frightenedTimeMax && nearEndAnimationPlaying && ghostFrightened)
        {
            StateOfVarsWhenNoGhostsAreFrightened();
            SetAllBackToChaseOrScatterIfFrightened();
        }
    }

    public void ResetAllGhosts()
    {
        SetAllBackToChaseOrScatter();
        foreach (Ghost ghost in ghosts)
        {
            ghost.Reset();
        }
    }

    public void UpdateGhostEatenCounter()
    {
        ghostsEaten ++;
    }

    public int ScoreForEatingGhost()
    {
        return (int)Mathf.Pow(2, ghostsEaten - 1) * GHOST_BASE_SCORE;
    }

    public void FrightenGhosts()
    {
        StateOfVarsWhenNoGhostsAreFrightened();
        ghostFrightened = true;
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.currentMode != Ghost.Mode.FRIGHTENED)
            {
                ghost.FrightenGhost();
            }
        }
    }

    private void StateOfVarsWhenNoGhostsAreFrightened() 
    {
        ghostsEaten = 0;
        ghostFrightened = false;
        nearEndAnimationPlaying = false;
        frightenedTime = 0;
    }

    // Needed so that animations sync with all ghosts.
    public void PlayFrightenedNearEndAnimations()
    {
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.currentMode == Ghost.Mode.FRIGHTENED) 
            {
                ghost.ghostAnimation.PlayFrightenedNearEndAnimation();
            }
        }
        nearEndAnimationPlaying = true;
    }

    public void SetAllBackToChaseOrScatterIfFrightened()
    {
        StateOfVarsWhenNoGhostsAreFrightened();
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.currentMode == Ghost.Mode.FRIGHTENED)
            {
                ghost.SetToChaseOrScatter();
            }
        }
    }

    public void SetAllBackToChaseOrScatter()
    {
        StateOfVarsWhenNoGhostsAreFrightened();
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.currentMode == Ghost.Mode.FRIGHTENED || ghost.currentMode == Ghost.Mode.EATEN)
            {
                ghost.SetToChaseOrScatter();
            }
        }
    }

    public void FreezeAllGhosts()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ghostMovement.FreezeCharacter();
        }
    }

    public void UnfreezeAllGhosts()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ghostMovement.UnfreezeCharacter();
        }
    }
}