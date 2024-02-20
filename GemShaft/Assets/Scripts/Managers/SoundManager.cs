using GemShaft.Data;
using UnityEngine;

namespace GemShaft.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundConfig config;
        [SerializeField] private AudioSource soundAudioSource;

        public void PlaySound(SoundType type)
        {
            var data = config.GetData(type);
            soundAudioSource.volume = data.Volume;
            soundAudioSource.clip = data.Clip;
            soundAudioSource.Play();
        }
    }
    
    
}
