using UnityEngine;
using System.Collections;

public class PreviewNode : MonoBehaviour 
{
    protected Domoni3d m_domoni3d = null;

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0.3f, 0.6f, 0.9f);
	}

    /// <summary>
    /// add domino 3d for preview 
    /// </summary>
    /// <param name="d3"></param>
    public void AddDomino3d( Domoni3d d3 )
    {
        m_domoni3d = d3;

        m_domoni3d.transform.parent = transform;
        m_domoni3d.transform.localPosition = Vector3.zero;
        m_domoni3d.transform.localScale = new Vector3(25.0f, 25.0f, 25.0f);
        
        //[HACK]
        if( m_domoni3d.m_stable )
        {
            m_domoni3d.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }
        //[HACK]
    }

    /// <summary>
    /// clean the preview 
    /// </summary>
    public void Clean()
    {
        if( m_domoni3d != null )
        {
            m_domoni3d.transform.parent = null;
            m_domoni3d = null;
        }
    }

}
