using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
    public Board m_board;
    public GameObject[] m_templetes;
    public UILabel m_txtScore;
    public PreviewNode m_previewNode;

    protected float m_time;
    protected Domoni3d m_pendingDomino;
    protected bool m_addScore;

	// Use this for initialization
	void Start () 
    {
        m_time = 0.0f;
        m_addScore = false;

        genPendingDomino();
        m_previewNode.AddDomino3d(m_pendingDomino);

        GlobalWork.SharedInstance.SCORE = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_board.STATE == Board.STATE_DROP)
        {
            m_time += Time.deltaTime;

            if (m_time > 0.72f)
            {
                m_time = 0.0f;

                m_board.Tick();
            }
        }
        else if( m_board.STATE == Board.STATE_IDLE )
        {
            m_previewNode.Clean();
            dropDomino();

            genPendingDomino();
            m_previewNode.AddDomino3d(m_pendingDomino);

            m_addScore = false;
        }
        else if (m_board.STATE == Board.STATE_CLEAN && !m_addScore )
        {
            int scoreInc = 0;
            int clearLines = m_board.CLEAN_LINES;
            
            if( clearLines == 1 )
            {
                scoreInc = 1;
            }
            else if( clearLines == 2 )
            {
                scoreInc = 3;
            }
            else if( clearLines == 3 )
            {
                scoreInc = 5;
            }
            else if( clearLines == 4 )
            {
                scoreInc = 7;
            }

            GlobalWork.SharedInstance.SCORE = GlobalWork.SharedInstance.SCORE + scoreInc;
            m_txtScore.text = GlobalWork.SharedInstance.SCORE + "";

            m_addScore = true;
        }
        else if( m_board.STATE == Board.STATE_GAME_OVER )
        {
            StartCoroutine("QuitToGameOver");
        }
	}

    //---------------------------- private function ------------------------------

    /// <summary>
    /// go to game over scene 
    /// </summary>
    /// <returns></returns>
    protected IEnumerator QuitToGameOver()
    {
		SePlayer.m_instance.PlayOver();

        yield return new WaitForSeconds(2.0f);

        TransScene.Load("GameOver", 1.0f);
    }

    /// <summary>
    /// drop a domino 
    /// </summary>
    protected void dropDomino()
    {
        m_pendingDomino.Normalize();

        // random raotate 
        if (m_pendingDomino.m_stable == false)
        {
            int i;

            int times = Random.Range(0, 3);
            for (i = 0; i < times; i++)
            {
                m_pendingDomino.SwitchX();
            }

            times = Random.Range(0, 3);
            for (i = 0; i < times; i++)
            {
                m_pendingDomino.SwitchY();
            }

            times = Random.Range(0, 3);
            for (i = 0; i < times; i++)
            {
                m_pendingDomino.SwitchZ();
            }
        }

        m_board.DropDomino(m_pendingDomino);
    }


    /// <summary>
    /// generate a random domino 
    /// </summary>
    /// <returns></returns>
    protected GameObject genPendingDomino()
    {
        GameObject go = null;

        go = (GameObject)Instantiate(m_templetes[Random.Range(0, m_templetes.Length)]);

        // save to pending 
        Domoni3d d3 = go.GetComponent<Domoni3d>();
        m_pendingDomino = d3;

        return go;
    }

}
