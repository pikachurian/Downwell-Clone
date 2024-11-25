using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemUI : MonoBehaviour
{
    public TextMeshProUGUI tmpro;

    public void SetValue(int value)
    {
        tmpro.text = value.ToString();
    }
}
