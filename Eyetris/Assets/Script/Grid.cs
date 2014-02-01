using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{
    public const float EFFECT_TIME = 1.0f;

    public SpriteRenderer m_sprite;
	public int m_x;
	public int m_y;

	// Use this for initialization
	void Start () 
    {
	}

    /// <summary>
    /// flicker effect 
    /// </summary>
	public void Flicker()
	{
        StartCoroutine("flickering");
	}

    //[HACK]            very hack 
    protected IEnumerator flickering()
    {
        int i;
        Color color = new Color(1.0f, 1.0f, 1.0f);

        for( i = 0; i < 12; i++ )
        {
            color.r -= 0.05f;
            color.g -= 0.05f;
            color.b -= 0.05f;

            m_sprite.color = color;

            yield return null;
        }

        for( i = 0; i < 12; i++ )
        {
            color.r += 0.05f;
            color.g += 0.05f;
            color.b += 0.05f;

            m_sprite.color = color;

            yield return null;
        }
    }

}
