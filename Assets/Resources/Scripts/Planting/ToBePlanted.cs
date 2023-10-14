using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBePlanted : MonoBehaviour
{
    #region ����

    public string plantName;   //��ǰѡ��ֲ�������

    SpriteRenderer spriteRenderer;   //����SpriteRenderer���

    Vector3 mouseWorldPos;  //���λ��

    #endregion

    #region ϵͳ��Ϣ

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ��ǰ���λ��
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        //����ֲֲ��������ƶ�
        transform.position = mouseWorldPos;

        //����������������ɼ�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region ˽���Զ��庯��

    #endregion

    #region �����Զ��庯��

    public void showPlantPreview(string name)
    {
        plantName = name;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Plants/" + plantName);

        //��ȡ��ǰ���λ��
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        transform.position = mouseWorldPos;
        gameObject.SetActive(true);
    }

    #endregion
}
