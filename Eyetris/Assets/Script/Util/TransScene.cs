using UnityEngine;
using System.Collections;

public class TransScene : MonoBehaviour 
{
	public UISprite m_sprMask;

    protected string m_targetLevel;
    protected float m_transformInterval;

    /// <summary>
    /// load scene 
    /// </summary>
    /// <param name="level"></param>
    static public TransScene Load(string level, float interval)
    {
        if (!Application.CanStreamedLevelBeLoaded(level))
        {
            Debug.LogError("[TransScene]: The level [" + level + "] can't be loaded.");

            return null;
        }

        GameObject go = (GameObject)Instantiate(Resources.Load<GameObject>("Prefab/Util/TransScene"));
        TransScene ft = go.GetComponent<TransScene>();
        ft.TARGET_LEVEL = level;
        ft.TRANSFORM_INTERVAL = interval;
        ft.startTrans();

        return ft;
    }

    /// <summary>
    /// initial 
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// setter && getter of the target level 
    /// </summary>
    public string TARGET_LEVEL
    {
        get
        {
            return m_targetLevel;
        }
        set
        {
            m_targetLevel = value;
        }
    }

    /// <summary>
    /// set the transform interval 
    /// </summary>
    public float TRANSFORM_INTERVAL
    {
        set
        {
            m_transformInterval = value;
        }
    }


    //------------------------- private functions ---------------------------


    /// <summary>
    /// trans to the target level 
    /// </summary>
    protected void startTrans()
    {
        m_sprMask.alpha = 0.0f;
        StartCoroutine("transformToLevel_default");
    }

    /// <summary>
    /// transform to the target level 
    /// </summary>
    /// <returns></returns>
    protected IEnumerator transformToLevel_default()
    {
        yield return null;

        TweenAlpha.Begin(m_sprMask.gameObject, m_transformInterval, 1.0f);
        while (m_sprMask.alpha < 1.0f)
        {
            yield return null;
        }

        AsyncOperation ao = Application.LoadLevelAsync(m_targetLevel);

        while (ao.isDone == false)
        {
            yield return null;
        }

        TweenAlpha.Begin(m_sprMask.gameObject, m_transformInterval, 0.0f);
        while (m_sprMask.alpha > 0.0f)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
