using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashDetectRegion : MonoBehaviour
{
    public Squash squash;   //ÎÑ¹Ï

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if (squash.collider_Idle.enabled == true)
            {
                //Ëø¶¨½©Ê¬
                squash.lockedZombie = collision.gameObject;

                //ÅÐ¶Ï½©Ê¬ÔÚ×ó±ß»¹ÊÇÓÒ±ß
                if (squash.lockedZombie.transform.position.x > transform.position.x)
                    squash.animator.SetBool("LookRight", true);
                else squash.animator.SetBool("LookLeft", true);

                squash.collider_Idle.enabled = false;   //Ì½²âÅö×²ÌåÊ§Ð§
                squash.idle = false;
            }
            else if (squash.collider_attack.enabled == true)
            {
                if (collision.GetComponent<Zombie>().pos_row == squash.row)
                {
                    collision.GetComponent<Zombie>().beSquashed();
                }
            }
        }
    }
}
