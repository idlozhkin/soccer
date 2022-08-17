using UnityEngine;

public class Cannon : ObjectPool
{
    [SerializeField] private GameObject gate;
    [Header("Projectiles")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject bombPrefab;
    private float cooldown;
    private float passedTime = 0;
    private AudioSource audioSource;
    private Transform[] gateCorners = new Transform[4];

    private void Start()
    {
        FillingPool();

        transform.LookAt(Vector3.zero);

        for (int i = 0; i < 4; i++)
        {
            gateCorners[i] = gate.transform.GetChild(i);
        }

        audioSource = GetComponent<AudioSource>();
        cooldown = GameBalance.GetCannonCooldown();
    }

    private void Update()
    {
        passedTime += Time.deltaTime;

        if (passedTime >= cooldown)
        {
            if (TryGetObject(out GameObject ball))
            {
                passedTime = 0;
                SetBall(ball);
            }
        }
    }

    private void FillingPool()
    {
        for (int i = 0; i < capacity; i++)
        {
            if (i <= 3)
            {
                Initialize(ballPrefab);
            }
            else if (i > 7)
            {
                Initialize(coinPrefab);
            }
            else if (i > 3 && i <= 7)
            {
                Initialize(bombPrefab);
            }
        }
    }

    private void SetBall(GameObject ball)
    {
        ball.transform.position = startPoint.position;
        ball.SetActive(true);

        audioSource.Play();

        ball.GetComponent<Rigidbody>().velocity = CalculateVelocityByTime(cooldown);
    }

    private Vector3 CalculateVelocityByTime(float time)
    {
        float max_y = (gateCorners[1].position.y - startPoint.position.y) / time + Physics.gravity.magnitude * time / 2.0f;
        float min_y = Physics.gravity.magnitude * time / 2.0f - startPoint.position.y / time;

        float x = Random.Range(gateCorners[0].position.x - startPoint.position.x, gateCorners[2].position.x - startPoint.position.x) / time;
        float y = Random.Range(min_y, max_y);
        float z = (gateCorners[1].position.z - startPoint.position.z) / time;

        //return new Vector3(x, max_y, z);
        return new Vector3(x, y, z);
    }
}