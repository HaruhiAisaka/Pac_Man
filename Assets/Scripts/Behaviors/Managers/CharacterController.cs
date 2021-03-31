using UnityEngine;

public class CharacterController : MonoBehaviour 
{
    public Ghosts ghosts;
    public Player player;

    public CharacterMovements characterMovements;
    public AnimationController animationController;

    public void ResetCharacters()
    {
        ghosts.ResetAllGhosts();
        player.Reset();
    }
}