using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wvv_f : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static Vector3 distance_between_camera_and_target(Camera camera, GameObject target)
    {
        return camera.WorldToViewportPoint(target.transform.position);
    }
    void test()
    {
        Camera camera=null;
        GameObject gameObject=null;
        Vector3 vector = distance_between_camera_and_target(camera, gameObject);
		if(vector.y<-0.8)
		{
			gameObject.SetActive(false);
		}
		else{
			gameObject.SetActive(true);
		}
    }
}
