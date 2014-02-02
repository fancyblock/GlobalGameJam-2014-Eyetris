using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Domoni3d : MonoBehaviour 
{
    public GameObject m_cubeModel;
	public Grid3d[] m_blockInfo;
    public float m_size;
    public List<GameObject> m_cubes;
    public int m_rotateSteps = 15;
    public float m_stepAngle = 6.0f;
    public bool m_stable = false;

    protected Grid3d[] m_blockState;
    protected eRotation m_angleX;
    protected eRotation m_angleY;
    protected eRotation m_angleZ;

    protected bool m_inCoroutine;

    /// <summary>
    /// initial 
    /// </summary>
	void Awake()
	{
        m_blockState = new Grid3d[m_blockInfo.Length];

        for (int i = 0; i < m_blockInfo.Length; i++)
        {
            m_blockState[i] = new Grid3d(m_blockInfo[i]);
        }

        m_angleX = eRotation.eAngle0;
        m_angleY = eRotation.eAngle0;
        m_angleZ = eRotation.eAngle0;

        m_inCoroutine = false;
	}

	// Use this for initialization
	void Start () 
	{
	}

	/// <summary>
	/// generate the domoni via blockInfo 
	/// </summary>
	[ContextMenu("Create Shape")]
	public void GenDomoni()
	{
		Debug.Log( "[Domoni3d]: GenDomoni" );

        m_cubes = new List<GameObject>();

        // create the cube 
		foreach( Grid3d g in m_blockInfo )
		{
            GameObject cube = (GameObject)Instantiate(m_cubeModel);

            cube.transform.parent = transform;
            cube.transform.localScale = new Vector3(m_size, m_size, m_size);
            cube.transform.localPosition = new Vector3(g.m_x * m_size, g.m_y * m_size, g.m_z * m_size);

            m_cubes.Add(cube);
		}
	}

    /// <summary>
    /// reset the domoni3d's rotation 
    /// </summary>
    [ContextMenu("Normalize")]
    public void Normalize()
    {
        Debug.Log("[Domoni3d]: Normalize");

        m_blockState = new Grid3d[m_blockInfo.Length];

        for (int i = 0; i < m_blockInfo.Length; i++)
        {
            m_blockState[i] = new Grid3d(m_blockInfo[i]);
        }

        m_angleX = eRotation.eAngle0;
        m_angleY = eRotation.eAngle0;
        m_angleZ = eRotation.eAngle0;

		transform.localRotation = Quaternion.Euler(0,0,0);
    }

    /// <summary>
    /// rotate the domoni3d with x axis
    /// </summary>
    [ContextMenu("SwitchX")]
    public void SwitchX()
    {
		if( m_inCoroutine )
		{
			return;
		}
		
		m_angleX = getNextRotation(m_angleX);
        m_inCoroutine = true;
		StartCoroutine("rotateX");

        // update the block state 
        foreach( Grid3d g3 in m_blockState )
        {
            int tmp = g3.m_y;
            g3.m_y = -g3.m_z;
            g3.m_z = tmp;
        }
    }

	/// <summary>
	/// return the domino2d after switched 
	/// </summary>
	/// <returns>The switched domino2d.</returns>
	public Domoni2d GetSwitchedXDomino2d()
	{
		Domoni2d d2 = new Domoni2d();
		d2.ConvertByBefore3dSwitchX( m_blockState );

		return d2;
	}

    /// <summary>
    /// rotation animation
    /// </summary>
    /// <returns></returns>
	IEnumerator rotateX()
	{
		yield return null;

        for (int i = 0; i < m_rotateSteps; i++)
		{
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.right, m_stepAngle);
			yield return null;
		}

        m_inCoroutine = false;
	}

    /// <summary>
    /// rotate the domoni3d with y axis 
    /// </summary>
	[ContextMenu("SwitchY")]
    public void SwitchY()
    {
		if( m_inCoroutine )
		{
			return;
		}

		m_angleY = getNextRotation(m_angleY);
        m_inCoroutine = true;
		StartCoroutine("rotateY");

        // update the block state 
        foreach (Grid3d g3 in m_blockState)
        {
            int tmp = g3.m_x;
            g3.m_x = g3.m_z;
            g3.m_z = -tmp;
        }
    }

	/// <summary>
	/// return the domino2d after switched 
	/// </summary>
	/// <returns>The switched domino2d.</returns>
	public Domoni2d GetSwitchedYDomino2d()
	{
		Domoni2d d2 = new Domoni2d();
		d2.ConvertByBefore3dSwitchY( m_blockState );
		
		return d2;
	}

    /// <summary>
    /// rotation animation
    /// </summary>
    /// <returns></returns>
	IEnumerator rotateY()
	{
		yield return null;

        for (int i = 0; i < m_rotateSteps; i++)
		{
			gameObject.transform.RotateAround( gameObject.transform.position, Vector3.up, m_stepAngle );
			yield return null;
		}

        m_inCoroutine = false;
	}

    /// <summary>
    /// rotate the domoni3d with z axis 
    /// </summary>
	[ContextMenu("SwitchZ")]
    public void SwitchZ()
    {
		if( m_inCoroutine )
		{
			return;
		}

		m_angleZ = getNextRotation(m_angleZ);
        m_inCoroutine = true;
		StartCoroutine("rotateZ");

        // update the block state 
        foreach (Grid3d g3 in m_blockState)
        {
            int tmp = g3.m_x;
            g3.m_x = -g3.m_y;
            g3.m_y = tmp;
        }
    }

	/// <summary>
	/// return the domino2d after switched 
	/// </summary>
	/// <returns>The switched domino2d.</returns>
	public Domoni2d GetSwitchedZDomino2d()
	{
		Domoni2d d2 = new Domoni2d();
		d2.ConvertByBefore3dSwitchZ( m_blockState );
		
		return d2;
	}

    /// <summary>
    /// rotation animation
    /// </summary>
    /// <returns></returns>
	IEnumerator rotateZ()
	{
		yield return null;

        for (int i = 0; i < m_rotateSteps; i++)
		{
            gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, m_stepAngle);
			yield return null;
		}

        m_inCoroutine = false;
	}

    /// <summary>
    /// return the block info 
    /// </summary>
    public Grid3d[] BLOCK_INFO
    {
        get
        {
            return m_blockState;
        }
    }


    //------------------------------ private function ------------------------------ 


    /// <summary>
    /// clean the current cubes 
    /// </summary>
    protected void clean()
    {
    }

    /// <summary>
    /// return the next rotation 
    /// </summary>
    /// <param name="rot"></param>
    /// <returns></returns>
	protected eRotation getNextRotation( eRotation rot )
	{
        eRotation nextRot = eRotation.eAngle0;

        switch( rot )
        {
            case eRotation.eAngle0:
                nextRot = eRotation.eAngle90;
                break;
            case eRotation.eAngle90:
                nextRot = eRotation.eAngle180;
                break;
            case eRotation.eAngle180:
                nextRot = eRotation.eAngle270;
                break;
            case eRotation.eAngle270:
                nextRot = eRotation.eAngle0;
                break;
            default:
                break;
        }

        return nextRot;
	}

    /// <summary>
    /// return the angle by eRotation enum 
    /// </summary>
    /// <param name="rot"></param>
    /// <returns></returns>
    protected float getAngleByEnum( eRotation rot )
    {
        float angle = 0.0f;

        switch( rot )
        {
            case eRotation.eAngle0:
                angle = 0.0f;
                break;
            case eRotation.eAngle90:
                angle = 90.0f;
                break;
            case eRotation.eAngle180:
                angle = 180.0f;
                break;
            case eRotation.eAngle270:
                angle = 270.0f;
                break;
            default:
                break;
        }

        return angle;
    }

}
