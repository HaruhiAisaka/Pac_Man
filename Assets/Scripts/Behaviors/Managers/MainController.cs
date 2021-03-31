using UnityEngine;
using System.Collections;


// Responcible for transitioning between moments of play 
// (where the player is in control of PacMan) and animation
// (where the player is not in control of pacman due to a death, transition in level, etc.)
public class MainController : MonoBehaviour
{
    private enum GameMode {Animation, Play}
    private GameMode currentMode;
    public GameState gameState;
    private Coroutine currentCoroutine;
    public CutsceenController cutsceenController;
    public CharacterController characterController;

    public Map map;

    void Start()
    {
        currentCoroutine = StartCoroutine(IntroCoroutine());
    }

    void Update()
    {
        if (currentMode == GameMode.Play)
        {
            if (map.LevelComplete())
            {
                LevelComplete();
            }
        }
    }

    public void LevelComplete()
    {
        if (currentMode == GameMode.Play)
        {
            currentCoroutine = StartCoroutine(LevelCompleteCoroutine());
        }
    }

    public void GhostEaten(Ghost eatenGhost)
    {
        if (currentMode == GameMode.Play)
        {
            currentCoroutine = StartCoroutine(GhostEatenCoroutine(eatenGhost));
        }
    }

    public void PlayerDeath()
    {
        if (currentMode == GameMode.Play) 
        {
            currentCoroutine = StartCoroutine(PlayerDeathCoroutine());
        }
    }

    private IEnumerator IntroCoroutine()
    {
        currentMode = GameMode.Animation;
        // gameState.ReplaceCurrentMap(mapSpawner.InstantiateOriginalMap());
        // Temporary
        characterController.animationController.ReplaceSuperPellets(map.edibleItems.superPellets);
        characterController.ResetCharacters();
        Coroutine a = StartCoroutine(cutsceenController.PlayIntroCoroutine());
        yield return a;
        gameState.timerOn = true;
        currentMode = GameMode.Play;
    }

    private IEnumerator LevelCompleteCoroutine()
    {
        currentMode = GameMode.Animation;
        Coroutine a = StartCoroutine(cutsceenController.PlayLevelCompleteCoroutine());
        yield return a;
        gameState.IncreaseLevel();
        characterController.ResetCharacters();
        map.ResetLevel();
        Coroutine b = StartCoroutine(cutsceenController.PlayResetCoroutine());
        yield return b;
        currentMode = GameMode.Play;
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        currentMode = GameMode.Animation;
        gameState.LifeLost();
        Coroutine a = StartCoroutine(cutsceenController.PlayDeathCoroutine());
        yield return a;
        characterController.ResetCharacters();
        Coroutine b = StartCoroutine(cutsceenController.PlayResetCoroutine());
        yield return b;
        currentMode = GameMode.Play;
    }

    private IEnumerator GhostEatenCoroutine(Ghost ghostEaten)
    {
        currentMode = GameMode.Animation;
        int score = characterController.ghosts.ScoreForEatingGhost();
        Coroutine a = StartCoroutine(cutsceenController.PlayGhostEatenCoroutine(ghostEaten, score));
        yield return a;
        currentMode = GameMode.Play;
    }
}