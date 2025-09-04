using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject transitionCanvas;

    private void Start()
    {
        transitionCanvas = GameObject.FindGameObjectWithTag("TransitionCanvas");
        animator = transitionCanvas.GetComponent<Animator>();
    }

    private IEnumerator CLoadLevel(string sceneToLoad)
    {
        //Debug.Log("Loaded level: " + sceneToLoad);
        animator?.SetTrigger("Start");
        yield return new WaitForSeconds(0.4f);

        if (SceneManager.GetActiveScene().name == "MainMenu")
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
        
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevel(string levelToLoad)
    {
        StartCoroutine(CLoadLevel(levelToLoad));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
