using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class wvv_utils : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }
    public void exit()
    {
        print("exit");
        Application.Quit();
    }

    // Update is called once per frame
    public static Vector3 distance_between_camera_and_target(Camera camera, GameObject target)
    {
        return camera.WorldToViewportPoint(target.transform.position);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exit();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}
