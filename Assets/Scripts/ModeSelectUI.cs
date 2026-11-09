using System.Collections.Generic;
using UnityEngine;

public class ModeSelectUI : MonoBehaviour
{
    public List<StageData> stageList;
    public GameObject stageButtonPrefab;
    public Transform gridParent;

    void Start()
    {
        foreach (Transform child in gridParent) { Destroy(child.gameObject); }

        foreach (StageData data in stageList)
        {
            GameObject btnObj = Instantiate(stageButtonPrefab, gridParent);
            ModeButton stageBtn = btnObj.GetComponent<ModeButton>();
            if (stageBtn != null) { stageBtn.Setup(data); }
        }
    }
}