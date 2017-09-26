using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using Vuforia; 
public class wvv_vumark : MonoBehaviour {

	string id;//用于显示ID  
	private VuMarkManager mVuMarkManager;  
	private VuMarkTarget mClosestVuMark;  

	// Use this for initialization
	void Start () {
		mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();  
	}

	// Update is called once per frame
	void Update()  
	{  
		UpdateClosestTarget();  
	}  

	void UpdateClosestTarget()  
	{  
		foreach(var bhvr in mVuMarkManager.GetActiveBehaviours())  
		{   //得到扫描到的vumark  
			mClosestVuMark = bhvr.VuMarkTarget;  
		}  

		if (mClosestVuMark != null) {  
			id = mClosestVuMark.InstanceId.NumericValue.ToString();  
			print(id);
		} else {
			id = "";
		}
	}  
}
