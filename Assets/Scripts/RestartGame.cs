using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartingGame()
    {
        SceneManager.LoadScene("Game");
    }
}
