using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public GameObject obj;

    public AudioSource audioSource; // 拖拽绑定 AudioSource
    public Slider volumeSlider;     // 拖拽绑定 Slider

    void Start()
    {
        // 初始化 Slider 值为当前音量
        volumeSlider.value = audioSource.volume;

        // 添加滑动条值改变事件监听
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // 当滑动条值改变时调用的方法
    private void OnSliderValueChanged(float value)
    {
        audioSource.volume = value;
    }

    // 可选：保存音量设置（例如退出游戏时）
    private void OnDestroy()
    {
       
    }




    public void LoadMain()
    {

        SceneManager.LoadScene("GameStart");
    }


    public  void CloseGame()
    {
        Time.timeScale = 1;
        obj.SetActive(false);
    }
}
