using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windInstances : MonoBehaviour
{
    public GameObject[] windType;
    public bool canSpawn = true;
    public Vector2 maxRange;
    public Vector2 minRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) { StartCoroutine(windInstance()); }
        
    }
    
    IEnumerator windInstance()
    {
        canSpawn = false;
        GameObject windToSpawn = windType[Random.Range(0,windType.Length)];
        Instantiate(windToSpawn, new Vector3(Random.Range(minRange.x,maxRange.x), Random.Range(minRange.y,maxRange.y), 10), Quaternion.Euler(0,180,0));

        Debug.Log("1");
        yield return new WaitForSeconds(Random.Range(.5f,4f));
        canSpawn = true;
        Debug.Log("2 " + canSpawn);
    }
}
