using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // speed up the drop 
        if( Input.GetKeyDown(KeyCode.DownArrow) )
        {
            SendMessage("speedDrop");
        }

        // move to left 
        if( Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            SendMessage("moveLeft");
        }

        // move to right 
        if( Input.GetKeyDown( KeyCode.RightArrow))
        {
            SendMessage("moveRight");
        }

        // rotate X
        if( Input.GetKeyDown(KeyCode.Z) )
        {
            SendMessage("rotateX");
        }

        // rotate y
        if( Input.GetKeyDown(KeyCode.X) )
        {
            SendMessage("rotateY");
        }

        // rotate z 
        if( Input.GetKeyDown(KeyCode.C) )
        {
            SendMessage("rotateZ");
        }

	}
}
