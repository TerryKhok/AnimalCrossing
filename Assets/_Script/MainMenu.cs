using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void LoadScene(string str){
    SceneManager.LoadScene(str);
   }

   public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        FindObjectOfType<AudioManager>().Stop("Title");
        FindObjectOfType<AudioManager>().Play("Game");
        SceneManager.LoadScene("Stage2");
    }

    public void Title()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        FindObjectOfType<AudioManager>().Stop("Game");
        FindObjectOfType<AudioManager>().Play("Title");
        SceneManager.LoadScene("Title");
    }
   
   public void EndGame(){
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
   }
}
