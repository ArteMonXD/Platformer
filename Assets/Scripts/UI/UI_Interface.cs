using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interface : MonoBehaviour
{
    [SerializeField] private PlayerUnit target;
    [Header("Bars")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider manaBar;
    [Header("Died Panel")]
    [SerializeField] private GameObject diedPanel;
    [SerializeField] private float waitTimeDiedPanel;

    public delegate void RestartButtonHandle();
    public event RestartButtonHandle RestartButtonEvent;

    private static UI_Interface _instance;
    public static UI_Interface Instance => _instance;
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }
    void Start()
    {
        target.DiedPlayerEvent += SandDiedPanel;
        SetMaxValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxValue()
    {
        healthBar.maxValue = target.MaxHealth;
        manaBar.maxValue = target.MaxCharge;
    }
    public void ValueUpdate()
    {
        healthBar.value = target.Health;
        manaBar.value = target.Charge;
    }
    public void RestartButton()
    {
        RestartButtonEvent?.Invoke();
    }
    private void SandDiedPanel()
    {
        StartCoroutine(DiedPanelActivate());
    }

    private IEnumerator DiedPanelActivate()
    {
        yield return new WaitForSeconds(waitTimeDiedPanel);
        diedPanel.SetActive(true);
    }
}
