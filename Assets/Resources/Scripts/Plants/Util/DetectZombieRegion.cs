using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombieRegion : MonoBehaviour
{
    public GameObject myPlant;
    public BoxCollider2D myCollider;
    private int zombieNum = 0;

    private void Start()
    {
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie" 
            && collision.GetComponent<Zombie>().pos_row == myPlant.GetComponent<Plant>().row)
        {
            if(zombieNum == 0)
            {
                myPlant.GetComponent<Animator>().SetBool("Attack", true);
            }
            zombieNum++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Zombie"
            && collision.GetComponent<Zombie>().pos_row == myPlant.GetComponent<Plant>().row)
        {
            zombieNum--;
            if (zombieNum == 0)
            {
                myPlant.GetComponent<Animator>().SetBool("Attack", false);
            }
        }
    }
}
