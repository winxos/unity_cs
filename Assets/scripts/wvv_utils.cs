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
