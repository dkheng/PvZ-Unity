using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrid : MonoBehaviour
{
    #region ����

    public int row;   //�ڵڼ���

    GameObject toBePlanted;   //To Be Planted����
    GameObject selectedShovel;        //SelectedShovel����

    SpriteRenderer spriteRenderer;  //����SpriteRenderer���
    AudioSource audioSource;   //����AudioSource���

    bool havePlanted = false;   //�ø��Ƿ�����ֲֲ��
    GameObject nowPlant;    //��ǰ����ֲ��

    #endregion

    #region ϵͳ��Ϣ

    private void Awake()
    {
        //��ȡ���������
        toBePlanted = GameObject.Find("To Be Planted");
        selectedShovel = GameObject.Find("SelectedShovel");

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if(havePlanted == false && toBePlanted.activeSelf == true)
        {
            spriteRenderer.sprite = toBePlanted.GetComponent<SpriteRenderer>().sprite;
        }
        else if(havePlanted == true && selectedShovel.activeSelf == true)
        {
            nowPlant.GetComponent<Plant>().highlight();
        }
    }

    private void OnMouseExit()
    {
        if (havePlanted == false && toBePlanted.activeSelf == true)
        {
            spriteRenderer.sprite = null;
        }
        else if (havePlanted == true && selectedShovel.activeSelf == true)
        {
            nowPlant.GetComponent<Plant>().cancelHighlight();
        }
    }

    private void OnMouseDown()
    {
        if (havePlanted == false && toBePlanted.activeSelf == true)
        {
            plant(toBePlanted.GetComponent<ToBePlanted>().plantName);
        }
        else if (havePlanted == true && selectedShovel.activeSelf == true)
        {
            nowPlant.GetComponent<Plant>().die("shovelPlant");
        }
    }

    #endregion

    #region ˽���Զ��庯��

    #endregion

    #region �����Զ��庯��

    public void plant(string name)
    {
        spriteRenderer.sprite = null;   //������Ӱ
        havePlanted = true;   //����ֲ��

        //����ֲ��
        nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
                                transform.position + new Vector3(0, 0, 5),
                                Quaternion.Euler(0, 0, 0),
                                transform);
        nowPlant.GetComponent<Plant>().initialize(
            this,
            spriteRenderer.sortingLayerName,
            spriteRenderer.sortingOrder
        );

        //������Ч
        audioSource.clip =
            Resources.Load<AudioClip>("Sounds/UI/SeedAndShovelBank/plant");
        audioSource.Play();

        //��PlantingManagement������Ϣ�Դ���UI����¼�
        GameObject.Find("Planting Management").GetComponent<PlantingManagement>().plant();

    }

    //�ϵ�ģʽ��ֲ�����ڹؿ���ʼ�Ի����ɲ���Ի���ֲ��
    public GameObject plantByGod(string name)
    {
        havePlanted = true;   //����ֲ��

        //����ֲ��
        nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
                                          transform.position + new Vector3(0, 0, 5),
                                          Quaternion.Euler(0, 0, 0),
                                          transform);
        nowPlant.GetComponent<Plant>().initialize(
            this,
            spriteRenderer.sortingLayerName,
            spriteRenderer.sortingOrder
        );

        return nowPlant;
    }

    public void plantDie(string reason)
    {
        havePlanted = false;   //��û��ֲ��

        AudioClip clip = null;
        if (reason != "") clip = Resources.Load<AudioClip>("Sounds/Plants/" + reason);
        if (clip != null)
        {
            //������Ч
            audioSource.clip = clip; 
            audioSource.Play();
        }
    }

    #endregion
}
