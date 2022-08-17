using UnityEngine;

public class CoinBall : Ball
{
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            gameManager.AddCoins(1);
            gameManager.AddPoints(1);
            AudioSource.PlayClipAtPoint(playerAudioClip, collision.transform.position);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Gate" && !hasEntered)
        {
            hasEntered = true;
            AudioSource.PlayClipAtPoint(goalAudioClip, collision.transform.position);
            gameObject.SetActive(false);
        }
    }
}
