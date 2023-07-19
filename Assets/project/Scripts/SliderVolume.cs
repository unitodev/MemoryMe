using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    private Slider m_Slider;

     [SerializeField] 
    private string _name;
    // Start is called before the first frame update
    void Start()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = PlayerPrefs.GetFloat(_name);
        AudioManager.Instance.SetVolumeMixer(_name, m_Slider.value);
        m_Slider.onValueChanged.AddListener(val=>
            AudioManager.Instance.SetVolumeMixer(_name,m_Slider.value));
    }
}
