using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public GameObject[] events;

    public Transform[] fireEventLocations;
    [HideInInspector] public bool[] fireLocalUsed;
    public Transform[] steamEventLocations;
    [HideInInspector] public bool[] steamLocalUsed;
    public Transform[] electricEventLocations;
    [HideInInspector] public bool[] electricLocalUsed;

    public int numberOfEvents;


    public float timer = 2.0f;         // countdown timer till next event appears, given number is how long till first spawn.

    int startEvents = 1;           // number of events that appear at the beginning of the play session.

    void Start()
    {
        fireLocalUsed = new bool[fireEventLocations.Length];
        steamLocalUsed = new bool[steamEventLocations.Length];
        electricLocalUsed = new bool[electricEventLocations.Length];

        numberOfEvents = 0;

        for (int i = startEvents; i > 0; i--)
        {
            SpawnEvent();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEvent();
            
            timer = Random.Range(5f, 20.0f);      // reset the event timer
            timer = 0.5f;                          // quicker timer for debugging
        }
    }

    public void SpawnEvent()
    {
        int eventDecideNum = Random.Range(0, events.Length);

        GameObject temp;
        switch (eventDecideNum)
        {
            case 0:
                int fireLocalTem = Random.Range(0, fireEventLocations.Length);
                if (fireLocalUsed[fireLocalTem] == true)
                {
                    return;
                }
                else
                {
                    //temp = Instantiate(events[eventDecideNum], fireEventLocations[Random.Range(0, fireEventLocations.Length)]);
                    temp = Instantiate(events[eventDecideNum], fireEventLocations[fireLocalTem]);
                    temp.name = "Fire" + fireLocalTem.ToString() +" fucking banana";
                    fireLocalUsed[fireLocalTem] = true;

                    numberOfEvents++;
                }
                break;
            case 1:
                int steamLocalTem = Random.Range(0, steamEventLocations.Length);
                if (steamLocalUsed[steamLocalTem] == true)
                {
                    return;
                }
                else
                {
                    //temp = Instantiate(events[eventDecideNum], steamEventLocations[Random.Range(0, steamEventLocations.Length)]);
                    temp = Instantiate(events[eventDecideNum], steamEventLocations[steamLocalTem]);
                    temp.name = "Steam" + steamLocalTem.ToString();
                    steamLocalUsed[steamLocalTem] = true;

                    numberOfEvents++;
                }
                break;
            case 2:
                int electricLocalTem = Random.Range(0, electricEventLocations.Length);
                if (electricLocalUsed[electricLocalTem] == true)
                {
                    return;
                }
                else
                {
                    //temp = Instantiate(events[eventDecideNum], electricEventLocations[Random.Range(0, electricEventLocations.Length)]);
                    temp = Instantiate(events[eventDecideNum], electricEventLocations[electricLocalTem]);
                    temp.name = "Electric" + electricLocalTem.ToString();
                    electricLocalUsed[electricLocalTem] = true;

                    numberOfEvents++;
                }
                break;
            default:
                break;
        }
    }
}
