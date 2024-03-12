using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string[] buttons = new string[4] { "A", "B", "X", "Y" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // remove event from scene when called
    public void Delete()
    {
        Destroy(gameObject);
    }

    public void CheckForItself()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    public string getButton(int index)
    {
        return this.buttons[index];
    }
}
