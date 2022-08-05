using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI coinCount;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject panel;
    [HideInInspector] public List<GameObject> curLevelGameObjects;
    #region "Game variables"
    private int scoreAim;
    private int curPoints = 0;
    private int coins;
    private int levelCost;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        GameBalance.AddDifficulty(PlayerPrefs.GetInt("Difficulty", 1));
    }

    private void Start()
    {
        AddCoins(PlayerPrefs.GetInt("Coins", 0));
        StartLevel();
    }

    private void Update()
    {
        ProgressBarChanging();
    }

    public void AddPoints(int points)
    {
        curPoints = Mathf.Min(curPoints + points, scoreAim);
    }

    public void RemovePoints(int points)
    {
        curPoints = Mathf.Max(curPoints - points, 0);
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
        coinCount.text = this.coins.ToString();
        PlayerPrefs.SetInt("Coins", this.coins);
    }

    public void BuyLevel()
    {
        levelCost = GameBalance.GetLevelCost();
        if (levelCost <= coins)
        {
            coins -= levelCost;
            coinCount.text = this.coins.ToString();
            PlayerPrefs.SetInt("Coins", coins);

            GameBalance.AddDifficulty(1);
            StartLevel();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        panel.SetActive(true);

        levelCost = GameBalance.GetLevelCost();
        costText.text = $"{levelCost} coins";
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void StartLevel()
    {
        Pause();
        ClearCurLevelGameObjects();
        timer.SetTime();

        scoreAim = GameBalance.GetScoreAim();
        curPoints = 0;

        progressBar.maxValue = scoreAim;
        progressBar.value = curPoints;

        foreach (var position in GameBalance.GetCannonsPosition())
        {
            GameObject cannon = Instantiate(cannonPrefab, position, Quaternion.identity);
            curLevelGameObjects.Add(cannon);
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = 1 - AudioListener.volume;
    }

    private void ProgressBarChanging()
    {
        progressBar.value = curPoints;
        if (curPoints >= scoreAim)
        {
            GameBalance.AddDifficulty(1);
            StartLevel();
        }
    }

    private void ClearCurLevelGameObjects()
    {
        foreach (var levelGameObject in curLevelGameObjects)
        {
            Destroy(levelGameObject);
        }

        curLevelGameObjects.Clear();
    }
}
