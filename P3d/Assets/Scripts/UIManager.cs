using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject startPanel;
    internal bool isStart;

    private void Awake()
    {
        Instance = this;
    }

    public void StartClick()
    {
        startPanel.SetActive(false);
        isStart = true;
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void BackClick()
    {
        startPanel.SetActive(true);
        isStart = false;
    }
}
