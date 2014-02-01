using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour 
{
    public UISprite m_imgEye;
    public Animator m_animation;

	// Use this for initialization
	void Start () 
    {	
	}

    /// <summary>
    /// rotate the x 
    /// </summary>
    public void RotateX()
    {
		m_animation.Play("eyeRotateX");
    }

    /// <summary>
    /// rotate the y
    /// </summary>
    public void RotateY()
    {
		m_animation.Play("eyeRotateY");
    }

    /// <summary>
    /// rotate the z 
    /// </summary>
    public void RotateZ()
    {
		m_animation.Play("eyeRotateZ");
    }
}
