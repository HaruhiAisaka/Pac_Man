using UnityEngine;
using System.Collections;

public abstract class Tile : MonoBehaviour
{
    public Sprite sprite;
    public TileDictionary tileDictionary;
    public static float tileSize = 0.5f;

    public Animator animator;

    public virtual void SetUpTile(TileDictionary tileDictionary)
    {
        this.tileDictionary = tileDictionary;
        SetSprite();
    }

    protected void SetSprite()
    {
        if (sprite == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    protected void SetSprite(Sprite sprite, float rotation = 0f, bool flipX = false, bool flipY = false)
    {
        this.sprite = sprite;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = sprite;
        sp.flipX = flipX;
        sp.flipY = flipY;
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    void OnEnable()
    {
        gameObject.transform.position = RoundToNearestTilePos(gameObject.transform.position);
    }

    public static Vector2 RoundToNearestTilePos(Vector2 vector)
    {
        return new Vector2(RoundToOneFourthOrThreeFourth(vector.x), RoundToOneFourthOrThreeFourth(vector.y));
    }

    private static float RoundToOneFourthOrThreeFourth(float value)
    {
        if (value - Mathf.Floor(value) < .5f)
        {
            value = Mathf.Floor(value) + .25f;
        }
        else
        {
            value = Mathf.Floor(value) + .75f;
        }
        return value;
    }

    public void PlayStageClear()
    {
        animator.SetTrigger("STAGE_CLEAR");
    }

    public void StopStageClearAnimation()
    {
        animator.SetTrigger("STOP_STAGE_CLEAR");
    }
}