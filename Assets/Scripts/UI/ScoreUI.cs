using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + Lighter.Instance.Score;
    }
}
