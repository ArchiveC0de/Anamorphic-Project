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
    public AudioClip AnamorphicBGM;

    private List<AudioSource> audioSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            for (int i = 0; i < 4; i++) // �ʿ信 ���� AudioSource�� ���� ������ �� �ֽ��ϴ�.
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
        // ��� AudioSource�� ��� ���� ���, ù ��° AudioSource�� ��� (�ʿ信 ���� ���� ����)
        return audioSources[0];
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        AudioSource audioSource = GetAvailableAudioSource();
        audioSource.clip = clip;
        audioSource.loop = loop; // loop ����
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

    #region ���� �÷���
    public void MainSoundPlay()
    {
        PlaySound(MainBGM, true); // MainBGM ���� �ݺ�
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

    public void AnamolphicSoundPlay()
    {
        PlaySound(AnamorphicBGM);
    }
    #endregion

    #region ���� ����
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

    public void AnamolphicSoundStop()
    {
        StopSpecificSound(AnamorphicBGM);
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
