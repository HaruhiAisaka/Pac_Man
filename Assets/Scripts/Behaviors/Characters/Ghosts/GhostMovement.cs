using UnityEngine;
using System.Collections;
public abstract class GhostMovement : CharacterMovement
{
    [Header("OtherGameObjects")]
    public Player pacMan;
    public Ghost ghost;
    protected Vector2 target;

    [Header("Movement Options")]
    protected static float normalSpeedFactor = 0.75f;
    protected static float frightenedSpeedFactor = 0.5f;

    protected static float eatenSpeedFactor = 1f;

    [Header("Standby Vars")]
    public Vector2 specificGhostHouseLocationForGhost;
    public static float standbySpeed = 5.5f;

    [Header("Scatter Vars")]
    public Vector2 scatterPos;

    [Header("GhostHouseVars")]
    public bool inGhostHouse;
    protected bool exitingGhostHouse = false;
    protected bool enteringGhostHouse = false;
    protected static Vector2 ghostHouseCenter = new Vector2(0f, 0.25f);
    protected static Vector2 ghostHouseExit = new Vector2(0f, 1.75f);
    protected static Tile ghostHouseExitTile1;
    protected static Tile ghostHouseExitTile2;

    protected Vector2[] FOUR_CARDINAL_DIRECTIONS = 
        new Vector2[] {Vector2.up, Vector2.left, Vector2.down, Vector2.right};

    protected override void Awake()
    {
        base.Awake();
        ghost = GetComponent<Ghost>();
        UpdateTarget();
    }

    public static void SetGhostHouseExitTiles(Tile tile1, Tile tile2)
    {
        ghostHouseExitTile1 = tile1;
        ghostHouseExitTile2 = tile2;
    }

    protected override void FixedUpdate()
    {
        if (ghost.standBy && !freezeMovement)
        {
            StandByMovement();
            UpdateAnimation();
        }
        else if (ghost.currentMode == Ghost.Mode.EATEN && !freezeMovement 
        && (NearGhostHouseExit() || inGhostHouse) && !AtSpecificCorner())
        {
            EnterGhostHouseMovement();
            UpdateAnimation();
        }
        else if (ghost.currentMode == Ghost.Mode.EATEN && AtSpecificCorner() && !freezeMovement)
        {
            ghost.currentMode = ghost.ChaseOrScatter();
            ExitGhostHouseMovement();
            ghost.ghostAnimation.PlayChaseOrScatter();
            UpdateAnimation();
        }
        else if (ghost.currentMode != Ghost.Mode.EATEN && inGhostHouse && !freezeMovement)
        {
            ExitGhostHouseMovement();
            UpdateAnimation();
        }
        else {
            UpdateTarget();
            base.FixedUpdate();
        }
    }

    public void ReverseDirection()
    {
        currentMovementDirection *= -1;
        if (CanMoveInDirection(currentMovementDirection))
        {
            MoveCharacterBasedOnCurrentState();
        }
        else
        {
            UpdateCurrentMovementDirection();
        }
    }

    protected void UpdateTarget() 
    {
        if (ghost.currentMode == Ghost.Mode.CHASE)
        {
            UpdateTargetChase();
        }
        else if (ghost.currentMode == Ghost.Mode.SCATTER)
        {
            UpdateTargetScatter();
        }
        else if (ghost.currentMode == Ghost.Mode.EATEN)
        {
            target = ghostHouseExit;
        }
    }

    protected abstract void UpdateTargetChase();

    protected void UpdateTargetScatter()
    {
        target = scatterPos;
    }

    protected override bool UpdateCurrentMovementDirection()
    {
        bool update = !transformLerpRunning;
        if (currentTile is WarpTile)
        {
            return update;
        }
        if (update)
        {
            Vector2 choosenDirection = currentMovementDirection;
            if (ghost.currentMode == Ghost.Mode.FRIGHTENED && !inGhostHouse)
            {
                int index = RNG.Next(FOUR_CARDINAL_DIRECTIONS.Length);
                for (int i = 0; i < FOUR_CARDINAL_DIRECTIONS.Length; i++)
                {
                    Vector2 direction = FOUR_CARDINAL_DIRECTIONS[index];
                    if (CanMoveInDirection(direction) 
                    && !OppositeVectors(currentMovementDirection, direction))
                    {
                        choosenDirection = direction;
                        break;
                    }
                    index ++;
                    index = index % FOUR_CARDINAL_DIRECTIONS.Length;
                }
            }

            else {
                float minDistanceToTarget = float.MaxValue;
                foreach (Vector2 direction in FOUR_CARDINAL_DIRECTIONS)
                {
                    if (CanMoveInDirection(direction) 
                    && !OppositeVectors(currentMovementDirection, direction))
                    {
                        Tile nextPossibleTile = NextTile(direction);
                        float distance = Vector2.Distance(
                            nextPossibleTile.transform.position, 
                            target);
                        if (distance < minDistanceToTarget)
                        {
                            minDistanceToTarget = distance;
                            choosenDirection = direction;
                        }
                    }
                }    
            }
            currentMovementDirection = choosenDirection;
        }
        return update;
    }

    protected override void UpdateSpeed()
    {
        if (ghost.currentMode == Ghost.Mode.FRIGHTENED)
        {
            speed = CalculateSpeed(frightenedSpeedFactor);
        }
        else if (ghost.currentMode == Ghost.Mode.EATEN)
        {
            speed = CalculateSpeed(eatenSpeedFactor);   
        }
        else
        {
            speed = CalculateSpeed(normalSpeedFactor);
        }
    }


    public bool AtCenterOfGhostHouse()
    {
        return this.transform.position.Equals(ghostHouseCenter);
    }

    public bool AtGhostHouseExit()
    {
        return this.transform.position.Equals(ghostHouseExit);
    }

    public bool NearGhostHouseExit()
    {
        bool correctY = this.transform.position.y == ghostHouseExit.y;
        bool correctX = this.transform.position.x <= ghostHouseExitTile2.transform.position.x 
        && this.transform.position.x >= ghostHouseExitTile1.transform.position.x;
        return correctX && correctY;
    }

    public bool AtGhostHouseCenter()
    {
        return this.transform.position.Equals(ghostHouseCenter);
    }

    public bool AtSpecificCorner()
    {
        return this.transform.position.Equals(specificGhostHouseLocationForGhost);
    }
    
    protected void StandByMovement()
    {
        if (!transformLerpRunning)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            Vector2 upperY = new Vector2(
                this.transform.position.x, 
                specificGhostHouseLocationForGhost.y + Tile.tileSize * .5f);
            
            Vector2 lowerY = new Vector2(
                this.transform.position.x, 
                specificGhostHouseLocationForGhost.y - Tile.tileSize * .5f);
            if (this.transform.position.y == upperY.y) 
            {
                currentMovementDirection = new Vector2(0,-1);
                currentCoroutine = StartCoroutine(
                    TransformOverTime(
                        this.transform,
                        lowerY,
                        TimeOfCouroutine(this.transform.position, lowerY, standbySpeed)
                    ));
            }
            else
            {
                currentMovementDirection = new Vector2(0,1);
                currentCoroutine = StartCoroutine(
                    TransformOverTime(
                        this.transform,
                        upperY,
                        TimeOfCouroutine(this.transform.position, upperY, standbySpeed)
                    ));
            }
            
        }
    }
    
    protected void ExitGhostHouseMovement()
    {
        if (!transformLerpRunning)
        {
            if (this.transform.position.x == specificGhostHouseLocationForGhost.x 
            && this.transform.position.y != ghostHouseCenter.y)
            {
                currentMovementDirection = GetDirection(this.transform.position, specificGhostHouseLocationForGhost);
                currentCoroutine = StartCoroutine(
                    TransformOverTime(
                        this.transform,
                        specificGhostHouseLocationForGhost,
                        TimeOfCouroutine(this.transform.position, specificGhostHouseLocationForGhost, standbySpeed)
                    ));
            }
            else if (this.transform.position.y == ghostHouseCenter.y && !this.transform.position.Equals(ghostHouseCenter))
            {
                currentMovementDirection = GetDirection(this.transform.position, ghostHouseCenter);
                currentCoroutine = StartCoroutine(
                    TransformOverTime(
                        this.transform, 
                        ghostHouseCenter, 
                        TimeOfCouroutine(this.transform.position, ghostHouseCenter, standbySpeed)
                    ));
            }
            else
            {   
                currentMovementDirection = GetDirection(this.transform.position, ghostHouseExit);
                currentCoroutine = StartCoroutine(
                    TransformOverTime(
                        this.transform, 
                        ghostHouseExit, 
                        TimeOfCouroutine(this.transform.position, ghostHouseExit, standbySpeed)
                    ));
                inGhostHouse = false;
            }
        } 
    }

    protected void EnterGhostHouseMovement()
    {
        if (!transformLerpRunning)
        {
            if (AtGhostHouseExit())
            {
                currentMovementDirection = GetDirection(this.transform.position, ghostHouseCenter);
                StartCoroutine(
                    TransformOverTime(
                            this.transform,
                            ghostHouseCenter,
                            TimeOfCouroutine(this.transform.position, ghostHouseCenter, standbySpeed)
                        ));
                inGhostHouse = true;
            }
            else if (NearGhostHouseExit()) {
                currentMovementDirection = GetDirection(this.transform.position, ghostHouseExit);
                StartCoroutine(
                    TransformOverTime(
                            this.transform,
                            ghostHouseExit,
                            TimeOfCouroutine(this.transform.position, ghostHouseExit, standbySpeed)
                        ));
            }
            else if (AtCenterOfGhostHouse() && !specificGhostHouseLocationForGhost.Equals(ghostHouseCenter))
            {
                currentMovementDirection = GetDirection(this.transform.position, specificGhostHouseLocationForGhost);
                StartCoroutine(
                    TransformOverTime(
                        this.transform,
                        specificGhostHouseLocationForGhost,
                        TimeOfCouroutine(this.transform.position, specificGhostHouseLocationForGhost, standbySpeed)
                    )
                );
            }
        }
    }
}