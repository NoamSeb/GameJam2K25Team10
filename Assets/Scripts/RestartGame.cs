using System;
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
        DNDOL.Instance.restartedGames += 1;
        if (DNDOL.Instance.restartedGames == 3)
        {
            DNDOL.Instance.bKonami = true;
        }
        
        AudioManager.Instance.musicAudioSource.Play();
        SceneManager.LoadScene("Game");
    }

    public void GoBackToMenu()
    {
        transitionCanvas = GameObject.FindGameObjectWithTag("TransitionCanvas");
        animator = transitionCanvas.GetComponent<Animator>();

        DNDOL.Instance.restartedGames = 0;
        DNDOL.Instance.bKonami = false;
        
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

    private void Start()
    {
        if (DNDOL.Instance.bKonami)
        {
            AudioManager.Instance.musicAudioSource.clip = AudioManager.Instance.konamiMusic;
            AudioManager.Instance.musicAudioSource.Play();
        }
        else
        {
            AudioManager.Instance.musicAudioSource.clip = AudioManager.Instance.gameplayMusic;
            AudioManager.Instance.musicAudioSource.Play();
        }
    }
}
