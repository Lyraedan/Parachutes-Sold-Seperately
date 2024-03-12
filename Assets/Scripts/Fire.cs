using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : EventsManager
{
    public string randomButton;
    public float fixRate = 0f, finishAt;

    public Image progressBar;

    public GameObject handler;
    private EventsManager eventsManger;

    void Start()
    {
        //handler = GameObject.Find("EventManager");
        eventsManger = handler.GetComponent<EventsManager>();
        Debug.Log("--------------------->" + eventsManger);
        progressBar.fillAmount = 100;
    }

    void Update()
    {
        if (fixRate > 0)
        {
            progressBar.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            progressBar.transform.parent.gameObject.SetActive(false);
        }
        FixRotation rot = transform.parent.GetComponent<FixRotation>();
        if (rot == null) return;
        rot.updateRotation();
    }

    public void FixIssue()
    {
        float percentage = MathHelper.getPercentage(fixRate, finishAt); // Get between 0 and 100 in percentage
        
        if (percentage >= 100)
        {
            char charNumber = gameObject.name[4];
            int num = charNumber - '0';
            
            Debug.Log("index is: " + num);
            Debug.Log("fireLocalUsed length is: " + fireLocalUsed.Length);
            
            fireLocalUsed[num] = false;

            Destroy(gameObject);
        }
    }

    public void increaseFixRate()
    {
        fixRate++;
        progressBar.fillAmount -= (MathHelper.getPercentage(fixRate, finishAt) / 7500);
    }

    public void reset()
    {
        fixRate = 0;
        progressBar.fillAmount = finishAt;
    }
}
