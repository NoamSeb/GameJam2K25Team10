using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    // The Konami code sequence
    private KeyCode[] konamiCode = new KeyCode[]
    {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.Q
    };

    private int currentIndex = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamiCode[currentIndex]))
            {
                currentIndex++;

                if (currentIndex >= konamiCode.Length)
                {
                    DNDOL.Instance.bKonami = true;
                    AudioManager.Instance.sfxAudioSource.PlayOneShot(AudioManager.Instance.konamiValidatedSfx);
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex = 0;
            }
        }
    }
}
