using UnityEngine;
using UnityEngine.Audio;


namespace TFG_SP
{



    [System.Serializable]
    public class Sound
    {

        public string name;

        public AudioMixerGroup audioMixerGroup;

        public AudioClip[] clips;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(.1f, 3f)] public float pitch = 1f;
        [Range(0f, 1f)] public float spatialBlend;

        public bool loop;
        public bool mute;
        public bool playOnAwake;


        [HideInInspector] public AudioSource source;


        public AudioClip GetClip()
        {

            if (clips.Length > 0)
            {

                int choice = UnityEngine.Random.Range(0, clips.Length);

                return clips[choice];

            }
            else
            {

                return null;

            }
        }


    }
}
