using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAsset : MonoBehaviour
{
    public float lifetime = 6f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initiateKill());
    }

    IEnumerator initiateKill()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
