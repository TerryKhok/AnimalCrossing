using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class RespawnChara : MonoBehaviour
{
    private string _charaName = "Marie_sum";

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == _charaName)
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            other.transform.position = new Vector3(-4.0f, 0.7f, 0.0f);
        }
    }
}
