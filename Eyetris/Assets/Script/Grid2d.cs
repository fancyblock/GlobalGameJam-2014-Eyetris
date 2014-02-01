using System;

public class Grid2d 
{
    public int m_x;
    public int m_y;

    /// <summary>
    /// constructor 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
	public Grid2d( int x, int y )
    {
        m_x = x;
        m_y = y;
    }

    /// <summary>
    /// constructor 
    /// </summary>
    /// <param name="grid3d"></param>
    public Grid2d( Grid3d grid3d )
    {
        m_x = grid3d.m_x;
        m_y = grid3d.m_y;
    }

    /// <summary>
    /// judge if the grid2d is occupy the same position with another 
    /// </summary>
    /// <param name="g2"></param>
    /// <returns></returns>
    public bool IsEqual( Grid2d g2 )
    {
        if( m_x == g2.m_x && m_y == g2.m_y )
        {
            return true;
        }

        return false;
    }

}
