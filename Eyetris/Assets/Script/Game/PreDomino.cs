using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PreDomino : MonoBehaviour 
{
	public GameObject m_blockTemplete;

	protected List<GameObject> m_gridList;

	// Use this for initialization
	void Start () 
	{
		m_gridList = new List<GameObject>();
	}

	/// <summary>
	/// set domino 
	/// </summary>
	/// <param name="domino">Domino.</param>
	public void SetDomino( Domoni2d domino )
	{
		foreach( GameObject go in m_gridList )
		{
			Destroy( go );
		}

		if( domino != null )
		{
			foreach( Grid2d g2 in domino.BLOCK_INFO )
			{
				GameObject go = (GameObject)Instantiate( m_blockTemplete );
				go.transform.parent = transform;
				go.transform.localScale = Vector3.one;
				go.transform.localPosition = new Vector3( g2.m_x * 30.0f, g2.m_y * 30.0f, 0.0f );

				m_gridList.Add( go );
			}
		}
	}

}
