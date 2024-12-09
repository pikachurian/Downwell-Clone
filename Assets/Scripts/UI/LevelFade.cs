using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    public Animator animator;
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelEndReached()
    {
        animator.SetTrigger("LevelFinished");
    }
}
