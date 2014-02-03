using UnityEngine;
using System.Collections;

public class GestureHandler : MonoBehaviour 
{
    public Controller m_controller;

	// Use this for initialization
	void Start () 
    {	
	}

    /// <summary>
    /// get the gesture 
    /// </summary>
    /// <param name="gesture"></param>
    void OnCustomGesture(PointCloudGesture gesture)
    {
        Debug.Log(gesture.RecognizedTemplate.name);

        switch( gesture.RecognizedTemplate.name )
        {
            case "move_left":
                m_controller.SendInput("moveLeft");
                break;
            case "move_right":
                m_controller.SendInput("moveRight");
                break;
            case "rotate_x":
                m_controller.SendInput("rotateX");
                break;
            case "rotate_y":
                m_controller.SendInput("rotateY");
                break;
            case "rotate_z":
                m_controller.SendInput("rotateZ");
                break;
            case "move_down":
                m_controller.SendInput("speedDrop");
                break;
            default:
                break;
        }
    }
}
