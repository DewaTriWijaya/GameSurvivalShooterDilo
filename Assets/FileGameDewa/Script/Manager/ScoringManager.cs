using UnityEngine;
using UnityEngine.UI;

public class ScoringManager : MonoBehaviour
{
    public static int score;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        // reset the score
        score = 0;
    }

  
    void Update()
    {
        // Atur teks yang ditampilkan menjadi kata "Skor" diikuti dengan nilai skor.
        text.text = "Score: " + score;
    }
}
