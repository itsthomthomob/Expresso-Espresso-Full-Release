using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public enum TimeSpeeds
    {
        RT, Two, Three
    }

    [Header("Time Controllers")]
    public Stopwatch Timer = new Stopwatch();
    public TimeSpan GameTime = new TimeSpan();
    public int RealTime;
    public int TwoTimes;
    public int ThreeTimes;
    public int scale;

    [Header("Player Controls")]
    public Button B_RealTime;
    public Button B_TwoTimes;
    public Button B_ThreeTimes;
    public TimeSpeeds CurrentSpeed = TimeSpeeds.RT;

    [Header("Time Texts")]
    public TMP_Text MonthDay;
    public TMP_Text HourMinute;
    private void Start()
    {
        LoadButtons();
        GameTime = TimeSpan.Zero;
        Timer.Start();
    }
    private void FixedUpdate()
    {
        scale = 0;
        switch (CurrentSpeed)
        {
            case TimeSpeeds.RT: scale = RealTime; break;
            case TimeSpeeds.Two: scale = TwoTimes; break;
            case TimeSpeeds.Three: scale = ThreeTimes; break;
            default: return;
        }

        GetPlayerInputs();

        GameTime += TimeSpan.FromTicks(Timer.ElapsedTicks * scale);
        Timer.Restart();

        MonthDay.text = $"{GameTime:%d} Days";
        HourMinute.text = $"{GameTime:hh':'mm':'s}";
    }
    private void GetPlayerInputs() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentSpeed = TimeSpeeds.RT;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentSpeed = TimeSpeeds.Two;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentSpeed = TimeSpeeds.Three;
        }
    }

    private void LoadButtons() 
    {
        B_RealTime.onClick.AddListener(ClickedRealTime);
        B_TwoTimes.onClick.AddListener(ClickedTwoTimes);
        B_ThreeTimes.onClick.AddListener(ClickedThreeTImes);
    }
    private void ClickedRealTime() 
    {
        CurrentSpeed = TimeSpeeds.RT;
    }
    private void ClickedTwoTimes()
    {
        CurrentSpeed = TimeSpeeds.Two;
    }
    private void ClickedThreeTImes()
    {
        CurrentSpeed = TimeSpeeds.Three;
    }

}
