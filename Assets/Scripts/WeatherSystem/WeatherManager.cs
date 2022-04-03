using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Timers;
using System.Diagnostics;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherTypes
    {
        None,
        Rain,
        Storm,
        Fog
    }

    [Header("Weather Controllers")]
    public bool DebugMode;
    public WeatherTypes curWeather = WeatherTypes.None;
    public float curWeatherCondition;
    public float RainChance;
    public float StormChance;
    public float FogChance;
    public TimeManager getTime;
    public Stopwatch weatherTimer = new Stopwatch();

    [Header("Weather Prefabs")]
    public GameObject rain;
    public GameObject storm;
    public GameObject fog;
    public AudioClip rainSound;
    public AudioClip stormSound;

    private void Start()
    {
        MasterCafeSystem masterCafeSystem = FindObjectOfType<MasterCafeSystem>();
        curWeatherCondition = masterCafeSystem.WeatherCondition;
        SetWeatherChances();
    }

    private void Update()
    {
        if (!DebugMode)
        {
            ChooseCurrentWeather();
            ManageWeather();
        }
        else 
        {
            DebugWeather();
        }
    }

    private void SetWeatherChances() 
    { 
        RainChance = 0.60f * curWeatherCondition;
        StormChance = 0.50f * curWeatherCondition;
        FogChance = 0.80f * curWeatherCondition;
    }

    private void DebugWeather() 
    {
        ParticleSystem getRPS = rain.GetComponent<ParticleSystem>();
        ParticleSystem getSPS = storm.GetComponent<ParticleSystem>();
        ParticleSystem getFPS = fog.GetComponent<ParticleSystem>();
        switch (curWeather)
        {
            case WeatherTypes.None:
                rain.SetActive(false);
                storm.SetActive(false);
                fog.SetActive(false);
                break;
            case WeatherTypes.Rain:
                if (getRPS.isPlaying == false)
                {
                    getRPS.Play();
                    getSPS.Stop();
                    getFPS.Stop();
                }
                rain.SetActive(true);
                storm.SetActive(false);
                fog.SetActive(false);
                break;
            case WeatherTypes.Storm:
                if (getSPS.isPlaying == false)
                {
                    getRPS.Stop();
                    getSPS.Play();
                    getFPS.Stop();
                }
                storm.SetActive(true);
                fog.SetActive(false);
                rain.SetActive(false);
                break;
            case WeatherTypes.Fog:
                if (getFPS.isPlaying == false)
                {
                    getRPS.Stop();
                    getSPS.Stop();
                    getFPS.Play();
                }
                storm.SetActive(false);
                fog.SetActive(true);
                rain.SetActive(false);
                break;
        }
    }

    private void ChooseCurrentWeather() 
    {
        if (getTime.GameTime.Hours % 3 == 0)
        {
            // For every 3 hours that pass, choose a random weather type
            // Weather is based on the locations weather condition and weather chance
            float index = UnityEngine.Random.Range(0, 1);
            if (index < RainChance)
            {
                curWeather = WeatherTypes.None;
            }
            if (index > RainChance && curWeather == WeatherTypes.None)
            {
                curWeather = WeatherTypes.Rain;
                weatherTimer.Start();
            }
            if (index > StormChance && curWeather == WeatherTypes.None)
            {
                curWeather = WeatherTypes.Storm;
                weatherTimer.Start();
            }
            if (index > FogChance && curWeather == WeatherTypes.None)
            {
                curWeather = WeatherTypes.Fog;
                weatherTimer.Start();
            }
        }
    }

    private void ManageWeather() 
    {
        if (weatherTimer.Elapsed >= new TimeSpan(0, 10, 0) && curWeather != WeatherTypes.None)
        {
            // For every 10 minutes in-game, if there's weather, stop
            weatherTimer.Stop();
            weatherTimer.Restart();
            curWeather = WeatherTypes.None;
        }
        ParticleSystem getRPS = rain.GetComponent<ParticleSystem>();
        ParticleSystem getSPS = storm.GetComponent<ParticleSystem>();
        ParticleSystem getFPS = fog.GetComponent<ParticleSystem>();
        switch (curWeather)
        {
            case WeatherTypes.None:
                rain.SetActive(false);
                storm.SetActive(false);
                fog.SetActive(false);
                break;
            case WeatherTypes.Rain:
                if (getRPS.isPlaying == false)
                {
                    getRPS.Play();
                    getSPS.Stop();
                    getFPS.Stop();
                }
                rain.SetActive(true);
                storm.SetActive(false);
                fog.SetActive(false);
                break;
            case WeatherTypes.Storm:
                if (getSPS.isPlaying == false)
                {
                    getRPS.Stop();
                    getSPS.Play();
                    getFPS.Stop();
                }
                storm.SetActive(true);
                fog.SetActive(false);
                rain.SetActive(false);
                break;
            case WeatherTypes.Fog:
                if (getFPS.isPlaying == false)
                {
                    getRPS.Stop();
                    getSPS.Stop();
                    getFPS.Play();
                }
                storm.SetActive(false);
                fog.SetActive(true);
                rain.SetActive(false);
                break;
        }
    }
}
