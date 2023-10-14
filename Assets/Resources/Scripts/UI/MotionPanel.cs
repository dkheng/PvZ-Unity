using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionPanel : MonoBehaviour
{
    bool moving = false;

    public Direction direction;
    protected RectTransform rectTransform;   //自身RectTransform组件

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            switch (direction)
            {
                case Direction.Up: moveUp(); break;
                case Direction.Down: moveDown(); break;
            }
        }
    }

    private void moveUp()
    {
        float newY = rectTransform.anchoredPosition.y + 100 * Time.deltaTime; ;
        if (newY < 0)
        {
            rectTransform.anchoredPosition = new Vector3(0, newY, 0);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            this.enabled = false;
        }
    }

    private void moveDown()
    {
        float newY = rectTransform.anchoredPosition.y - 100 * Time.deltaTime; ;
        if (newY > 0)
        {
            rectTransform.anchoredPosition = new Vector3(0, newY, 0);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            this.enabled = false;
        }
    }

    public void startMove()
    {
        moving = true;
    }
}

public enum Direction {Up, Down}