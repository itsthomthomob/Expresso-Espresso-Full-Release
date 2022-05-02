using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Music Controls")]
    [SerializeField] private float MusicAmount;
    public Slider MusicSlider;
    public TMP_Text MusicAmountText;

    [Header("SFX Controls")]
    [SerializeField] private float SFXAmount;
    public Slider SFXSlider;
    public TMP_Text SFXAmountText;

    [Header("Settings")]
    public GameObject SettingsPanel;
    public AudioSource MusicAudio;
    public AudioSource SFXAudio;
    public Button B_Close;
    public Button B_Open;

    private void Awake()
    {
        MusicAudio.volume = PlayerPrefs.GetFloat("P_MusicAmount");
        SFXAudio.volume = PlayerPrefs.GetFloat("P_SFXAmount");
    }

    private void Start()
    {
        MusicSlider.onValueChanged.AddListener(delegate { ChangeMusic(); });
        SFXSlider.onValueChanged.AddListener(delegate { ChangeSFX(); });
        B_Close.onClick.AddListener(OnClose);
        B_Open.onClick.AddListener(OnOpen);
        MusicAmountText.text = MusicAmount + "%";
        SFXAmountText.text = SFXAmount + "%";
    }

    private void ChangeMusic() 
    {
        MusicAmount = MusicSlider.value;
        MusicAmountText.text = MusicAmount + "%";
        MusicAudio.volume = MusicAmount;
        PlayerPrefs.SetFloat("P_MusicAmount", MusicAmount);
    }

    private void ChangeSFX()
    {
        SFXAmount = SFXSlider.value;
        SFXAmountText.text = SFXAmount + "%";
        SFXAudio.volume = SFXAmount;
        PlayerPrefs.SetFloat("P_SFXAmount", SFXAmount);
    }

    private void OnOpen() 
    {
        SettingsPanel.SetActive(true);
    }

    private void OnClose()
    {
        SettingsPanel.SetActive(false);
    }
}
