using System.Collections;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{

    [Header("Movement Options")]
    public Vector2 initialDirection;
    public Vector2 initialPosition;

    [Header("Protected Variables")]
    public TileDictionary tileDictionary;
    protected float speed;
    protected static float baseLineSpeed = 10;
    protected Tile currentTile;
    protected CharacterAnimation characterAnimation;
    protected Vector2 currentMovementDirection;
    protected Vector2 currentViewingDirection;
    protected Coroutine currentCoroutine;
    public bool freezeMovement = false;
    protected bool transformLerpRunning = false;

    protected virtual void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        // tileDictionary = FindObjectOfType<TileDictionary>();
    }

    // Should be called when character positions need to be reset.
    public virtual void Reset()
    {
        StopAllCoroutines();
        currentMovementDirection = initialDirection;
        this.transform.position = initialPosition;
        UpdateAnimation();
    }

    protected virtual void FixedUpdate()
    {
        if (UpdateCurrentMovementDirection() && !freezeMovement)
        {
            MoveCharacterBasedOnCurrentState();
            UpdateCurrentTile();
            UpdateAnimation();
        }
        UpdateSpeed();
    }

    protected abstract bool UpdateCurrentMovementDirection();

    protected void MoveCharacterBasedOnCurrentState()
    {
        if (currentTile is WarpTile)
        {
            WarpTile theCurrentWarpTile = currentTile as WarpTile;
            if (currentMovementDirection.Equals(theCurrentWarpTile.directionToWarp))
            {
                theCurrentWarpTile.WarpObject(this.gameObject);
            }
        }
        Tile nextTile = NextTile(currentMovementDirection);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        Vector3 nextPosition = nextTile.gameObject.transform.position;
        currentCoroutine = StartCoroutine(
            TransformOverTime(
                this.transform,
                nextPosition,
                TimeOfCouroutine(this.transform.position, nextPosition, this.speed)));
    }

    protected virtual void UpdateCurrentTile()
    {
        currentTile = NextTile(currentMovementDirection);
    }

    protected void UpdateAnimation()
    {
        characterAnimation.UpdateAnimator(currentMovementDirection);
    }

    public virtual void FreezeCharacter()
    {
        freezeMovement = true;
        StopCurrentCoroutine();
        transformLerpRunning = false;
    }

    public virtual void UnfreezeCharacter()
    {
        freezeMovement = false;
    }
    protected IEnumerator TransformOverTime(Transform transform, Vector3 target, float duration)
    {
        transformLerpRunning = true;
        Vector3 initialCameraPosition = transform.position;
        float t = 0;
        while (Vector3.Distance(transform.position, target) > 0)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initialCameraPosition, target, t / duration);
            yield return null;
        }
        transformLerpRunning = false;
        yield return null;
    }

    protected bool CanMoveInDirection(Vector2 vector)
    {
        if (vector.Equals(Vector2.zero))
        {
            return false;
        }
        Tile nextTile = tileDictionary.GetNextTile(this.transform.position, vector);
        return nextTile is AccessableTile;
    }

    protected bool OppositeVectors(Vector2 vector1, Vector2 vector2)
    {
        return vector1.Equals(vector2 * -1);
    }

    protected Tile NextTile(Vector2 direction)
    {
        Tile nextTileBasedOnDirection = 
            tileDictionary.GetNextTile(this.transform.position, direction);
        return nextTileBasedOnDirection;
    }

    protected float TimeOfCouroutine(Vector2 currentPosition, Vector2 nextPosition, float speed)
    {
        float distance = Vector2.Distance(currentPosition, nextPosition);
        return (1 / speed) * (distance / Tile.tileSize);
    }

    protected Vector2 GetDirection (Vector2 start, Vector2 end)
    {
        return (end - start).normalized;
    }

    protected void StopCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
    }

    protected float CalculateSpeed(float factor)
    {
        return  baseLineSpeed * factor;
    }

    protected abstract void UpdateSpeed();

}