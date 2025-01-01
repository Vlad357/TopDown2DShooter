using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public ScoreData scoreData;

    public Gun playerGun;
    public Gun enemyGun;

    public TextMeshProUGUI playerCountText;
    public TextMeshProUGUI enemyCountText;

    private void Start()
    {
        playerCountText.text = scoreData.playerScore.ToString();
        enemyCountText.text = scoreData.enemyScore.ToString();

        playerGun.bulletIsCollisionEvent += AddPlayerCount;
        enemyGun.bulletIsCollisionEvent += AddEnemyCount;
    }

    private void AddPlayerCount()
    {
        scoreData.AddPlayerScore(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddEnemyCount()
    {
        scoreData.AddEnemyScore(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
