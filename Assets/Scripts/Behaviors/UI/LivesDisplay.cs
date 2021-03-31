using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public SpriteRenderer[] liveShowers;

    public NumberDisplay numericalDisplay;


    public void UpdateLives(int lives)
    {
        foreach (var liveShower in liveShowers)
        {
            liveShower.enabled = false;
        }

        if (lives <= liveShowers.Length)
        {
            for (int i = 0; i < lives; i++)
            {
                liveShowers[i].enabled = true;
            }
        }
    }
}