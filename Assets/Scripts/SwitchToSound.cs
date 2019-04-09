using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SwitchToSound : MonoBehaviour
{
    //Distance that the sounds should start changing at.
    public int changeDistance;
    public AudioMixer musicMixer;
    public AudioMixerSnapshot first;
    public AudioMixerSnapshot second;

    private Plane plane;
    private GameObject player;
    private AudioMixerSnapshot[] snapshots;

    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane();
        plane.SetNormalAndPosition(this.transform.forward, this.transform.position);
        player = GameObject.FindGameObjectWithTag("Player");
        snapshots = new AudioMixerSnapshot[] { first, second };
    }

    // Update is called once per frame
    void Update()
    {
        float distance = plane.GetDistanceToPoint(player.transform.position);
        Debug.Log(distance);
        if (Mathf.Abs(distance) < changeDistance)
        {
            float ratio = Mathf.Abs(distance) / changeDistance;
            Debug.Log(ratio);
            if (distance < 0)
            {
                //Start making normal sound more quiet, forest sound louder.
                float weightSecond = .5f + .5f * ratio;
                float weightFirst = .5f * (1 - ratio);
                musicMixer.TransitionToSnapshots(snapshots, new float[] { weightFirst, weightSecond }, 0);
            }
            else
            {
                //Start making normal sound more quiet, forest sound louder.
                float weightFirst = .5f + .5f * ratio;
                float weightSecond = .5f * (1 - ratio);
                musicMixer.TransitionToSnapshots(snapshots, new float[] { weightFirst, weightSecond }, 0);
            }
        }
    }

}

