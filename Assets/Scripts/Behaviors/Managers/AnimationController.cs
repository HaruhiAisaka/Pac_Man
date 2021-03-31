using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public CharacterAnimation[] characterAnimations;

    public PlayerAnimation playerAnimation;

    public SuperPellet[] superPellets;

    public void ReplaceSuperPellets(SuperPellet[] superPellets)
    {
        this.superPellets = superPellets;
    }

    public void MakeAllCharactersInvisible()
    {
        foreach (CharacterAnimation characterAnimation in characterAnimations)
        {
            characterAnimation.MakeCharacterInvisible();
        }
    }

    public void MakeAllCharactersVisible()
    {
        foreach (CharacterAnimation  characterAnimation in characterAnimations)
        {
            characterAnimation.MakeCharacterVisible();
        }
    }

    public void FreezeAllAnimations()
    {
        foreach (CharacterAnimation  characterAnimation in characterAnimations)
        {
            characterAnimation.FreezeAnimation();
        }

        foreach (SuperPellet superPellet in superPellets)
        {
            superPellet.SetPelletStill();
        }
    }

    public void UnfreezeAllAnimations()
    {
        foreach (CharacterAnimation  characterAnimation in characterAnimations)
        {
            characterAnimation.UnfreezeAnimation();
        }

        foreach (SuperPellet superPellet in superPellets)
        {
            superPellet.SetPelletToFlicker();
        }
    }
}