using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevel0 : MonoBehaviour
{
    public GameObject zombieIntroduce;   //Ω© ¨ΩÈ…‹¿∏
    public GameObject plantIntroduce;    //÷≤ŒÔΩÈ…‹¿∏
 

    // Start is called before the first frame update
    void Start()
    {
        Invoke("showSnowPlantIntroduce", 1f);
    }

    private void showSnowZombieIntroduce()
    {
        zombieIntroduce.SetActive(true);
    }

    private void showSnowPlantIntroduce()
    {
        plantIntroduce.SetActive(true);
    }

    public void clickStartOfZombie()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/UI/graveButtonClick"),
                new Vector3(0, 0, -10)
            );
        zombieIntroduce.SetActive(false);

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }

    public void clickStartOfPlant()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/UI/buttonClick"),
                new Vector3(0, 0, -10)
            );
        plantIntroduce.SetActive(false);

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }

}
