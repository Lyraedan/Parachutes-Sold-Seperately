using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Camera only needs to modify X value
    private Vector3 center = new Vector3(0, 0, 0);
    private EntityManager entityManager;
    public float zoomStrength = 1.2f;
    private float[] distances;
    private float originX, originY, originZ;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("EntityManager");
        entityManager = managerObject.GetComponent<EntityManager>();
        this.distances = new float[entityManager.numberOfPlayers];

        this.originX = transform.position.x;
        this.originY = transform.position.y;
        this.originZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        calculateDistance();
    }

    public void zoom(long id)
    {
        float x = transform.position.x;
        float y = originY + ((distances[id] * zoomStrength) / 5);
        float z = originZ - ((distances[id] * zoomStrength) / 2);
        Vector3 calculated = new Vector3(originX, y, z);
        transform.localPosition = calculated;
    }

    // 1 3 2 4
    public void calculateDistance()
    {
        foreach(GameObject go in entityManager.getPlayerObjects()) {
            float distance = Vector3.Distance(center, go.transform.position);
            Player p = go.GetComponent<Player>();
            distances[p.id] = distance;
        }

        for(int i = 0; i < distances.Length; i++)
        {
            float max = Mathf.Max(distances);
            if (distances[i] == max)
            {
                zoom(i);
            }
        }
    }

}
