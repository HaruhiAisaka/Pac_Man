using UnityEngine;
using System.Collections;


// Class is responcible for playing speicifc animations at specific times of the game.
public class CutsceenController : MonoBehaviour
{
    [Header("Other Game Objects")]
    public GameState gameState;
    public CharacterController characterController;
    public Map map;
    public UIManager uIManager;

    [Header("Intro Animation Settings")]
    public float firstDelay, secondDelay;

    [Header("Death Options")]
    public float freezeSecondsBeforeDeathAnimation;

    [Header("Eaten Animation Options")]
    public float timeOfPauseWhenEaten;
    public float delayTillNextLevel;

    public enum Animations {INTRO, RESET, DEATH, LEVELCOMPLETE, EATENGHOST};

    private Coroutine currentCoroutine;

    public bool inGameStoppingAnimation {get; private set;}
    

    public IEnumerator PlayIntroCoroutine()
    {
        inGameStoppingAnimation = true;
        uIManager.UpdateLives(gameState.lives + 1);
        characterController.animationController.FreezeAllAnimations();
        characterController.characterMovements.FreezeAllCharacters();
        characterController.animationController.MakeAllCharactersInvisible();
        uIManager.ShowPlayer1Text();
        uIManager.ShowReadyText();
        yield return new WaitForSeconds(firstDelay);
        uIManager.HidePlayer1Text();
        characterController.animationController.MakeAllCharactersVisible();
        uIManager.UpdateLives(gameState.lives);
        yield return new WaitForSeconds(secondDelay);
        uIManager.HideReadyText();
        characterController.animationController.playerAnimation.PlayMovementAnimation();
        characterController.characterMovements.UnfreezeAllCharacters();
        characterController.animationController.UnfreezeAllAnimations();
        inGameStoppingAnimation = false;
    }

    public IEnumerator PlayResetCoroutine()
    {
        inGameStoppingAnimation = true;
        uIManager.UpdateLives(gameState.lives + 1);
        characterController.animationController.FreezeAllAnimations();
        characterController.characterMovements.FreezeAllCharacters();
        uIManager.ShowReadyText();
        characterController.animationController.MakeAllCharactersVisible();
        characterController.animationController.playerAnimation.PlayCircle();
        uIManager.UpdateLives(gameState.lives);
        yield return new WaitForSeconds(secondDelay);
        uIManager.HideReadyText();
        characterController.animationController.playerAnimation.PlayMovementAnimation();
        characterController.characterMovements.UnfreezeAllCharacters();
        characterController.animationController.UnfreezeAllAnimations();
        inGameStoppingAnimation = false;
    }

    public IEnumerator PlayDeathCoroutine()
    {
        inGameStoppingAnimation = true;
        characterController.animationController.FreezeAllAnimations();
        characterController.characterMovements.FreezeAllCharacters();
        yield return new WaitForSeconds(freezeSecondsBeforeDeathAnimation);
        characterController.animationController.MakeAllCharactersInvisible();
        characterController.animationController.playerAnimation.MakeCharacterVisible();
        characterController.animationController.playerAnimation.UnfreezeAnimation();
        characterController.animationController.playerAnimation.PlayDeath();
        while (characterController.animationController.playerAnimation.playingDeathAnimation) 
        {
            yield return null;
        }
        inGameStoppingAnimation = false;
    }

    public IEnumerator PlayLevelCompleteCoroutine()
    {
        inGameStoppingAnimation = true;
        characterController.characterMovements.FreezeAllCharacters();
        characterController.animationController.playerAnimation.PlayCircle();
        characterController.animationController.FreezeAllAnimations();
        yield return new WaitForSeconds(freezeSecondsBeforeDeathAnimation);
        characterController.animationController.MakeAllCharactersInvisible();
        characterController.animationController.playerAnimation.MakeCharacterVisible();
        map.PlayStageClear();
        yield return new WaitForSeconds(delayTillNextLevel);
        map.StopStageClearAnimation();
        inGameStoppingAnimation = false;
    }

    public IEnumerator PlayGhostEatenCoroutine(Ghost ghostEaten, int score)
    {
        characterController.characterMovements.FreezeAllCharacters();
        characterController.animationController.playerAnimation.FreezeAnimation();
        characterController.animationController.playerAnimation.MakeCharacterInvisible();
        ghostEaten.ghostAnimation.MakeCharacterInvisible();
        ghostEaten.ghostAnimation.scoreShower.enabled = true;
        ghostEaten.ghostAnimation.ShowSpriteForScore(score);
        yield return new WaitForSeconds(timeOfPauseWhenEaten);
        ghostEaten.ghostAnimation.scoreShower.enabled = false;
        characterController.animationController.playerAnimation.MakeCharacterVisible();
        ghostEaten.ghostAnimation.MakeCharacterVisible();
        characterController.animationController.playerAnimation.UnfreezeAnimation();
        characterController.characterMovements.UnfreezeAllCharacters();
        ghostEaten.ghostAnimation.PlayEaten();
    }
}