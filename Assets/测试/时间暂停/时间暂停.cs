using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 时间暂停 : MonoBehaviour
{

    public bool 暂停;
    public bool 怪物减慢;
    public Animator monsterAnim;
    public AnimationState monsteranim;

    float currentSpeed = 0f;

    float m_MySliderValue;

    enum AnimSpeed
    {
        normal,

    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("5.5f : " + Mathf.Round(5.5f));
        Debug.Log("6.5f : " + Mathf.Round(6.5f));
        Debug.Log("7.5f : " + Mathf.Round(7.5f));
        Debug.Log("8.5f : " + Mathf.Round(8.5f));
    }

    

    void OnGUI()
    {
        Debug.Log("OnGUI");
        //Create a Label in Game view for the Slider
        GUI.Label(new Rect(0, 25, 40, 60), "Speed");
        //Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.

        m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        //Make the speed of the Animator match the Slider value
        //Time.timeScale = m_MySliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");

        if(暂停)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        //if(怪物减慢 && currentSpeed != monsterAnim.speed)
        //{
        //    monsterAnim.speed = 0.2f;
        //}
        //else
        //{
        //    monsterAnim.speed = 1f;
        //}
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
}
