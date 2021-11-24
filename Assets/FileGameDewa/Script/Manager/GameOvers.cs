using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOvers : MonoBehaviour
{
    public PlayerHeal playerHeal;
    public Text warningText;
    public float restartDelay = 6f;

    Animator anim;
    float restartGame;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(playerHeal.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            if(restartGame >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }
}
