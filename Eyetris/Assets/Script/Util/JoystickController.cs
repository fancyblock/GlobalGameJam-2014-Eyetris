using UnityEngine;
using System.Collections;

public class JoystickController : MonoBehaviour 
{
	public const float STICK_COOLDOWN_TIME = 0.35f;

    public Controller m_controller;

	protected float m_cooldownTime;

	// Use this for initialization
	void Start () 
    {	
		m_cooldownTime = 0.0f;
	}

	// update 
	void Update()
	{
		if( m_cooldownTime > 0.0f )
		{
			m_cooldownTime -= Time.deltaTime;

			if( m_cooldownTime < 0.0f )
			{
				m_cooldownTime = 0.0f;
			}
		}
	}

    /// <summary>
    /// move joystick 
    /// </summary>
    /// <param name="move"></param>
	public void On_JoystickMove(MovingJoystick move)
    {
		float angle = move.Axis2Angle();
		float len = move.joystickValue.magnitude;
		print (angle);
		if( len < 0.5f || m_cooldownTime > 0.0f )
		{
			return;
		}

        if( move.joystickName == "MoveJoystick" )
        {
            if( angle >= 80.0f && angle <= 100.0f )
			{
				m_controller.SendInput("moveRight");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
			else if( angle <= -80.0f && angle >= -100.0f )
			{
				m_controller.SendInput("moveLeft");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
			else if( angle >= 170.0f || angle <= -170.0f )
			{
				m_controller.SendInput("speedDrop");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
        }
        else
        {
            if( angle <= 10.0f && angle >= -10.0f )
			{
				m_controller.SendInput("rotateX");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
			else if( angle <= -80.0f && angle >= -100.0f )
			{
				m_controller.SendInput("rotateY");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
			else if( angle >= 80.0f && angle <= 100.0f )
			{
				m_controller.SendInput("rotateZ");
				m_cooldownTime = STICK_COOLDOWN_TIME;
			}
        }
    }

	/// <summary>
	/// move joystick end 
	/// </summary>
	/// <param name="move">Move.</param>
	public void On_JoystickMoveEnd(MovingJoystick move )
	{
		m_cooldownTime = 0.0f;
	}

}
