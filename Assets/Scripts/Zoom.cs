using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {
	[Range (0, 3)]
	public float zoom =2f;
	public static bool zoomIn;


	// Use this for initialization
	void Start () {
		zoomIn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0) && (MovePlayer.lookObject)) // Input.GetButtonUp("Tap") && !(MovePlayer.lookAtMarker))
		{
			Vector3 pos = transform.position;
			if (!zoomIn)
			{
				pos = transform.position / zoom;
				pos.y = 2f;
			}
			else
			{
				pos = transform.position * zoom;
				pos.y = 2f;
				Vector3 look = new Vector3(0f, pos.y, 0f);
				transform.LookAt(look);
			}
			transform.position = pos;
			zoomIn = !zoomIn;
		}
	}

}
