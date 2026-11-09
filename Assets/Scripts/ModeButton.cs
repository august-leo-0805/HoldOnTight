using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModeButton : MonoBehaviour
{
    public Text titleText;
    public Button button;

    ModeData modeData;

    public void Setup(ModeData data)
    {
        modeData = data;
        UpdateModeUI();

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnModeButtonClicked);
        }
    }

    void UpdateModeUI()
    {
        if (modeData == null) { return; }

        if (titleText != null) titleText.text = modeData.modeTitle;
    }

    void OnModeButtonClicked()
    {
        if (modeData == null) return;

        // 1. 연습 모드일 경우 연습 모드 전용 UI 창으로 이동
        if (modeData.isPractice) { UIManager.Instance.GoToPracticeStage(); return; }

        StageManager.Instance.ApplyRandomModeData(modeData);

        GameManager.Instance.NewGame();

        UIManager.Instance.PlayGame();

        AudioManager.Instance.PlayGameplayBGM();
    }
}