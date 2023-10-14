using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���쳡�����������ֲ������������Ч��
//Ӧ������Planting Management������
public class Winter : MonoBehaviour
{
    private int times = 0;   //���䶳����

    private void Start()
    {
        Invoke("freezePlant", 40.0f);
    }

    private void freezePlant()
    {
        Plant[] plants = gameObject.GetComponentsInChildren<Plant>();
        if(plants.Length > 0)
        {
            plants[Random.Range(0, plants.Length)].cold();
        }
        times++;
        if(times >= 3) Invoke("freezePlant", 15.0f);
        else Invoke("freezePlant", 30.0f);
    }
}
