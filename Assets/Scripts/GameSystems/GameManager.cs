using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(_instance);;
    }
    private void Start()
    {
        UI_Interface.Instance.RestartButtonEvent += Restart;
    }
    private void Update()
    {

    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
