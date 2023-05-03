using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    #region Target
    [Space(5)]
    [Header("TARGET")]
    public List<GameObject> targetObjects = new();
    public List<Transform> targetPositions = new();
    [SerializeField] private GameObject targetPrefab;
    public List<int> targetNumbers = new();
    #endregion

    [HideInInspector] public int answer = 0;
    [HideInInspector] public int sum = 0;

    [HideInInspector] public int score = 0;
    public int timeRemainingInSeconds;

    private bool isGameOver = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameState = GameState.LevelStart;
        UIController.Instance.UpdateCountdownText();
        InvokeRepeating(nameof(DecreaseTime), 1f, 1f);
        StartLevel();
    }

    private void Update()
    {
        SetCursor();
        HandleState();
    }

    private void SetCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void HandleState()
    {
        switch (gameState)
        {
            case GameState.LevelStart:
                UIController.Instance.ResetQuestionText();
                gameState = GameState.InLevel;
                break;

            case GameState.InLevel:
                if (targetObjects.Count <= 3)
                {
                    CheckSum();
                    DestroyAllTargets();
                    gameState = GameState.LevelEnd;
                }
                else if (isGameOver)
                {
                    gameState = GameState.GameOver;
                }
                break;

            case GameState.LevelEnd:
                StartLevel();
                break;

            case GameState.GameOver:
                if (isGameOver)
                {
                    StartCoroutine(RestartGame());
                    isGameOver = false;
                }
                break;
        }
    }

    private void StartLevel()
    {
        answer = 0;
        sum = 0;
        RespawnTargetObjects();
        DecideNumbers();
        ChooseNumbers();
        gameState = GameState.LevelStart;
    }

    private void DestroyAllTargets()
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            Destroy(targetObjects[i]);
        }
        targetObjects.Clear();
        UIController.Instance.numberTexts.Clear();
    }

    private void RespawnTargetObjects()
    {
        for (int i = 0; i < targetPositions.Count; i++)
        {
            GameObject target = Instantiate(targetPrefab, targetPositions[i].position, Quaternion.identity);
            targetObjects.Add(target);
        }
        targetNumbers.Clear();
        UIController.Instance.FillNumberTextList();
    }

    private IEnumerator RestartGame()
    {
        UIController.Instance.UpdateLevelStateText($"GAME OVER\nYour Score: {score}");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Number Functions
    public void DecideNumbers()
    {
        for (int i = 0; i < 6; i++)
        {
            int number = Random.Range(1, 10);
            targetNumbers.Add(number);
        }
        UIController.Instance.UpdateNumbersText();
    }

    public void ChooseNumbers()
    {
        List<int> previousIndexes = new();
        int index;

        for (int i = 0; i < 3; i++)
        {
            do
            {
                index = Random.Range(0, 6);
            } while (previousIndexes.Contains(index));

            previousIndexes.Add(index);

            answer += targetNumbers[index];
        }

        previousIndexes.Clear();
    }

    public void AddNumbersToSum(int number)
    {
        sum += number;
    }

    private void CheckSum()
    {
        if (sum == answer)
        {
            UIController.Instance.UpdateLevelStateText("Congrats!");
            IncreaseScore(25);
            IncreaseTime(10);
            UIController.Instance.UpdateScoreText();
        }
        else
        {
            UIController.Instance.UpdateLevelStateText("Try again!");
        }
    }
    #endregion

    #region Increase - Decrease Functions
    public void IncreaseScore(int amount) => score += amount;
    public void DecreaseScore(int amount) => score -= amount;
    public void IncreaseTime(int amount) => timeRemainingInSeconds += amount;
    private void DecreaseTime()
    {
        timeRemainingInSeconds -= 1;

        UIController.Instance.UpdateCountdownText();

        if (timeRemainingInSeconds <= 0)
        {
            CancelInvoke();
            isGameOver = true;
        }
    }
    #endregion
}

public enum GameState
{
    LevelStart,
    InLevel,
    LevelEnd,
    GameOver
}
