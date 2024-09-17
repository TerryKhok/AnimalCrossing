using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dance : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Result")
            _anim.SetBool("dancing", true);
        else
            _anim.SetBool("dancing", false); 
    }
}
