using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DeerDash : MonoBehaviour
{
    private float _speed; 
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb=GetComponent<Rigidbody>();
        _speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
        _rb.velocity = transform.forward * _speed;

        if(gameObject.transform.position.x<50||gameObject.transform.position.y>50){
            Object.Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.name == "Sparrow"){
            FindObjectOfType<AudioManager>().Play("Impact");
        }
    }
}
