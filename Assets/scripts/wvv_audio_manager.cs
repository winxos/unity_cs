using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wvv_audio_manager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip appear, ask;
    public AudioClip[] reply;
    public AudioClip[] bye;
    // Use this for initialization
    void Start()
    {

    }
    public void stop_audio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    public void play_appear_audio()
    {
        // AudioSource.PlayClipAtPoint(appear, Vector3.zero, 1.0f);
        stop_audio();
        audioSource.PlayOneShot(appear);
    }
    public void play_ask_audio()
    {
        stop_audio();
        audioSource.PlayOneShot(ask);
    }
    public void show_ask_menu()
    {
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel");
        t.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void play_replay_audio(int n)
    {
        if (n > 3) return;
        audioSource.Stop();
        audioSource.PlayOneShot(reply[n]);
    }
    public void play_bye_audio(int n)
    {
        if (n > 2) return;
        audioSource.Stop();
        audioSource.PlayOneShot(bye[n]);
    }

}
