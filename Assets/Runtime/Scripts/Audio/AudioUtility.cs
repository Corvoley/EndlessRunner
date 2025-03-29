using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    public static void PlayAudioCue(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup == null)
        {
            Debug.LogError("Erro: Todo AudioSource deve ter um AudioMixerGroup assinalado");
        }
        else
        {
            source.pitch = Random.Range(0.98f, 1.03f);
            source.clip = clip;
            source.loop = false;
            source.Play();
        }
    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup == null)
        {
            Debug.LogError("Erro: Todo AudioSource deve ter um AudioMixerGroup assinalado");
        }
        else
        {
            source.pitch = 1f;
            source.clip = clip;
            source.loop = false;
            source.Play();
        }
            
    }


}
  
   

