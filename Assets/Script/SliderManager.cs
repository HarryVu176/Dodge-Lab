using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    public GameObject _DifficultyHolder;
    // Start is called before the first frame update
    void Start()
    {
        _slider.value = PlayerPrefs.GetInt("srold");
        _sliderText.text = _slider.value.ToString();
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0");
        });
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("diff", (int)_slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
