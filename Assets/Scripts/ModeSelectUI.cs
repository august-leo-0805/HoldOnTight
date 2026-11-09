using System.Collections.Generic;
using UnityEngine;

public class ModeSelectUI : MonoBehaviour
{
    public List<ModeData> modeList;
    public GameObject modeButtonPrefab;
    public Transform gridParent;

    void Start()
    {
        RefreshModeButtons();
    }

    public void RefreshModeButtons()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ModeData data in modeList)
        {
            GameObject btnObj = Instantiate(modeButtonPrefab, gridParent);
            ModeButton modeBtn = btnObj.GetComponent<ModeButton>();
            if (modeBtn != null) { modeBtn.Setup(data); }
        }
    }
}