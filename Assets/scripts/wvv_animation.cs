using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wvv_animation : MonoBehaviour {
	public Animator animator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void zhuan()
	{
		animator.Play("zhuan");
	}
	public void fan()
	{
		animator.Play("fan");
	}
}
