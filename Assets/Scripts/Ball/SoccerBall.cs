using UnityEngine;

public class SoccerBall : Ball
{
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            gameManager.AddPoints(1);
            AudioSource.PlayClipAtPoint(playerAudioClip, collision.transform.position);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Gate" && !hasEntered)
        {
            hasEntered = true;
            gameManager.RemovePoints(1);
            AudioSource.PlayClipAtPoint(goalAudioClip, collision.transform.position);
            gameObject.SetActive(false);
        }
    }
}
