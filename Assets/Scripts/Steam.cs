using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steam : EventsManager
{
    public string randomButton;
    public float fixRate = 0f, finishAt;

    public Image progressBar;

    public GameObject handler;
    private EventsManager eventsManger;

    void Start()
    {
        //handler = GameObject.Find("EventManager");
        eventsManger = handler.transform.GetComponent<EventsManager>();
        progressBar.fillAmount = 100;
        Debug.Log("STEAM ->>>>>>>>>>>>>>>>>>>>>>>>>> " + eventsManger.steamLocalUsed.Length);
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
            char charNumber = gameObject.name[5];
            int num = charNumber - '0';
            eventsManger.steamLocalUsed[num] = false;

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
