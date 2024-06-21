using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    public void Anamolphic()
    {
        SoundManager.Instance.AnamolphicSoundPlay();
        Debug.Log("아나몰픽 사운드 테스");
    }

    public void AnamolpgicStop()
    {
        SoundManager.Instance.AnamolphicSoundStop();
    }
}
