using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {
	private AsyncOperation asyncOp;

	// Use this for initialization
	void Start () {
		StartCoroutine("load");

	}

	void Update()
	{
		/*if (Input.GetMouseButtonUp(0))
	    {
			asyncOp.allowSceneActivation = true;
			Destroy(GameObject.FindGameObjectWithTag("Select"));
		}*/
	}
	// Update is called once per frame

	IEnumerator load()
	{
		asyncOp = Application.LoadLevelAsync(1);
		asyncOp.allowSceneActivation = false;
		yield return asyncOp;
	}
}
