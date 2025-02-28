using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController : MonoBehaviour, IInteractiveButton
{
    public UnityEvent onGameRestart;
    public void ButtonClick()
    {
        onGameRestart.Invoke();
    }
}
