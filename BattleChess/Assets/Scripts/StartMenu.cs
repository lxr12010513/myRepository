using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //��ʼ�˵�
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
