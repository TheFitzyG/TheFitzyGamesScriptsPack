using System;
using UnityEngine;


namespace TFG_SP
{

    //I would suggest using Fmod as it has a buttload more functionality. but this works well if you want to stay within Unity
    public class AudioManager : MonoBehaviour
    {

        public Sound[] sounds;

        public bool master;

        [HideInInspector] public static AudioManager Master;

        void Awake()
        {


            if (master)
            {

                if (Master == null)
                {

                    Master = this;

                }
                else
                {

                    DestroyImmediate(gameObject);
                    return;

                }


                DontDestroyOnLoad(gameObject);
            }

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.outputAudioMixerGroup = s.audioMixerGroup;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.mute = s.mute;
                s.source.playOnAwake = s.playOnAwake;

                if (s.playOnAwake)
                {

                    Play(s.name);

                }

            }
        }



        public void Play(string name)
        {

            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.clip = s.GetClip();

            s.source.Play();


        }

        public void Stop(string name)
        {

            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;

            }

            if (s.source.isPlaying)
            {

                s.source.Stop();
                Debug.Log("Sopped: " + name);

            }
            else
            {

                Debug.LogWarning("Sound: " + name + " is not playing");
                return;

            }


        }

        public void Pause(string name)
        {

            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;

            }

            if (s.source.isPlaying)
            {

                s.source.Pause();
                Debug.Log("Paused: " + name);

            }
            else
            {

                Debug.LogWarning("Sound: " + name + " is not playing");
                return;

            }




        }


        public void ChangePitch(string name, float newLevel, float max)
        {

            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;

            }


            s.source.pitch = newLevel;

            if (s.source.pitch > max)
            {

                s.source.pitch = max;

            }



        }

    }
}
