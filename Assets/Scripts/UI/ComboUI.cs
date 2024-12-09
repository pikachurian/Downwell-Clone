using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public playerController player;

    public Vector3 followPlayerOffset = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position = player.transform.position + followPlayerOffset;
    }

    public void Show(int comboCount)
    {
        gameObject.SetActive(true);
        SetValue(comboCount);
    }

    public void Hide()
    {
        text.text = "";
        gameObject.SetActive(false);
    }

    public void SetValue(int value)
    {
        text.text = value.ToString();
    }
}
