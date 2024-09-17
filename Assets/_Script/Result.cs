using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float res = Timer.ElapsedTime;

        String mes = "Your Time is "+ res.ToString("0.00") +" !!!";

        GetComponent<TextMeshProUGUI>().text = mes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
