using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cannonPrefab;
    [Inject] private DiContainer diContainer;
    [Inject] private StartGameScreen startGameScreen;
    [Inject] private GameScreen gameScreen;
    [Inject] private PauseScreen pauseScreen;
    [Inject] private GameOverScreen gameOverScreen;
    private List<GameObject> curLevelGameObjects = new List<GameObject>();
    private int scoreAim;
    private int curPoints = 0;
    private int coins;
    private float time;

    public event UnityAction<float> TimeChanged;
    public event UnityAction<int> CoinsChanged;
    public event UnityAction<int, int> ProgressChanged;
    public event UnityAction<int> CostChanged;

    private void Awake()
    {
        // if (Application.isEditor)
        // {
        //     PlayerPrefs.DeleteAll(); //delete in build 
        // }
        coins = PlayerPrefs.GetInt("Coins", 0);

        GameBalance.AddDifficulty(PlayerPrefs.GetInt("Difficulty", 0));
    }


    private void OnEnable()
    {
        startGameScreen.PlayButtonClick += OnPlayButtonClick;
        gameScreen.PauseButtonClick += OnPauseButtonClick;
        pauseScreen.UnpauseButtonClick += OnUnpauseButtonClick;
        pauseScreen.MuteButtonClick += ChangeVolume;
        pauseScreen.BuyButtonClick += BuyLevel;
        gameOverScreen.RestartButtonClick += StartLevel;
    }

    private void OnDisable()
    {
        startGameScreen.PlayButtonClick -= OnPlayButtonClick;
        gameScreen.PauseButtonClick -= OnPauseButtonClick;
        pauseScreen.UnpauseButtonClick -= OnUnpauseButtonClick;
        pauseScreen.MuteButtonClick -= ChangeVolume;
        pauseScreen.BuyButtonClick -= BuyLevel;
        gameOverScreen.RestartButtonClick -= StartLevel;
    }

    private void Start()
    {
        startGameScreen.Close();
        pauseScreen.Close();
        gameOverScreen.Close();
        gameScreen.Close();

        CoinsChanged?.Invoke(coins);
        Time.timeScale = 0;
        time = GameBalance.GetRoundDuration();
        startGameScreen.Open();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            EndTime();
        }
    }

    private void OnUnpauseButtonClick()
    {
        pauseScreen.Close();
        Time.timeScale = 1;
        gameScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        startGameScreen.Close();
        StartLevel();
    }

    private void OnPauseButtonClick()
    {
        CostChanged?.Invoke(GameBalance.GetLevelCost());
        gameScreen.Close();
        pauseScreen.Open();
        Time.timeScale = 0;
    }

    private void EndTime()
    {
        if (curPoints >= scoreAim)
        {
            GameBalance.AddDifficulty(1);
            StartLevel();
            OnPauseButtonClick();
        }
        else
        {
            StartLevel();
            OnPauseButtonClick();
        }
    }
    private void BuyLevel()
    {
        int levelCost = GameBalance.GetLevelCost();
        if (levelCost <= coins)
        {
            coins -= levelCost;
            CoinsChanged?.Invoke(coins);
            PlayerPrefs.SetInt("Coins", coins);

            GameBalance.AddDifficulty(1);
            StartLevel();
        }
    }

    private void StartLevel()
    {
        gameScreen.Open();
        time = GameBalance.GetRoundDuration();
        Time.timeScale = 1;
        ClearCurLevelGameObjects();
        TimeChanged?.Invoke(time);

        scoreAim = GameBalance.GetScoreAim();
        curPoints = 0;

        ProgressChanged?.Invoke(curPoints, scoreAim);

        foreach (var position in GameBalance.GetCannonsPosition())
        {
            GameObject cannon = diContainer.InstantiatePrefab(cannonPrefab, position, Quaternion.identity, transform);
            curLevelGameObjects.Add(cannon);
        }
    }

    private void ChangeVolume()
    {
        AudioListener.volume = 1 - AudioListener.volume;
    }

    private void ClearCurLevelGameObjects()
    {
        foreach (var levelGameObject in curLevelGameObjects)
        {
            Destroy(levelGameObject);
        }

        curLevelGameObjects.Clear();
    }
    public void AddPoints(int points)
    {
        curPoints = Mathf.Min(curPoints + points, scoreAim);
        ProgressChanged?.Invoke(curPoints, scoreAim);
    }

    public void RemovePoints(int points)
    {
        curPoints = Mathf.Max(curPoints - points, 0);
        ProgressChanged?.Invoke(curPoints, scoreAim);
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
        CoinsChanged?.Invoke(this.coins);
        PlayerPrefs.SetInt("Coins", this.coins);
    }
}
