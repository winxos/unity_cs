using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class wvv_event : MonoBehaviour
{
    public wvv_audio_manager wam;
    public Animator animator;
    // Use this for initialization
    public GameObject effect;
    public GameObject actor;
    public Camera arcamera;
    private bool is_show_actor_finished = false;
    private bool is_found = false;
    public GameObject moshu_maozi;
    public GameObject moshu_maozi_effect;
    public GameObject[] moshu_tuzi;
    public GameObject joy_stick;

    void Start()
    {
        wam = GameObject.Find("scripts").GetComponent<wvv_audio_manager>();
        effect.SetActive(false);
        actor.SetActive(false);
    }
    void distance_check()
    {
        Vector3 vector = wvv_utils.distance_between_camera_and_target(arcamera, actor);
        if (vector.y < 0)
        {
            actor.SetActive(false);
        }
        else
        {
            actor.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (is_found && is_show_actor_finished)
        {
            distance_check();
        }
    }
    private void show_actor()
    {
        actor.SetActive(true);
        is_show_actor_finished = true;
        StartCoroutine(WaitDoAction(1, () =>
        {
            effect.SetActive(false);
            wam.play_appear_audio();
        }));

    }
    public void found()
    {
        print("found");
        effect.SetActive(true);
        is_show_actor_finished = false;
        is_found = true;
        StartCoroutine(WaitDoAction(2, show_actor));
    }
    public void lost()
    {
        print("lost");
        wam.stop_audio();
        is_found = false;
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
        joy_stick.SetActive(false);
    }

    public void show_ask_menu()
    {
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel");
        t.SetActive(true);
    }
    private void clear_lamp()
    {
        GameObject l0 = GameObject.Find("ImageTarget/Canvas/TextPanel/btn0/bulb0/Point light");
        GameObject l1 = GameObject.Find("ImageTarget/Canvas/TextPanel/btn1/bulb1/Point light");
        GameObject l2 = GameObject.Find("ImageTarget/Canvas/TextPanel/btn2/bulb2/Point light");
        GameObject l3 = GameObject.Find("ImageTarget/Canvas/TextPanel/btn3/bulb3/Point light");
        l0.SetActive(false);
        l1.SetActive(false);
        l2.SetActive(false);
        l3.SetActive(false);
    }
    public void click_very_satisfy()
    {
        clear_lamp();
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel/btn0/bulb0/Point light");
        t.SetActive(true);
        wam.play_replay_audio(0);
        animator.CrossFade("shuohua", 0.5f);
        StartCoroutine(WaitDoAction(6, () =>
        {
            animator.CrossFade("tuomasi", 0.5f);
        }));
    }
    public void click_satisfy()
    {
        clear_lamp();
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel/btn1/bulb1/Point light");
        t.SetActive(true);
        wam.play_replay_audio(1);
        animator.CrossFade("shuohua", 0.5f);
        StartCoroutine(WaitDoAction(10, () =>
        {
            animator.CrossFade("kongfan", 0.5f);
        }));
        StartCoroutine(WaitDoAction(13, () =>
        {
            wam.play_bye_audio(0);
        }));
    }
    public void click_normal()
    {
        clear_lamp();
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel/btn2/bulb2/Point light");
        t.SetActive(true);
        wam.play_replay_audio(2);
        animator.CrossFade("shuohua", 0.5f);
        StartCoroutine(WaitDoAction(12, () =>
        {
            animator.CrossFade("moshu", 0.5f);
            moshu_maozi.SetActive(true);
            StartCoroutine(WaitDoAction(3, () =>
            {
                moshu_maozi_effect.SetActive(true);
                StartCoroutine(WaitDoAction(0.5f, () =>
                {
                    moshu_tuzi[0].SetActive(true);
                    moshu_tuzi[1].SetActive(true);

                }));
            }));
        }));
        StartCoroutine(WaitDoAction(20, () =>
        {
            moshu_maozi.SetActive(false);
            moshu_tuzi[0].SetActive(false);
            moshu_tuzi[1].SetActive(false);
            moshu_maozi_effect.SetActive(false);
            animator.CrossFade("daiji", 0.5f);
            wam.play_bye_audio(1);
        }));
    }
    public void click_not_good()
    {
        clear_lamp();
        GameObject t = GameObject.Find("ImageTarget/Canvas/TextPanel/btn3/bulb3/Point light");
        t.SetActive(true);
        wam.play_replay_audio(3);
        animator.CrossFade("shuohua", 0.5f);
        StartCoroutine(WaitDoAction(7, () =>
        {
            animator.CrossFade("zoulu", 0.5f);
            joy_stick.SetActive(true);
        }));
    }
    public static IEnumerator WaitDoAction(float t, System.Action action)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(t);
        action();
    }
}
