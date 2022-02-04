using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinDiscord : MonoBehaviour
{
    public Button Join;
    private void Start()
    {
        Join.onClick.AddListener(JoinDiscordServer);
    }

    private void JoinDiscordServer() { Application.OpenURL("https://discord.com/invite/AcbjVdttuY"); }
}
