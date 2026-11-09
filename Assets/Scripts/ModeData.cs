using System.Collections.Generic;
using UnityEngine;

public enum GimmickType
{
    SealIBlock,         // 1. I-블록 봉인
    VariableDropSpeed,  // 2. 변속 낙하
    TimeAttack,         // 3. 미션 타임 어택
    InfectedBlock,      // 4. 감염 블록
    Earthquake,         // 5. 지진
    DisableHardDrop     // 6. 하드 드롭 봉인
}

[CreateAssetMenu(fileName = "ModeData_01", menuName = "Tetris/Mode Data")]
public class ModeData : ScriptableObject
{
    [Header("Mode Info")]
    public string modeTitle;
    //    [TextArea] public string modeDescription;

    [Header("Random Gimmick Rule")]
    [Range(1, 6)]
    public int randomGimmickCount = 2; // 2개 또는 3개 동시 적용 설정
    public List<GimmickType> gimmickPool = new List<GimmickType>()
    {
        GimmickType.SealIBlock,
        GimmickType.VariableDropSpeed,
        GimmickType.TimeAttack,
        GimmickType.InfectedBlock,
        GimmickType.Earthquake,
        GimmickType.DisableHardDrop
    };

    [Header("Override Rules & Gimmick Settings")]
    public bool isPractice;
    public int targetLines = 40;
    public float timeAttackLimit = 60f;
    public float minDropDelay = 0.15f;
    public float maxDropDelay = 0.8f;
    public float infectionInterval = 6f;
    public float earthquakeInterval = 10f;
}