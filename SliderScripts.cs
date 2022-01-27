using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderScripts : MonoBehaviour
{
    private float slideSpeed;
    private float slideValue;
    [SerializeField]
    private Text slider;
    // Start is called before the first frame update
    void Start()
    {
        slideValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (slideValue == 0)
        {
            this.gameObject.GetComponent<Slider>().value += 0.9f * Time.deltaTime;
            if (this.gameObject.GetComponent<Slider>().value >= 1)
            {
                slideValue = 1;
            }
        }
        else if (slideValue == 1)
        {
            this.gameObject.GetComponent<Slider>().value -= 0.9f * Time.deltaTime;
            if (this.gameObject.GetComponent<Slider>().value <= 0)
            {
                slideValue = 0;
            }
        }
        SlideStart();
    }


    void SlideStart()
    {

        if (this.gameObject.GetComponent<Slider>().value < 0.2f)
        {
            slider.text = "x2";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.3f)
        {
            slider.text = "x3";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.4f)
        {
            slider.text = "x4";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.5f)
        {
            slider.text = "x5";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.6f)
        {
            slider.text = "x4";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.7f)
        {
            slider.text = "x3";
        }
        else if (this.gameObject.GetComponent<Slider>().value < 0.8f)
        {
            slider.text = "x2";
        }
        else if (this.gameObject.GetComponent<Slider>().value >= 0.9f)
        {
            slider.text = "x2";
        }
   
    }
}
