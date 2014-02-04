using UnityEngine;
using System.Collections;

public class JoystickController : MonoBehaviour 
{
    public Controller m_controller;

	// Use this for initialization
	void Start () 
    {	
	}

    /// <summary>
    /// move joystick 
    /// </summary>
    /// <param name="move"></param>
    public void On_JoystickMoveEnd(MovingJoystick move)
    {
        Debug.Log(move.joystickValue);

        if( move.joystickName == "MoveJoystick" )
        {
            //TODO 
        }
        else
        {
            //TODO 
        }
    }

}
