using UnityEngine;
using System.Collections;

public class SePlayer : MonoBehaviour 
{
    static public SePlayer m_instance = null;

    public AudioClip m_clip1;
    public AudioClip m_clip2;
    public AudioClip m_clip3;
    public AudioClip m_clip4;

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(gameObject);

        m_instance = this;
	}

    public void PlayBreak()
    {
        AudioSource.PlayClipAtPoint(m_clip1, Vector3.one);
    }

    public void PlayDong()
    {
        AudioSource.PlayClipAtPoint(m_clip2, Vector3.one);
    }

    public void PlayRoll()
    {
        AudioSource.PlayClipAtPoint(m_clip3, Vector3.one);
    }

    public void PlayOver()
    {
        AudioSource.PlayClipAtPoint(m_clip4, Vector3.one);
    }
	
}
