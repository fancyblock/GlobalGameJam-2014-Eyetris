using UnityEngine;
using System.Collections;

public class DisplayInit : MonoBehaviour 
{
	public int m_width;
	public int m_height;
	public int m_fps;
	public bool m_isFullscreen;

	// Use this for initialization
	void Start () 
	{
		Screen.SetResolution( m_width, m_height, m_isFullscreen, m_fps );
	}

}
