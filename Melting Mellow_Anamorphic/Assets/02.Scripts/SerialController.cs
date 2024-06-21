using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialController : MonoBehaviour
{
    public SoundManager SoundManager;

    public string portName = "COM3"; // �Ƶ��̳밡 ����� ��Ʈ ��ȣ
    public int baudRate = 9600;
    private SerialPort serialPort;
    public GameObject[] targetObjects; // �ִϸ��̼��� ������ ������Ʈ �迭
    private Animator[] animators;
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
                Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�: " + targetObjects[i].name);
                return;
            }
        }

        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 1000; // Ÿ�Ӿƿ� ����
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
                    Debug.Log("���");
                    PlayAnimations();
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
                // Ÿ�Ӿƿ� ���� ó��
            }
            catch (Exception ex)
            {
                Debug.LogError("�ø��� ��� ����: " + ex.Message);
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
        SoundManager.Instance.StopAllCoroutines();
        SoundManager.Instance.MainSoundStop();
    }

    void OnApplicationQuit()
    {
        keepReading = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // �����尡 ����� ������ ���
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
}
