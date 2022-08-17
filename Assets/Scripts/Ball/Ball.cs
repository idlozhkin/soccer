using UnityEngine;
using Zenject;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected AudioClip goalAudioClip;
    [SerializeField] protected AudioClip playerAudioClip;
    [Inject] protected GameManager gameManager;
    protected bool hasEntered;

    protected void OnEnable()
    {
        hasEntered = false;
    }
}
