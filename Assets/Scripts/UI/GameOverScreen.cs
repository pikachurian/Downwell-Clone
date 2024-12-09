using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI combosText;

    public void Setup(int gems, int combos)
    {
        gameObject.SetActive(true);

        gemsText.text += gems.ToString();
        combosText.text += combos.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
