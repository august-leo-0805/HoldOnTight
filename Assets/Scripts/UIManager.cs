using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct GimmickIconEntry
{
    public GimmickType type;
    public Sprite icon;
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("InGame Gimmick Icons")]
    public GimmickIconEntry[] gimmickIconDatabase; // 6개 기믹 타입별 아이콘 등록
    public Image[] activeGimmickIconSlots;          // 우측 상단 아이콘 Image 슬롯 3개


    [Header("UI Text Fields")]
    public Text scoreText;
    public Text timerText;
    public Text linesText;
    public Image stageIconImage;

    [Header("UI Panels")]
    public GameObject gameStart;
    public GameObject selectMode;
    public GameObject selectPractice;
    public GameObject inGameUI;
    public GameObject gamePlay;
    public GameObject gameOver;
    public GameObject stageClearPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetUI();
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }

    public void UpdateTimerUI(float time, bool isTimeAttack)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        string label = isTimeAttack ? "LEFT" : "TIME";
        timerText.text = $"{label}: {minutes:00}:{seconds:00}.{milliseconds:00}";
    }

    public void UpdateLinesUI(int current, int target)
    {
        if (linesText != null) linesText.text = $"LINES: {current} / {target}";
    }

    public void UpdateStageIcon(Sprite sprite)
    {
        stageIconImage.sprite = sprite;
        stageIconImage.gameObject.SetActive(sprite != null);
    }

    // UIManager.cs 내부에 추가

    /// <summary>
    /// 현재 적용된 기믹 리스트를 받아 우측 상단 아이콘 슬롯을 갱신합니다.
    /// </summary>
    public void UpdateActiveGimmickIcons(List<GimmickType> activeGimmicks)
    {
        for (int i = 0; i < activeGimmickIconSlots.Length; i++) { activeGimmickIconSlots[i].gameObject.SetActive(false); }

        int count = Mathf.Min(activeGimmicks.Count, activeGimmickIconSlots.Length);
        for (int i = 0; i < count; i++)
        {
            Sprite icon = GetGimmickSprite(activeGimmicks[i]);
            activeGimmickIconSlots[i].sprite = icon;
            activeGimmickIconSlots[i].gameObject.SetActive(true);
        }
    }

    private Sprite GetGimmickSprite(GimmickType type)
    {
        for (int i = 0; i < gimmickIconDatabase.Length; i++)
        {
            if (gimmickIconDatabase[i].type == type) { return gimmickIconDatabase[i].icon; }
        }
        return null;
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void ShowStageClear()
    {
        if (stageClearPanel != null) stageClearPanel.SetActive(true);
    }

    public void ResetUI()
    {
        UpdateScore(0);

        if (StageManager.Instance != null)
        {
            UpdateTimerUI(StageManager.Instance.stageTimer, StageManager.Instance.isTimeAttack);
            UpdateLinesUI(0, StageManager.Instance.targetLines);
        }

        ShowPanels(gameStart);
    }

    void SetPanelActive(GameObject panel, bool active)
    {
        panel.SetActive(active);
    }

    void ShowPanels(params GameObject[] targetPanels)
    {
        SetPanelActive(gameStart, Array.Exists(targetPanels, p => p == gameStart));
        SetPanelActive(selectMode, Array.Exists(targetPanels, p => p == selectMode));
        SetPanelActive(selectPractice, Array.Exists(targetPanels, p => p == selectPractice));
        SetPanelActive(inGameUI, Array.Exists(targetPanels, p => p == inGameUI));
        SetPanelActive(gamePlay, Array.Exists(targetPanels, p => p == gamePlay));
        SetPanelActive(gameOver, Array.Exists(targetPanels, p => p == gameOver));
        SetPanelActive(stageClearPanel, Array.Exists(targetPanels, p => p == stageClearPanel));
    }

    void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void GameStart()
    {
        GoToStageSelect();
    }

    public void PlayGame()
    {
        ResumeTime();
        ShowPanels(inGameUI, gamePlay);
    }

    public void GoToTitle()
    {
        ResumeTime();
        ClearBoardTiles();

        ResetUI();

        AudioManager.Instance.PlayTitleBGM();

    }

    public void GoToStageSelect()
    {
        ResumeTime();
        ClearBoardTiles();
        ShowPanels(selectMode);

        AudioManager.Instance.PlayTitleBGM();
    }

    public void GoToPracticeStage()
    {
        ResumeTime();
        ClearBoardTiles();
        ShowPanels(selectPractice);

        AudioManager.Instance.PlayTitleBGM();
    }

    public void ReStart()
    {
        ResumeTime();
        ShowPanels(inGameUI, gamePlay);

        GameManager.Instance.ReStart();
    }

    private void ClearBoardTiles()
    {
        if (GameManager.Instance != null && GameManager.Instance.board != null)
        {
            Board board = GameManager.Instance.board;
            if (board.tilemap != null) board.tilemap.ClearAllTiles();
            if (board.previewTilemap != null) board.previewTilemap.ClearAllTiles();
            if (board.holdTilemap != null) board.holdTilemap.ClearAllTiles();
        }
    }
}