using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CoroutinesStates : MonoBehaviour
{
    #region Coroutines PlayerStateMachine
    public IEnumerator FallCoroutine(PlayerStateMachine playerState)
    {
        //throw new NotImplementedException();
        yield return new WaitForSeconds(playerState.FallWait);
        playerState.TransitionToState(PlayerStateMachine.PlayerState.FALL);
    }
    #endregion
}
