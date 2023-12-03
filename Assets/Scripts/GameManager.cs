using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += CallGameOver;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= CallGameOver;
    }

    private void CallGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
