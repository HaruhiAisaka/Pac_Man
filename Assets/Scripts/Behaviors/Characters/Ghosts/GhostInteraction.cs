using UnityEngine;
using System.Collections;

public class GhostInteraction : MonoBehaviour, IEdible
{
    public bool currentlyEdible {get; set;}
    static int score;
    public Ghosts ghosts;
    public Ghost ghost;

    public int EatenScore()
    {
        ghosts.UpdateGhostEatenCounter();
        return ghosts.ScoreForEatingGhost();
    }

    [ContextMenu("EatenInteraction")]
    public void EatenAction()
    {
        ghosts.mainController.GhostEaten(ghost);
        ghost.currentMode = Ghost.Mode.EATEN;
        currentlyEdible = false;
    }
}