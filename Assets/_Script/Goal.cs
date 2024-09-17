using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private string _charaName = "Sparrow";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.name == _charaName)
        {
            FindObjectOfType<AudioManager>().Stop("Game");
            FindObjectOfType<AudioManager>().Play("Victory");
            SceneManager.LoadScene("Result");
        }
    }
}
