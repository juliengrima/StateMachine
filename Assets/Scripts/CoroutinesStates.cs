using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoroutinesStates : MonoBehaviour
{
    #region Coroutines PlayerStateMachine
    public IEnumerator FallCoroutine(PlayerStateMachine playerState)
    {
        //throw new NotImplementedException();
        yield return new WaitForSeconds(playerState.FallWait);
        playerState.TransitionToState(PlayerStateMachine.PlayerState.FALL);
    }

   public IEnumerator CheckPlayer(float wait, Light light, Color reset)
    {
        // Changez la couleur du spotlight
        light.color = reset; // Par exemple, changez la couleur en rouge
        yield return new WaitForSeconds(wait);
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    #endregion
}
