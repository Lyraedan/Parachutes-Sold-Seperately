using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float minBright, maxBright;
    public float minRange, maxRange;

    // animate the game object from -1 to +1 and back
    public float minimum = -1.0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    float t = 0.0f;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Light>().intensity = Random.Range(minBright, maxBright);
        gameObject.GetComponent<Light>().range = Random.Range(minRange, maxRange);
    }
}
