using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerRevised : MonoBehaviour
{
    public GameObject[] fx = new GameObject[3];

    // x, y, z, fx_id (0, 2)
    public List<Vector4> locations = new List<Vector4>();
    public List<Vector4> rotations = new List<Vector4>();

    private int timer = 0;
    public int spawnRate = 100;

    private bool isRunning = false;
   
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 13; i++)
        {
            spawnObject(i, fx[i % 3]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer >= spawnRate)
        {
            bool check = spawn();
            timer = 0;
        }
    }

    bool spawn()
    {
        int index = Random.Range(0, 12);
        GameObject source = fx[(int) locations[index].w];
        Vector3 location = locations[index];
        GameObject find = GameObject.FindWithTag("Interactable"); // (source.name + "_" + Mathf.Round(location.x) + "_" + Mathf.Round(location.y) + "_" + Mathf.Round(location.z)) != null;
        bool exists = (find.transform.position == location);
        if (!exists)
        {
            spawnObject(index, source);
            return true;
        }
        return false;
    }

    public void spawnObject(int index, GameObject source)
    {
        Vector3 location = new Vector3(locations[index].x, locations[index].y, locations[index].z);
        Vector3 rotation = new Vector3(rotations[index].x, rotations[index].y, rotations[index].z);
        GameObject added = Instantiate(source, location, new Quaternion(rotation.x, rotation.y, rotation.z, 0));
        added.name = source.name + "_" + Mathf.Round(location.x) + "_" + Mathf.Round(location.y) + "_" + Mathf.Round(location.z);
    }
}
