using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    private Slider m_Slider;

    [SerializeField] 
    private string name;
    // Start is called before the first frame update
    void Start()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = PlayerPrefs.GetFloat(name);
        AudioManager.Instance.SetVolumeMixer(name, m_Slider.value);
        m_Slider.onValueChanged.AddListener(val=>
            AudioManager.Instance.SetVolumeMixer(name,m_Slider.value));
    }
}
