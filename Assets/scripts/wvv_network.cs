/*
file:wvv_network.cs
created:wvv
date:20170918
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using LitJson;
public class wvv_network : MonoBehaviour
{


    public static string csrf_header_key = "ArCsrfToken";
    public static string csrf_pwd = "aabbcc";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    download file from the url.
    callback Action for data return
     */
    public static IEnumerator download_file(string url, System.Action<string> callback)
    {
        WWW w = new WWW(url);
        yield return w;
        if (w.error != null)
        {
            Debug.LogError(w.error);
            callback("");
        }
        else
        {
            callback(w.text);
        }
    }
    /*
    post data to the url using json.
    callback Action for data return
     */
    public static IEnumerator Post(string url, string data, System.Action<UnityWebRequest> callback)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] postBytes = Encoding.Default.GetBytes(data);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader(csrf_header_key, csrf_pwd); //csrf
        yield return request.Send();
        callback(request);
    }
}
