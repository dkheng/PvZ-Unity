using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowKing : MultiImagePlant
{
    public GameObject SongController;
    public List<GameObject> bullets;

    private Vector3 bulletOffset = new Vector3(0.656f, 0.392f, 0);   //子弹初始位置偏移量
    private Vector2 castEndPoint;   //射线投射时终点

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector2(5.3f, transform.position.y);
        GameObject songController = GameObject.Find("SnowKingSongController");
        if(songController != null)
        {
            songController.GetComponent<SnowKingSong>().addSnowKing();
        }
        else
        {
            songController = Instantiate(SongController);
            songController.name = "SnowKingSongController";
            songController.GetComponent<SnowKingSong>().addSnowKing();
        }
    }

    public void fireEvent()
    {
        RaycastHit2D[] hitResults =
                Physics2D.LinecastAll(transform.position, castEndPoint, LayerMask.GetMask("Zombie"));
        foreach (RaycastHit2D hitResult in hitResults)
        {
            if (hitResult.transform.GetComponent<Zombie>().pos_row == row)
            {
                int bulletIndex = Random.Range(0, bullets.Count);
                Instantiate(
                    bullets[bulletIndex],
                    transform.position + bulletOffset,
                    Quaternion.Euler(0, 0, 0)
                ).GetComponent<ThrowBullet>()
                    .initialize(hitResult.transform.GetComponent<Zombie>(), this, row);
                audioSource.Play();
                break;
            }
        }
    }

    //雪王不会寒冷，故重写为空
    public override void cold()
    {
        
    }
}
