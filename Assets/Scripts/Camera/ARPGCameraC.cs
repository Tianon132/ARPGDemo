using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPGCameraC : MonoBehaviour
{
    public Transform target;
    public float targetHeight = 1.8f;//������ĸ߶�
    public float targetSide = -0.1f;
    public float distance = 4;//������֮��ľ���
    public float maxdistance = 8;
    public float mindistance = 2.2f;
    public float xSpeed = 250;
    public float ySpeed = 125;
    public float yMinLimit = -10;
    public float yMaxLimit = 72;
    public float zoomRate = 80;
    public float x = 20;
    public float y = 0;

    public InputC m_Input;
    //public Vector2 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Input = FindObjectOfType<InputC>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//�������
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()//һ�����������LateUpdate����ɫUpdate��֮��LateUpdate��������ƶ����������Ա��ⶶ��
    {
        x += m_Input.m_Camera.x * xSpeed * Time.deltaTime;
        y -= m_Input.m_Camera.y * ySpeed * Time.deltaTime;
        distance -= (m_Input.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, mindistance, maxdistance);

        y = ClampAngle(y, yMinLimit, yMaxLimit);
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        if (m_Input.m_Camera.x != 0 || m_Input.m_Camera.y != 0)
        {
            target.transform.rotation = Quaternion.Euler(0, x, 0);//�����������Ǻ������һ����ת
        }

        transform.position = target.position - (rotation * new Vector3(targetSide, 0, 1) * distance - new Vector3(0, targetHeight, 0));
    }

    //�Ƕȵ�����
    float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
