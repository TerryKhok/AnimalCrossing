using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _startTime;
    public static float ElapsedTime;
    private TextMeshProUGUI _tx;

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
       ElapsedTime = 0.0f;
        _tx=GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime = Time.time-_startTime;
        _tx.text = ElapsedTime.ToString("0");
    }
}
