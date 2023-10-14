using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmPlantRegion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Plant")
        {
            collision.GetComponent<Plant>().warm();
        }
    }

    public void stopWarm()
    {
        List<Collider2D> plants = new List<Collider2D>();

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        contactFilter.SetLayerMask(LayerMask.GetMask("Plant"));

        if (GetComponent<Collider2D>().OverlapCollider(contactFilter, plants) != 0)
        {
            foreach (Collider2D collider in plants)
            {
                collider.gameObject.GetComponent<Plant>().stopWarm();
            }
        }
    }
}
