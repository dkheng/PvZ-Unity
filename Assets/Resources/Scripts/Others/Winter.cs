using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//冬天场景组件，产生植物随机寒冷减速效果
//应挂载于Planting Management对象下
public class Winter : MonoBehaviour
{
    private int times = 0;   //已冷冻次数

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
