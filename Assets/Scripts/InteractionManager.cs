using UnityEngine;
using System.Collections;
using UnityEngine.Events;
//using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
	private Image	fillableImage;
	private bool	isActivated = false;

	// Use this for initialization
	void Start () {
		fillableImage = GetComponentInChildren<Image>();
		fillableImage.enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		fillableImage.enabled = isActivated;
	}
	
	public void activate()
	{
		isActivated = true;
	}

	public void desactivate()
	{
		isActivated = false;
	}
}

