using UnityEngine;
using UnityEngine.UI;

namespace UnityWebGLMicrophone.Samples
{
    public class LoopbackMicrophone : MonoBehaviour
    {
        private AudioClip clip;
        private AudioSource audioSource;

        void FixedUpdate()
        {
            Debug.Log($"Is Recording: {Microphone.IsRecording("")}");
        }
        
        void Update()
        {
            if (!clip)
            {
                // This will return null if the audio context has not started
                // up yet. This might happen if permissions have not been
                // granted in the browser, or if the user hasn't interacted
                // with the web page yet. So just keep trying until we get a
                // clip.
                clip = Microphone.Start("",true,5,16000);
                
                if (!clip)
                {
                    UpdateUI("Click to start loopback");
                    return;
                }
                
                if (clip)
                {
                    Debug.Log($"Clip Frequency: {clip.frequency}");
                    Debug.Log($"Clip Length: {clip.samples}");
                }
            }

            if (!audioSource)
            {
                // Create audio source and set audio source clip to microphone
                // clip. This creates the loopback.
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.PlayDelayed(0.1f);
            }

            var data = new float[clip.samples];
            clip.GetData(data,0);
            var pos = Microphone.GetPosition("");
            var vol = 0.0f;
            for(int i = pos - 2048; (i < data.Length) && (i < pos) && (i >= 0); i++)
            {
                vol += Mathf.Abs(data[i]);
            }
            vol /= 2048;
            Debug.Log($"Volume: {vol}");
            
            UpdateUI($"Connected! Volume:<color=yellow>{vol * 100:00}%</color>",vol);
        }
        
        void UpdateUI(string state, float volume = -1.0f)
        {
            var text = GetComponentInChildren<Text>();
            if (text)
            {
                text.text = state;
            }
            
            var slider = GetComponentInChildren<Slider>(includeInactive:true);
            if (volume < 0.0f)
            {
                slider.gameObject.SetActive(false);
                return;
            }
            
            slider.gameObject.SetActive(true);
            slider.value = volume;
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