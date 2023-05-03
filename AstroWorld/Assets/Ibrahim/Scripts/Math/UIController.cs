using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Text Components
    [Space(5)]
    [Header("TEXT COMPONENTS")]
    public List<TextMeshProUGUI> numberTexts = new();
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelStateText;
    #endregion

    public List<int> questionNums = new();

    public static UIController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FillNumberTextList();
    }

    #region Update Functions
    public void ResetQuestionText()
    {
        questionText.text = $"_ + _ + _ = {GameManager.Instance.answer}";
        questionNums.Clear();
    }

    public void UpdateQuestionText()
    {
        if (questionNums.Count == 1)
            questionText.text = $"<u>{questionNums[0]}</u> + _ + _ = {GameManager.Instance.answer}";
        else if (questionNums.Count == 2)
            questionText.text = $"<u>{questionNums[0]}</u> + <u>{questionNums[1]}</u> + _ = {GameManager.Instance.answer}";
        else if (questionNums.Count == 3)
            questionText.text = $"<u>{questionNums[0]}</u> + <u>{questionNums[1]}</u> + <u>{questionNums[2]}</u> = {GameManager.Instance.answer}";
    }

    public void UpdateScoreText() => scoreText.text = "SCORE: " + GameManager.Instance.score.ToString("0000");

    public void UpdateCountdownText()
    {
        int minutes = GameManager.Instance.timeRemainingInSeconds / 60;
        int seconds = GameManager.Instance.timeRemainingInSeconds % 60;
        timeText.text = $"TIME: {minutes:00}:{seconds:00}";
    }

    public void UpdateNumbersText()
    {
        for (int i = 0; i < 6; i++)
        {
            numberTexts[i].text = GameManager.Instance.targetNumbers[i].ToString();
        }
    }

    public void UpdateLevelStateText(string str)
    {
        levelStateText.text = str;
        Invoke(nameof(ResetLevelStateText), 1f);
    }

    private void ResetLevelStateText() => levelStateText.text = " ";
    #endregion

    public void FillNumberTextList()
    {
        GameObject[] numberTextGOs = GameObject.FindGameObjectsWithTag("TargetNumber");

        for (int i = 0; i < numberTextGOs.Length; i++)
        {
            TextMeshProUGUI numberTextTMP = numberTextGOs[i].GetComponent<TextMeshProUGUI>();
            numberTexts.Add(numberTextTMP);
        }
    }
}