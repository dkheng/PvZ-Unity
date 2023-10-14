using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject shovelUI;   //����UI�����ƿɼ����

    Vector3 mouseWorldPos;   //���λ��

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   //�����ɼ�
    }

    // Update is called once per frame
    void Update()
    {
        //����ʼ�ո������
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;

        //����������������ɼ���UI�ɼ�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shovelUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void clickShovel()
    {
        //����UI���ɼ�
        shovelUI.SetActive(false);

        //����ɼ����������
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
        gameObject.SetActive(true);

        //������Ч
        GetComponent<AudioSource>().Play();
    }
}
