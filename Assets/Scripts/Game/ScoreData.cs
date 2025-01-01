using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject
{
    public int playerScore = 0;
    public int enemyScore = 0;

    public void ResetScore()
    {
        playerScore = 0;
        enemyScore = 0;
    }

    public void AddPlayerScore(int points)
    {
        playerScore += points;
    }

    public void AddEnemyScore(int points)
    {
        enemyScore += points;
    }
}
