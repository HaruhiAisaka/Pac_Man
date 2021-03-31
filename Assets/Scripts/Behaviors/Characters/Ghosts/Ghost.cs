using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Ghosts ghosts;
    public GhostMovement ghostMovement;
    public GhostAnimation ghostAnimation;
    public GhostInteraction ghostInteraction;
    public enum Mode {CHASE, SCATTER, FRIGHTENED, EATEN}
    public bool standBy;
    public Mode currentMode;

    public int dotsNeededToBeEatenBeforeExiting;

    protected static TimeInterval[] chaseIntervals = 
        new TimeInterval[] {
            new TimeInterval(7f, 27f),
            new TimeInterval(34f, 54f),
            new TimeInterval(61f)
        };

    protected virtual void Update()
    {
        currentMode = UpdatedMode();
    }

    protected Mode UpdatedMode()
    {
        if (dotsNeededToBeEatenBeforeExiting > ghosts.mainController.gameState.dotsEaten)
        {
            standBy = true;
        }
        else
        {
            standBy = false;
        }
        if (currentMode == Mode.FRIGHTENED)
        {
            return Mode.FRIGHTENED;
        }
        else if (currentMode == Mode.EATEN)
        {
            return Mode.EATEN;
        }
        return ChaseOrScatter();
    }

    public virtual void Reset()
    {
        ghostMovement.Reset();
        standBy = true;
        ghostMovement.inGhostHouse = true;
    }

    public Mode ChaseOrScatter()
    {
        foreach (TimeInterval chaseInterval in chaseIntervals)
        {
            if (chaseInterval.InTimeInterval(ghosts.mainController.gameState.time))
            {
                return Mode.CHASE;
            }
        }
        return Mode.SCATTER;
    }

    public void SetToChaseOrScatter()
    {
        currentMode = ChaseOrScatter();
        ghostAnimation.PlayChaseOrScatter();
        ghostInteraction.currentlyEdible = false;
    }

    public void FrightenGhost()
    {
        currentMode = Mode.FRIGHTENED;
        ghostInteraction.currentlyEdible = true;
        if (!standBy) {
            ghostMovement.ReverseDirection();
        }
        ghostAnimation.PlayFrightened();
    }

    public void SetGhostToEaten()
    {
        currentMode = Mode.EATEN;
        ghostInteraction.currentlyEdible = false;
        ghostAnimation.PlayEaten();
    }

    [System.Serializable]
    public struct TimeInterval
    {
        public float startTime;
        public float endTime;
        public bool noEndTime;

        public TimeInterval(float startTime, float endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.noEndTime = false;
        }

        public TimeInterval(float startTime)
        {
            this.startTime = startTime;
            this.endTime = float.MaxValue;
            this.noEndTime = true;
        }

        public bool InTimeInterval(float time)
        {
            if (noEndTime)
            {
                return startTime <= time;
            }
            else
            {
                return startTime <= time && endTime > time;
            }
        }
    }
}

