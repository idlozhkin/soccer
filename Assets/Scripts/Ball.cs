using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private AudioClip catchAudioClip;
    [SerializeField] private AudioClip goalAudioClip;
    [SerializeField] private AudioClip coinAudioClip;
    private GameManager gameManager;
    private bool hasEntered;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            if (tag == "Ball")
            {
                gameManager.AddPoints(1);
                AudioSource.PlayClipAtPoint(catchAudioClip, collision.transform.position);
            }
            else if (tag == "Coin")
            {
                gameManager.AddCoins(1);
                AudioSource.PlayClipAtPoint(coinAudioClip, collision.transform.position);
            }
            else if (tag == "Bomb")
            {
                gameManager.RemovePoints(1);
                AudioSource.PlayClipAtPoint(catchAudioClip, collision.transform.position);
            }
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "Gate" && !hasEntered)
        {
            hasEntered = true;
            if (tag == "Ball")
            {
                gameManager.RemovePoints(1);
                AudioSource.PlayClipAtPoint(goalAudioClip, collision.transform.position);
            }
            if (tag == "Coin")
            {
                AudioSource.PlayClipAtPoint(goalAudioClip, collision.transform.position);
            }
            Destroy(gameObject);
        }
    }
}
