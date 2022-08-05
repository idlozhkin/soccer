using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject gate;
    [Header("Projectiles")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject coinBallPrefab;
    [SerializeField] private GameObject bombPrefab;
    private AudioSource audioSource;
    private Transform[] gateCorners = new Transform[4];
    private GameManager gameManager;

    private void Start()
    {
        transform.LookAt(Vector3.zero);

        for (int i = 0; i < 4; i++)
        {
            gateCorners[i] = gate.transform.GetChild(i);
        }

        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.instance;

        StartCoroutine(Shoot(GameBalance.GetCannonCooldown()));
    }

    private IEnumerator Shoot(float cooldown)
    {
        yield return new WaitForSeconds(Random.Range(0.4f, cooldown));

        while (true)
        {
            yield return new WaitForSeconds(cooldown);

            GameObject ball = Instantiate(SelectRandomPrefab(), startPoint.position, Quaternion.identity);
            gameManager.curLevelGameObjects.Add(ball);
            audioSource.Play();

            ball.GetComponent<Rigidbody>().velocity = CalculateVelocityByTime(cooldown);
        }
    }

    private GameObject SelectRandomPrefab()
    {
        int randomNumber = Random.Range(1, 101);
        if (randomNumber <= 30)
        {
            return ballPrefab;
        }
        else if (randomNumber > 70)
        {
            return coinBallPrefab;
        }
        return bombPrefab;
    }

    private Vector3 CalculateVelocityByTime(float time)
    {
        float max_y = (gateCorners[1].position.y - startPoint.position.y) / time + Physics.gravity.magnitude * time / 2.0f;
        float min_y = Physics.gravity.magnitude * time / 2.0f - startPoint.position.y / time;

        float x = Random.Range(gateCorners[0].position.x - startPoint.position.x, gateCorners[2].position.x - startPoint.position.x) / time;
        float y = Random.Range(min_y, max_y);
        float z = (gateCorners[1].position.z - startPoint.position.z) / time;

        return new Vector3(x, y, z);
    }
}