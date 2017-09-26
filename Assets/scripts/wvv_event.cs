using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wvv_event : MonoBehaviour
{
    public wvv_audio_manager wam;
    public Animator animator;
    // Use this for initialization
    public GameObject effect;
    public GameObject actor;
    void Start()
    {
        wam = GameObject.Find("scripts").GetComponent<wvv_audio_manager>();
        effect.SetActive(false);
        actor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void show_actor()
    {
        actor.SetActive(true);

        StartCoroutine(WaitDoAction(1, () =>
        {
            effect.SetActive(false);
            wam.play_appear_audio();
        }));

    }
    public void found()
    {
        effect.SetActive(true);
        StartCoroutine(WaitDoAction(2, show_actor));
    }
    public void lost()
    {
        wam.stop_audio();
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel");
        t.SetActive(false);
        effect.SetActive(false);
        actor.SetActive(false);
    }
    private void show_obj(GameObject g)
    {
        // StartCoroutine(show_effect());
        Renderer[] rendererComponents = g.GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = g.GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }
    }


    private void hide_obj(GameObject g)
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
    }
    public void click_body()
    {
        wam.play_ask_audio();
        StartCoroutine(WaitDoAction(3, show_ask_menu));
    }
    public void show_ask_menu()
    {
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel");
        t.SetActive(true);
    }
    public void click_very_satisfy()
    {
        wam.play_replay_audio(0);
        animator.Play("zhuan");
    }
    public void click_satisfy()
    {
        wam.play_replay_audio(1);
        animator.Play("fan");
        StartCoroutine(WaitDoAction(10, () =>
        {
            wam.play_bye_audio(0);
        }));
    }
    public void click_normal()
    {
        wam.play_replay_audio(2);
        animator.Play("zhuan");
        StartCoroutine(WaitDoAction(12, () =>
        {
            wam.play_bye_audio(1);
        }));

    }
    public void click_not_good()
    {

        wam.play_replay_audio(3);
        animator.Play("fan");
        StartCoroutine(WaitDoAction(10, () =>
        {
            wam.play_bye_audio(2);
        }));
    }
    IEnumerator WaitDoAction(float t, System.Action action)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(t);
        action();
    }
}
