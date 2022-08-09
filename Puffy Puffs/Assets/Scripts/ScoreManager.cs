using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : Singleton<ScoreManager> 
{
	int m_currentScore = 0;
    public int CurrentScore
    {
        get
        {
            return m_currentScore;
        }
    }

	int m_counterValue = 0;
	int m_increment = 1;

	public Text scoreText;
	public float countTime = 1f;

	// Use this for initialization
	void Start () 
	{
		UpdateScoreText (m_currentScore);
	}

	public void UpdateScoreText(int scoreValue)
	{
		if (scoreText != null) 
		{
			scoreText.text = scoreValue.ToString ();
		}
	}

	public void AddScore(int value)
	{
		m_currentScore += value;
		StartCoroutine (CountScoreRoutine ());
	}

	IEnumerator CountScoreRoutine()
	{
		int iterations = 0;

		while (m_counterValue < m_currentScore && iterations < 100000) 
		{
			m_counterValue += m_increment;
			UpdateScoreText (m_counterValue);
			iterations++;
			yield return null;
		}

		m_counterValue = m_currentScore;
		UpdateScoreText (m_currentScore);

	}

}
