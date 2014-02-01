using UnityEngine;
using System.Collections;

public class PressToStart : MonoBehaviour 
{
    protected bool m_isTrans;

	// Use this for initialization
	void Start () 
    {
        m_isTrans = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if( Input.anyKeyDown && !m_isTrans )
        {
            TransScene.Load("Game", 0.5f);

            m_isTrans = true;
        }
	}

}
