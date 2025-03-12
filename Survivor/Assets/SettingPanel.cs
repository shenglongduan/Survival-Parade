using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public GameObject obj;

    public AudioSource audioSource; // ��ק�� AudioSource
    public Slider volumeSlider;     // ��ק�� Slider

    void Start()
    {
        // ��ʼ�� Slider ֵΪ��ǰ����
        volumeSlider.value = audioSource.volume;

        // ��ӻ�����ֵ�ı��¼�����
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // ��������ֵ�ı�ʱ���õķ���
    private void OnSliderValueChanged(float value)
    {
        audioSource.volume = value;
    }

    // ��ѡ�������������ã������˳���Ϸʱ��
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
