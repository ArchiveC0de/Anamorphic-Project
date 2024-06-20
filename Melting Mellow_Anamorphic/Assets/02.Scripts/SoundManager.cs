using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip MainBGM;
    public AudioClip NeonBGM;
    public AudioClip ButtonBGM;
    public AudioClip ShoutterBGM;

    private List<AudioSource> audioSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            for (int i = 0; i < 4; i++) // 필요에 따라 AudioSource의 수를 조정할 수 있습니다.
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSources.Add(audioSource);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        // 모든 AudioSource가 재생 중인 경우, 첫 번째 AudioSource를 사용 (필요에 따라 조정 가능)
        return audioSources[0];
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        AudioSource audioSource = GetAvailableAudioSource();
        audioSource.clip = clip;
        audioSource.loop = loop; // loop 설정
        audioSource.Play();
    }

    public void StopAllSounds()
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    #region 사운드 플레이
    public void MainSoundPlay()
    {
        PlaySound(MainBGM, true); // MainBGM 무한 반복
    }

    public void NeonSoundPlay()
    {
        PlaySound(NeonBGM);
    }

    public void ButtonSoundPlay()
    {
        PlaySound(ButtonBGM);
    }

    public void ShoutterSoundPlay()
    {
        PlaySound(ShoutterBGM);
    }
    #endregion

    #region 사운드 종료
    public void MainSoundStop()
    {
        StopSpecificSound(MainBGM);
    }

    public void NeonSoundStop()
    {
        StopSpecificSound(NeonBGM);
    }

    public void ButtonSoundStop()
    {
        StopSpecificSound(ButtonBGM);
    }

    public void ShoutterSoundStop()
    {
        StopSpecificSound(ShoutterBGM);
    }

    private void StopSpecificSound(AudioClip clip)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying && audioSource.clip == clip)
            {
                audioSource.Stop();
                break;
            }
        }
    }
    #endregion
}
