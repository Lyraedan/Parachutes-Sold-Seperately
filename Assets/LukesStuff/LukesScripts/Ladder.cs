using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    Player p;

    void OnTriggerEnter(Collider collider)
    {
        p = collider.gameObject.GetComponent<Player>();

        if (p == null) return;

        if (collider.gameObject.tag == "Player")
        {
            p.jumpHeight = 8f;
        }
    }
     void OnTriggerExit()
    {
            p.jumpHeight = 5f;
     
    }
}
