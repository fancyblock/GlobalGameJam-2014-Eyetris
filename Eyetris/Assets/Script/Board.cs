using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Board : MonoBehaviour 
{
    public const int STATE_IDLE = 0;
    public const int STATE_DROP = 1;
	public const int STATE_MERGE = 2;
    public const int STATE_JUDGE = 3;
    public const int STATE_CLEAN = 4;
    public const int STATE_SPEEDUP = 5;
	public const int STATE_GAME_OVER = 6;

    public int m_width;
    public int m_height;
    public float m_size;
	public GameObject m_gridTemplate;
    public int m_moveStep = 10;
    public float m_moveOffset = 3.0f;
	public ResultEffect m_resultEffect;
    public float m_dorpDominoZ = 320.0f;
	public Eye m_leftEye;
	public Eye m_rightEye;

    protected Domoni3d m_curDomino3d = null;
    protected Domoni2d m_curDomino2d;
    protected Grid[,] m_allGrids;
    protected int m_curBlockX;
    protected int m_curBlockY;
    protected List<int> m_clearLines;

    protected int m_state;
	protected bool m_inCoroutine;

    /// <summary>
    /// initial 
    /// </summary>
	void Awake()
	{
        // initial the grid info 
		m_allGrids = new Grid[m_width,m_height];

        for( int i = 0; i < m_width; i++ )
        {
            for( int j = 0; j < m_height; j++ )
            {
                m_allGrids[i, j] = null;
            }
        }
        
        // create the domino 2d
        m_curDomino2d = new Domoni2d();

        m_state = STATE_IDLE;
		m_inCoroutine = false;
	}

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    /// <summary>
    /// tetris game tick 
    /// </summary>
    [ContextMenu("Tick")]
	public void Tick()
	{
        if( touchTheGround() )
        {
	    	mergeDomino();
        }
        else
        {
			m_inCoroutine = true;
            StartCoroutine("dropDominoOneGrid");
        }
	}

    public IEnumerator dropDominoOneGrid()
    {
        m_curBlockY--;

        for( int i = 0; i < m_moveStep; i++ )
        {
            Vector3 vec3 = m_curDomino3d.transform.localPosition;
            m_curDomino3d.gameObject.transform.localPosition = new Vector3(vec3.x,vec3.y-m_moveOffset,vec3.z);
            
            yield return null;
        }

		m_inCoroutine = false;
    }

    /// <summary>
    /// add a new domino to the board 
    /// </summary>
    /// <param name="d3"></param>
    public void DropDomino( Domoni3d d3 )
    {
        if( m_state == STATE_IDLE )
        {
            m_curDomino3d = d3;

            calculateSmartInitPos();
            addDominoToTheBoard();
            m_curDomino2d.ConvertByDomino3d(m_curDomino3d);

            m_state = STATE_DROP;
        }
		else
		{
            Debug.LogError("[Board]: DropDomino, state error.");
		}
    }

    /// <summary>
    /// rotate the domino in x axis
    /// </summary>
    public void rotateX()
    {
        Debug.Log("[Board]: rotateX");

		if( m_state != STATE_DROP )
		{
			return;
		}

        if( canSwitchX() )
        {
            m_curDomino3d.SwitchX();
            m_curDomino2d.ConvertByDomino3d(m_curDomino3d);
            m_leftEye.RotateX();
            m_rightEye.RotateX();
            SePlayer.m_instance.PlayRoll();
        }
    }


    /// <summary>
    /// rotate the domino in y axis
    /// </summary>
    public void rotateY()
    {
        Debug.Log("[Board]: rotateY");

		if( m_state != STATE_DROP )
		{
			return;
		}

        if( canSwitchY() )
        {
            //TODO 
            m_curDomino3d.SwitchY();
            m_curDomino2d.ConvertByDomino3d(m_curDomino3d);
            m_leftEye.RotateY();
            m_rightEye.RotateY();
            SePlayer.m_instance.PlayRoll();
        }
    }

    /// <summary>
    /// rotate the domino in z axis
    /// </summary>
    public void rotateZ()
    {
        Debug.Log("[Board]: rotateZ");

		if( m_state != STATE_DROP )
		{
			return;
		}

        if (canSwitchZ() )
        {
            //TODO 
            m_curDomino3d.SwitchZ();
            m_curDomino2d.ConvertByDomino3d(m_curDomino3d);
            m_leftEye.RotateZ();
            m_rightEye.RotateZ();
            SePlayer.m_instance.PlayRoll();
        }
    }

    /// <summary>
    /// speed up the drop 
    /// </summary>
    public void speedDrop()
    {
        Debug.Log("[Board: speedDrop");

        m_inCoroutine = true;
        m_state = STATE_SPEEDUP;
        StartCoroutine("speedupDrop");
    }

    protected IEnumerator speedupDrop()
    {
        int finalY = getLandPosition();
        int distance = finalY - m_curBlockY;

        int allSteps = Mathf.Abs(distance);
        for (int i = 0; i < allSteps; i++)
        {
            Vector3 vec3 = m_curDomino3d.transform.localPosition;
            m_curDomino3d.gameObject.transform.localPosition = new Vector3(vec3.x, vec3.y - m_size, vec3.z);

            yield return null;
        }

        m_curBlockY = finalY;
        m_inCoroutine = false;

        mergeDomino();
    }

    /// <summary>
    /// moveLeft
    /// </summary>
    public void moveLeft()
    {
        Debug.Log("[Board: moveLeft");

        if( touchLeft() )
        {
            //TODO 
        }
        else
        {
            // move left 
            m_inCoroutine = true;
            StartCoroutine("moveLeftOneGrid");
        }
    }

    protected IEnumerator moveLeftOneGrid()
    {
        yield return null;

        m_curBlockX--;

        Vector3 vec3 = m_curDomino3d.transform.localPosition;
        m_curDomino3d.gameObject.transform.localPosition = new Vector3(vec3.x - m_size, vec3.y, vec3.z);

        m_inCoroutine = false;
    }

    /// <summary>
    /// moveRight
    /// </summary>
    public void moveRight()
    {
        Debug.Log("[Board: moveRight");

        if( touchRight() )
        {
            //TODO 
        }
        else
        {
            // move right 
            m_inCoroutine = true;
            StartCoroutine("moveRightOneGrid");
        }
    }

    protected IEnumerator moveRightOneGrid()
    {
        yield return null;

        m_curBlockX++;

        Vector3 vec3 = m_curDomino3d.transform.localPosition;
        m_curDomino3d.gameObject.transform.localPosition = new Vector3(vec3.x + m_size, vec3.y, vec3.z);

        m_inCoroutine = false;
    }

    /// <summary>
    /// return the state of board 
    /// </summary>
	public int STATE
	{
        get
        {
            return m_state;
        }
	}

	/// <summary>
	/// return if the board is in coroutine or not 
	/// </summary>
	/// <value><c>true</c> if I n_ COROUTIN; otherwise, <c>false</c>.</value>
	public bool IN_COROUTINE
	{
		get
		{
			return m_inCoroutine;
		}
	}


    //------------------------ private function ----------------------------- 

    /// <summary>
    /// if the domino touch the left wall
    /// </summary>
    /// <returns></returns>
    protected bool touchLeft()
    {
		foreach( Grid2d g2 in m_curDomino2d.BLOCK_INFO )
		{
            int x = g2.m_x + m_curBlockX;
            int y = g2.m_y + m_curBlockY;

            int leftX = x - 1;

            if( leftX < 0 )
            {
                return true;
            }

            if( leftX >= m_width || y >= m_height )
            {
                continue;
            }

            Grid grid = m_allGrids[leftX, y];

            if( grid != null )
            {
                return true;
            }
		}

        return false;
    }

    /// <summary>
    /// if the domino touch the right wall 
    /// </summary>
    /// <returns></returns>
    protected bool touchRight()
    {
        foreach (Grid2d g2 in m_curDomino2d.BLOCK_INFO)
        {
            int x = g2.m_x + m_curBlockX;
            int y = g2.m_y + m_curBlockY;

            int rightX = x + 1;

            if (rightX >= m_width)
            {
                return true;
            }

            if (rightX < 0 || y >= m_height)
            {
                continue;
            }

            Grid grid = m_allGrids[rightX, y];

            if (grid != null)
            {
                return true;
            }
        }

        return false;
    }

	/// <summary>
	/// merge the domino to the ground 
	/// </summary>
	protected void mergeDomino()
	{
        m_state = STATE_MERGE;

		foreach( Grid2d g2 in m_curDomino2d.BLOCK_INFO )
        {
            int x = g2.m_x + m_curBlockX;
            int y = g2.m_y + m_curBlockY;

			// out of the range , game over 
			if( y >= m_height )
			{
				//TODO
				m_state = STATE_GAME_OVER;

				return;
			}

            // create the grid 
            GameObject go = (GameObject)Instantiate(m_gridTemplate);
            go.transform.parent = transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(x * m_size, y * m_size, 0.0f);

            // save the grid 
            Grid grid = go.GetComponent<Grid>();
			grid.m_x = x;
			grid.m_y = y;
            m_allGrids[x, y] = grid;

        }

        // destroy the domino3d 
        Destroy(m_curDomino3d.gameObject);
        m_curDomino3d = null;

        SePlayer.m_instance.PlayDong();

        // switch state 
        judgeBoard();
	}

    /// <summary>
    /// judge if any line can be remove 
    /// </summary>
    protected void judgeBoard()
    {
        m_state = STATE_JUDGE;

        List<int> clearLine = new List<int>();

        for( int i = 0; i < m_height; i++ )
        {
            int cnt = 0;

            // count the line 
            for( int j = 0; j < m_width; j++ )
            {
                if( m_allGrids[j,i] != null )
                {
                    cnt++;
                }
            }

            // if need to be clear 
            if( cnt == m_width )
            {
                clearLine.Add(i);
            }
        }

        // switch state 
        if( clearLine.Count > 0 )
        {
            m_clearLines = clearLine;
            cleanLines();
        }
        else
        {
            m_state = STATE_IDLE;
        }

    }

    /// <summary>
    /// clean lines that can be remove 
    /// </summary>
    protected void cleanLines()
    {
        m_state = STATE_CLEAN;

        StartCoroutine("removeCleanableLines");
    }

    /// <summary>
    /// return the removed lines 
    /// </summary>
    public int CLEAN_LINES
    {
        get
        {
            return m_clearLines.Count;
        }
    }

    protected IEnumerator removeCleanableLines()
    {
        List<Grid> gridList = new List<Grid>();
        Grid grid = null;
        int i;

        for( i = 0; i < m_clearLines.Count; i++ )
        {
			int line = m_clearLines[i];
            for( int j = 0; j < m_width; j++ )
            {
				grid = m_allGrids[j, line];
                gridList.Add( grid );

                grid.Flicker();
				m_allGrids[j, line] = null;
            }
        }

        yield return new WaitForSeconds(Grid.EFFECT_TIME);

        // remove the grids 
        foreach( Grid g in gridList )
        {
            Destroy(g.gameObject);
        }

        SePlayer.m_instance.PlayBreak();

        // display the feedback 
        int clearCnt = m_clearLines.Count;
        if( clearCnt == 1 )
        {
            m_resultEffect.Show(ResultEffect.RESULT_COOL);
        }
        else if( clearCnt >= 2 || clearCnt <= 3 )
        {
            m_resultEffect.Show(ResultEffect.RESULT_GREAT);
        }
        else
        {
            m_resultEffect.Show(ResultEffect.RESULT_PERFECT);
        }

        yield return null;

        // down other grids 
        for (i = m_clearLines.Count - 1; i >= 0; i-- )
        {
            int blankLineIdx = m_clearLines[i];

            for( int j = blankLineIdx + 1; j < m_height; j++ )
            {
                // move down one grid 
                for( int k = 0; k < m_width; k++ )
                {
                    Grid g = m_allGrids[k, j];

                    if( g != null )
                    {
                        // set the grid info 
                        m_allGrids[k, j] = null;
                        m_allGrids[k, j - 1] = g;

                        // move the display grid 
                        g.transform.localPosition = new Vector3(k*m_size, (j-1)*m_size, 0.0f);
                    }
                }
            }
        }

        m_state = STATE_IDLE;
    }

    /// <summary>
    /// calculate the position fit to the domino
    /// </summary>
    protected void calculateSmartInitPos()
    {
        // calculate the init X pos 
        m_curBlockX = 4;			//[TEMP]

        // calculate the init Y pos 
        int yOffset = 0;
        foreach( Grid3d g3 in m_curDomino3d.BLOCK_INFO )
        {
            if( g3.m_y < yOffset )
            {
                yOffset = g3.m_y;
            }
        }

        m_curBlockY = m_height - yOffset;
    }

    /// <summary>
    /// add the domino to the board 
    /// </summary>
    protected void addDominoToTheBoard()
    {
        m_curDomino3d.transform.parent = transform;
        m_curDomino3d.transform.localScale = new Vector3(m_size, m_size, m_size);
        m_curDomino3d.transform.localPosition = new Vector3(m_size * m_curBlockX, m_size * m_curBlockY, m_dorpDominoZ);
    }

    /// <summary>
    /// if domino touch the ground 
    /// </summary>
    /// <returns></returns>
    protected bool touchTheGround()
    {
        foreach( Grid2d g2 in m_curDomino2d.BLOCK_INFO )
        {
            int x = g2.m_x + m_curBlockX;
            int y = g2.m_y + m_curBlockY;

            int downY = y - 1;

            if( downY < 0 )
            {
                return true;
            }
            
            if( downY >= m_height )
            {
                continue;
            }

            if( x < 0 || x >= m_width )
            {
                Debug.LogError("[Board]: touchTheGround, domino position error.");
            }

            Grid grid = m_allGrids[x, downY];

            if( grid != null )
            {
                return true;
            }

        }

        return false;
    }

    /// <summary>
    /// return the Y position of the domino can land at 
    /// </summary>
    /// <returns></returns>
    int getLandPosition()
    {
        int y = m_curBlockY;

        for (; y >= 0; y-- )
        {
            bool hit = false;

            foreach( Grid2d g2 in m_curDomino2d.BLOCK_INFO )
            {
                int fx = g2.m_x + m_curBlockX;
                int fy = g2.m_y + y - 1;

				if( fy >= m_height )
				{
					continue;
				}

                if( fy < 0 )
                {
                    hit = true;
                    break;
                }
                else if( m_allGrids[fx,fy] != null )
                {
                    hit = true;
                    break;
                }
            }

            if( hit )
            {
                break;
            }
        }

        Debug.Log( "[Board]: getLandPosition => " + y );

        return y;
    }


    /// <summary>
    /// if can switch x 
    /// </summary>
    /// <returns></returns>
    protected bool canSwitchX()
    {
        //TODO 

        return true;
    }

    /// <summary>
    /// if can switch y
    /// </summary>
    /// <returns></returns>
    protected bool canSwitchY()
    {
        //TODO 

        return true;
    }

    /// <summary>
    /// if can switc z 
    /// </summary>
    /// <returns></returns>
    protected bool canSwitchZ()
    {
        //TODO 

        return true;
    }

}
