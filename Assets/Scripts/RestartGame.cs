using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject transitionCanvas;
    
    public void RestartingGame()
    {
        AudioManager.Instance.musicAudioSource.Play();
        SceneManager.LoadScene("Game");
    }

    public void GoBackToMenu()
    {
        transitionCanvas = GameObject.FindGameObjectWithTag("TransitionCanvas");
        animator = transitionCanvas.GetComponent<Animator>();
        
        StartCoroutine(CLoadLevel("MainMenu"));
    }
    
    private IEnumerator CLoadLevel(string sceneToLoad)
    {
        //Debug.Log("Loaded level: " + sceneToLoad);
        animator?.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            AudioManager.Instance.musicAudioSource.clip = AudioManager.Instance.menuMusic;
            AudioManager.Instance.musicAudioSource.Play();

        }
        
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevel(string levelToLoad)
    {
        StartCoroutine(CLoadLevel(levelToLoad));
    }
}
