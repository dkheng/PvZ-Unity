using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowKingSong : MonoBehaviour
{
    public static bool play = false;

    private int snowKingNum = 0;
    private Coroutine singCoroutine;
    private AudioSource audioSource;
    private GameObject plantingManagement;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        plantingManagement = GameObject.Find("Planting Management");
    }

    public void addSnowKing()
    {
        snowKingNum++;
        if (snowKingNum == 1)
        {
            singCoroutine = StartCoroutine(sing());
        }
    }

    public void subSnowKing()
    {
        snowKingNum--;
        if (snowKingNum == 0)
        {
            StopCoroutine(singCoroutine);
        }
    }

    private IEnumerator sing()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);

            audioSource.Play();
            Plant[] plants = plantingManagement.GetComponentsInChildren<Plant>();
            foreach(Plant plant in plants)
            {
                plant.intensify();
            }
            SnowKingSong.play = true;
            yield return new WaitForSeconds(25f);

            plants = plantingManagement.GetComponentsInChildren<Plant>();
            foreach (Plant plant in plants)
            {
                plant.cancelIntensify();
            }
            SnowKingSong.play = false;
        }
    }
}
