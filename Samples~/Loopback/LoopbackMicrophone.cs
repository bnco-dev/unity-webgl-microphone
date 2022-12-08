using UnityEngine;

namespace UnityWebGLMicrophone.Samples
{
    public class LoopbackMicrophone : MonoBehaviour
    {
        private AudioClip clip;
        private AudioSource audioSource;

        void Update()
        {
            if (clip == null)
            {
                clip = Microphone.Start("",true,10,16000);
            }

            if (clip == null)
            {
                return;
            }

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.PlayDelayed(0.1f);
            }

            var data = new float[clip.samples];
            clip.GetData(data,0);
            var pos = Microphone.GetPosition("");
            var vol = 0.0f;
            for(int i = pos - 512; (i < data.Length) && (i < pos) && (i >= 0); i++)
            {
                vol += Mathf.Abs(data[i]);
            }
            Debug.Log(vol / 512);
        }

        void OnDestroy()
        {
            if (clip != null)
            {
                Destroy(clip);
                clip = null;
            }

            if (audioSource != null)
            {
                Destroy(audioSource);
                audioSource = null;
            }
        }
    }
}