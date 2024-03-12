using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    //public Vector3 UINewRotation = new Vector3(0, 0, 0);
    public Quaternion UINewRotation = new Quaternion(0, 0, 0, 0);

    public void updateRotation()
    {
        UINewRotation = new Quaternion(transform.rotation.x * -1, transform.rotation.y * -1, transform.rotation.z * -1, 0);
        this.gameObject.transform.GetChild(0).GetChild(1).gameObject.transform.rotation = UINewRotation; //Quaternion.Euler(UINewRotation.x, UINewRotation.y, UINewRotation.z);

        Vector3 aboveLocation = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
        this.gameObject.transform.GetChild(0).GetChild(1).gameObject.transform.position = aboveLocation;
    }

}