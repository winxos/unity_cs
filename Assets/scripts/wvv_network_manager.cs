/*
file:wvv_network_manager.cs
created:wvv
date:20170918
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
public class wvv_network_manager : MonoBehaviour
{
    public static string app_version = "0.1.0";
    public static string server_update_json = "http://localhost/ar_card.json";
    public static string server_url = "http://192.168.8.175";
    public static string server_route_api = "api/ar/v1/statistical";
    public static string uuid;
    private string update_url = "";
    public GameObject canvas;
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("uuid"))
        {
            uuid = System.Guid.NewGuid().ToString("N");
            PlayerPrefs.SetString("uuid", uuid);
        }
        else
        {
            uuid = PlayerPrefs.GetString("uuid");
        }
        if (Application.internetReachability == NetworkReachability.NotReachable) //no net
        {
            Debug.Log("No net.");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) //wan
        {
            Debug.Log("Use Wan.");
            online_post();
            update_check();
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork) //mobile data
        {
            Debug.Log("Use data.");
            online_post();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void open_latest_url()
    {
        if (update_url != "")
        {
            Application.OpenURL(update_url);
        }
    }
    public void prompt_ok()
    {
        canvas.GetComponent<MenuManager>().GoBack();
    }
    public void update_check()
    {
        this.StartCoroutine(wvv_network.download_file(server_update_json, (value) =>
        {
            if (value == "") //server json file get error.
            {
                return;
            }
            JsonData jd = JsonMapper.ToObject(value);
            if (((IDictionary)jd).Contains("version")) //valid check.
            {
                Debug.Log("v:" + jd["version"]);
                System.Version v_local = new System.Version(app_version);
                System.Version v_server = new System.Version(jd["version"].ToString());
                if (v_server > v_local)
                {
#if UNITY_IOS
                    update_url=jd["ios_latest"].ToString();
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
                    update_url = jd["android_latest"].ToString();
#endif
                    canvas.GetComponent<MenuManager>().GoToMenu(canvas.transform.Find("update").gameObject);
                }
            }
            else
            {
                Debug.LogError("Json format error. Should contain key [version]");
                Debug.Log(value);
            }
            if (((IDictionary)jd).Contains("prompt_message"))
            {
                Debug.Log("prompt:" + jd["prompt_message"]);
                if (jd["prompt_message"].ToString().Trim() != "") //not blank
                {
                    Text t = (Text)canvas.transform.Find("prompt/Text").gameObject.GetComponent<Text>();
                    t.text = jd["prompt_message"].ToString();
                    canvas.GetComponent<MenuManager>().GoToMenu(canvas.transform.Find("prompt").gameObject);
                }
            }

        }));
    }
    public void online_post() //改用代理实现
    {
        print("online post");
        this.StartCoroutine(wvv_network.Post(server_url + "/" + server_route_api, build_post_data("online"), (request) =>
          {
              Debug.Log("Status Code: " + request.responseCode);
              if (request.responseCode == 200)
              {
                  string text = request.downloadHandler.text;
                  Debug.Log(text);
              }
          }
        ));
    }
    public void offline_post() //改用代理实现
    {
        print("offline");
        this.StartCoroutine(wvv_network.Post(server_url + "/" + server_route_api, build_post_data("offline"), (request) =>
          {
              Debug.Log("Status Code: " + request.responseCode);
              if (request.responseCode == 200)
              {
                  string text = request.downloadHandler.text;
                  Debug.Log(text);
              }
          }
        ));
    }
    private string build_post_data(string status)
    {
        JsonData jsonData = new JsonData();
        jsonData["uuid"] = uuid;
        jsonData["status"] = status;
        jsonData["time_info"] = DateTime.Now.ToString("yyyyMMddhhmmss");
        jsonData["device_info"] = SystemInfo.deviceModel;
        jsonData["sys_info"] = SystemInfo.operatingSystem;
        jsonData["app_version"] = app_version;
        print(jsonData.ToJson());
        return jsonData.ToJson();
    }
    void OnApplicationQuit()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork ||
        Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork) //wan and mobile data
        {
            offline_post();
        }
    }
}
