using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject shovelUI;   //铲子UI，控制可见与否

    Vector3 mouseWorldPos;   //鼠标位置

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   //自身不可见
    }

    // Update is called once per frame
    void Update()
    {
        //铲子始终跟随鼠标
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;

        //点击鼠标左键，自身不可见，UI可见
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shovelUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void clickShovel()
    {
        //铲子UI不可见
        shovelUI.SetActive(false);

        //自身可见，跟随鼠标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
        gameObject.SetActive(true);

        //播放音效
        GetComponent<AudioSource>().Play();
    }
}
