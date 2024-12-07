using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.AppleTV;

public class GemHighUI : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public playerController player;
    public GameObject gemIcon;
    public float timer = 7f;

    private float tick = 0f;
    private int gemsTowardsHigh = 0;
    private bool isHigh = false;

    private void Update()
    {
        if (tick <= 0)
        {
            //Times run out
            gemsTowardsHigh = 0;
            //Hide UI
            Hide();

            if (isHigh)
            {
                player.SetHigh(false);
                tmpro.text = "";
                isHigh = false;
            }
        }
        else tick -= Time.deltaTime;
    }

    public void SetValue(int value)
    {
        tmpro.text = value.ToString();
    }

    public void AddGemsToHigh(int count)
    {
        tick = timer;
        gemsTowardsHigh += count;
        SetValue(gemsTowardsHigh);

        if (isHigh == false && gemsTowardsHigh > 0)
        {
            Show();
            print("Does it work?");
        }

        if (gemsTowardsHigh > 100)
        {
            EnterHigh();
        }
    }

    private void Hide()
    {
        tmpro.text = "";
        gemIcon.SetActive(false);
    }

    private void Show()
    {
        gemIcon.SetActive(true);
    }

    private void EnterHigh()
    {
        tmpro.text = "GEM HIGH";
        gemIcon.SetActive(false);
        isHigh = true;
        tick = timer;
        player.SetHigh(true);
    }
}
