using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeData : MonoBehaviour
{
    public TextMeshPro clockText;
 

    void Start()
    {

        UpdateTime();
        InvokeRepeating("UpdateTime", 0f, 1f);
    }

    void UpdateTime()
    {

        System.DateTime now = System.DateTime.Now;


        string timeString = now.ToString("HH:mm:ss");
   


        clockText.text = timeString;
      
    }
}
