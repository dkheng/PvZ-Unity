using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBePlanted : MonoBehaviour
{
    #region 变量

    public string plantName;   //当前选中植物的名称

    SpriteRenderer spriteRenderer;   //自身SpriteRenderer组件

    Vector3 mouseWorldPos;  //鼠标位置

    #endregion

    #region 系统消息

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //获取当前鼠标位置
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        //待种植植物随鼠标移动
        transform.position = mouseWorldPos;

        //点击鼠标左键，自身不可见
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region 私有自定义函数

    #endregion

    #region 公有自定义函数

    public void showPlantPreview(string name)
    {
        plantName = name;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Plants/" + plantName);

        //获取当前鼠标位置
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        transform.position = mouseWorldPos;
        gameObject.SetActive(true);
    }

    #endregion
}
