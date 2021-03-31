using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public CharacterMovement[] characterMovements;

    public void FreezeAllCharacters()
    {

        foreach (CharacterMovement characterMovement in characterMovements)
        {
            characterMovement.FreezeCharacter();
        }
    }

    public void UnfreezeAllCharacters()
    {
        foreach (CharacterMovement characterMovement in characterMovements)
        {
            characterMovement.UnfreezeCharacter();
        }
    }
}