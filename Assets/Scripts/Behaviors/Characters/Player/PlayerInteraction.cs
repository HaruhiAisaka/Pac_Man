using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public MainController mainController;
    public bool eating;
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var edibleObjectCollided = otherCollider.GetComponent<IEdible>();
        var ghostCollided = otherCollider.GetComponent<Ghost>();
        if (edibleObjectCollided != null && edibleObjectCollided.currentlyEdible)
        {
            int score = edibleObjectCollided.EatenScore();
            mainController.gameState.AddScore(score);
            eating = true;
            edibleObjectCollided.EatenAction();
        }
        else if (ghostCollided != null && 
        (ghostCollided.currentMode == Ghost.Mode.CHASE || ghostCollided.currentMode == Ghost.Mode.SCATTER))
        {
            mainController.PlayerDeath();
        }
    }
}