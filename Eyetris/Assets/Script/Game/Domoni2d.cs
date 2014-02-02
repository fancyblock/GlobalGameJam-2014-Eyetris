using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Domoni2d
{
    protected List<Grid2d> m_gridInfo;

    /// <summary>
    /// constructor 
    /// </summary>
    public Domoni2d()
    {
        m_gridInfo = new List<Grid2d>();
    }

    /// <summary>
    /// initial the domino by domono3d 
    /// </summary>
    /// <param name="domino"></param>
	public void ConvertByDomino3d( Domoni3d domino )
	{
        m_gridInfo.Clear();

		foreach( Grid3d g3 in domino.BLOCK_INFO )
        {
            Grid2d g2 = new Grid2d(g3);

            if( hasGrid( m_gridInfo, g2 ) == false )
            {
                m_gridInfo.Add(g2);
            }
        }
	}

	/// <summary>
	/// Converts the by before3d switch x.
	/// </summary>
	/// <param name="g3s">G3s.</param>
	public void ConvertByBefore3dSwitchX( Grid3d[] g3s )
	{
		m_gridInfo.Clear();

		foreach( Grid3d g3 in g3s )
		{
			Grid2d g2 = new Grid2d( g3.m_x, -g3.m_z );

			if( hasGrid( m_gridInfo, g2 ) == false )
			{
				m_gridInfo.Add(g2);
			}
		}
	}

	/// <summary>
	/// Converts the by before3d switch y.
	/// </summary>
	/// <param name="g3s">G3s.</param>
	public void ConvertByBefore3dSwitchY( Grid3d[] g3s )
	{
		m_gridInfo.Clear();
		
		foreach( Grid3d g3 in g3s )
		{
			Grid2d g2 = new Grid2d( g3.m_z, g3.m_y );
			
			if( hasGrid( m_gridInfo, g2 ) == false )
			{
				m_gridInfo.Add(g2);
			}
		}
	}

	/// <summary>
	/// Converts the by before3d switch z.
	/// </summary>
	/// <param name="g3s">G3s.</param>
	public void ConvertByBefore3dSwitchZ( Grid3d[] g3s )
	{
		m_gridInfo.Clear();
		
		foreach( Grid3d g3 in g3s )
		{
			Grid2d g2 = new Grid2d( -g3.m_y, g3.m_x );
			
			if( hasGrid( m_gridInfo, g2 ) == false )
			{
				m_gridInfo.Add(g2);
			}
		}
	}

    /// <summary>
    /// return the block infos 
    /// </summary>
    public List<Grid2d> BLOCK_INFO
    {
        get
        {
            return m_gridInfo;
        }
    }


    //---------------------------- private function ---------------------------- 

    /// <summary>
    /// if already has the grid2d 
    /// </summary>
    /// <param name="gridList"></param>
    /// <param name="g2"></param>
    /// <returns></returns>
    protected bool hasGrid( List<Grid2d> gridList, Grid2d g2 )
    {
        foreach( Grid2d theG2 in gridList )
        {
            if( theG2.IsEqual( g2 ) )
            {
                return true;
            }
        }

        return false;
    }

}
