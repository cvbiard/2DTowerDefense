using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    [Header("References")]
    [SerializeField] TextMeshProUGUI waveCounterUI;



    private bool isHoveringUI;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }

    public void UpdateWave(int newWave)
    {
        waveCounterUI.text = "Wave: " + newWave;
    }
}
