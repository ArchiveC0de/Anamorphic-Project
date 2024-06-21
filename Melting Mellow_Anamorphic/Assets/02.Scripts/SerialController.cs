using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialController : MonoBehaviour
{
    public SoundManager SoundManager;

    public string portName = "COM3"; // 아두이노가 연결된 포트 번호
    public int baudRate = 9600;
    private SerialPort serialPort;
    public GameObject[] targetObjects; // 애니메이션을 실행할 오브젝트 배열
    public GameObject[] delayAni;
    private Animator[] animators;
    private Animator[] delayAni2;
    private Thread serialThread;
    private bool keepReading;
    private bool play = false;
    private Queue<string> serialQueue = new Queue<string>();

    void Start()
    {
        animators = new Animator[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            animators[i] = targetObjects[i].GetComponent<Animator>();
            if (animators[i] == null)
            {
                Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다: " + targetObjects[i].name);
                return;
            }
        }

        delayAni2 = new Animator[delayAni.Length];
        for (int i = 0; i < delayAni.Length; i++)
        {
            delayAni2[i] = delayAni[i].GetComponent<Animator>();
            if (delayAni2[i] == null)
            {
                Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다: " + delayAni[i].name);
                return;
            }
        }

        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 1000; // 타임아웃 설정
        serialPort.Open();

        keepReading = true;
        serialThread = new Thread(ReadSerial);
        serialThread.Start();
    }

    void Update()
    {
        lock (serialQueue)
        {
            while (serialQueue.Count > 0)
            {
                SoundManager.Instance.ButtonSoundPlay();
                string data = serialQueue.Dequeue();
                if (data == "1" )
                {
                    if (play == true)
                    {
                        StopAnimations();
                        play = false;
                        SoundManager.Instance.ShoutterSoundPlay();
                        return;
                    }
                    play = true;
                    Debug.Log("통신");
                    PlayAnimations();
                    Invoke("DelayAnimation", 2);
                }
            }
        }
    }

    void ReadSerial()
    {
        while (keepReading)
        {
            try
            {
                string data = serialPort.ReadLine();
                lock (serialQueue)
                {
                    serialQueue.Enqueue(data);
                }
            }
            catch (TimeoutException)
            {
                // 타임아웃 예외 처리
            }
            catch (Exception ex)
            {
                Debug.LogError("시리얼 통신 오류: " + ex.Message);
                keepReading = false;
            }
        }
    }

    void PlayAnimations()
    {
        SoundManager.Instance.NeonSoundPlay();
        SoundManager.Instance.ShoutterSoundPlay();
        foreach (var animator in animators)
        {
            animator.SetBool("PlayAnimation", true);
        }
        Invoke("MainSound",2);
    }

    void StopAnimations()
    {
        foreach(var animator in animators)
        {
            animator.SetBool("PlayAnimation", false);
        }
        foreach (var animator in delayAni2)
        {
            animator.SetBool("PlayAnimation", false);
        }
        SoundManager.Instance.StopAllCoroutines();
        SoundManager.Instance.MainSoundStop();
    }

    void OnApplicationQuit()
    {
        keepReading = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // 스레드가 종료될 때까지 대기
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    void MainSound()
    {
        SoundManager.Instance.MainSoundPlay();
    }

    void DelayAnimation()
    {
        foreach (var animator in delayAni2)
        {
            animator.SetBool("PlayAnimation", true);
        }
    }
}
