using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
	public UILabel m_txtScore;

	// Use this for initialization
	void Start () 
	{
		m_txtScore.text = ""+GlobalWork.SharedInstance.SCORE;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
