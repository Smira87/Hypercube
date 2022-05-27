using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    AudioSource[] mysounds;

    AudioSource starts;
    AudioSource works;

    // Start is called before the first frame update
    void Start()
    {
        mysounds = GetComponents<AudioSource>();

        starts = mysounds[0];
        works = mysounds[1];
        
    }

    public void PlayStarts()
    {
        starts.Play();
    }
    public void StopStarts()
    {
        starts.Stop();
    }
    public void PlayWorks()
    {
        works.Play();
    }
    public void StopWorks()
    {
        works.Stop();
    }
    // Update is called once per frame
   
}
