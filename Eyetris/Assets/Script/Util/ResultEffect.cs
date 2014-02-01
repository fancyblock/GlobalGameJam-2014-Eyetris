using UnityEngine;
using System.Collections;

public class ResultEffect : MonoBehaviour 
{
    public const int RESULT_COOL = 1;
    public const int RESULT_GREAT = 2;
    public const int RESULT_PERFECT = 3;

	public UISprite m_sprite;

	// Use this for initialization
	void Start () 
    {
        m_sprite.alpha = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    /// <summary>
    /// show result feedback 
    /// </summary>
    /// <param name="type"></param>
    public void Show( int type )
    {
        switch( type )
		{
            case RESULT_COOL:
                m_sprite.spriteName = "cool";
                break;
            case RESULT_GREAT:
                m_sprite.spriteName = "great";
                break;
            case RESULT_PERFECT:
                m_sprite.spriteName = "perfect";
                break;
            default:
                break;
		}

        m_sprite.MakePixelPerfect();

        TweenScale ts = TweenScale.Begin(gameObject, 0.23f, Vector3.one);
        ts.from = Vector3.zero;
        m_sprite.alpha = 1.0f;

        StartCoroutine("dismissResult");
    }

    protected IEnumerator dismissResult()
    {
        yield return new WaitForSeconds(1.2f);

        m_sprite.alpha = 0.0f;
    }
}
