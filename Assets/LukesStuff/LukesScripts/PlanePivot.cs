using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanePivot : MonoBehaviour
{
    private MeshRenderer fade;
    private float tenPercent;
    private float originY;
    private float fadeTime = 0;
    public float altitudeRate = 2, altitude = 0, maxAltitude = 100;
    private GameObject em;

    private GameObject altitudeObj;
    private Image altBar;
    bool ended = false;

    public AudioClip falling, blowingup;
    private AudioSource src, bang;

    // Start is called before the first frame update
    void Start()
    {
        this.altitudeObj = GameObject.Find("Canvas").transform.Find("Background").Find("Altitude").gameObject;
        this.tenPercent = MathHelper.getValueFromPercentage(altitude, 10);
        this.originY = transform.position.y;
        this.fade = GameObject.Find("cameraMasterPivot").transform.Find("cameraAnimatedPivot").Find("Zoom").Find("Main Camera").Find("Quad").GetComponent<MeshRenderer>();
        this.altitude = maxAltitude;
        this.src = GetComponent<AudioSource>();
        this.bang = transform.Find("Bang").gameObject.GetComponent<AudioSource>();
        this.em = GameObject.Find("EntityManager");
    }

    // Update is called once per frame
    void Update()
    {
        calculateAltitude();
        float angle = (1 -((1f / maxAltitude) * altitude)) * 12f;
        if (angle >= 12) angle = 12;
        if (altitude <= tenPercent)
        {
            float y = transform.localPosition.y + 1f * Time.deltaTime;
            float x = transform.localPosition.x - 1.4f * Time.deltaTime;
            transform.position = new Vector3(x, y, transform.position.z);
        } else if(angle <= 12)
        {
            transform.rotation = Quaternion.Euler(0,0, angle);
        }
        if(altitude < 15)
        {
            src.clip = falling;
            src.volume = 2f;
            src.Play();
        }
        if(altitude <= 0 && fadeTime <= 255)
        {
            // Fade out
            fadeTime += 30f * Time.deltaTime;
            fade.material.SetColor("_Color", new Color(0, 0, 0, fadeTime / 255));

            GameObject c = GameObject.Find("cameraMasterPivot").transform.Find("cameraAnimatedPivot").Find("Zoom").Find("Main Camera").gameObject;
            if (fade.material.color.a <= 120)
            {
                ShakeCamera sc = c.GetComponent<ShakeCamera>();
                sc.performShake(0.2f, 1000);

                bang.clip = blowingup;
                bang.Play();

                calculateScore();
            }
        } else if(fadeTime >= 255)
        {
            //Show end
        }
        if(ended)
        {
            EntityManager _em = em.GetComponent<EntityManager>();
            foreach(Player p in _em.getPlayerComponents())
            {
                if(p.getController().isAPressed())
                {
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                }
            }
        }
    }

    void calculateAltitude()
    {
        if(altitude > 0) altitude -= altitudeRate * Time.deltaTime;
        if (altitude <= 0) altitude = 0;
        altitudeObj.GetComponent<Image>().fillAmount = (1f / maxAltitude) * altitude;
    }

    void calculateScore()
    {
        int temp = 0;
        int winningPlayer = 0;

        EntityManager _em = em.GetComponent<EntityManager>();
        for(int i = 0; i < _em.numberOfPlayers; i++)
        {
            Player p = _em.getPlayerComponents()[i];
            
            int score = p.points;
            if (score > temp)
            {
                temp = score;
                winningPlayer = i + 1;
            }

            GameObject winningText = transform.Find("cameraAnimatedPivot").transform.Find("Zoom").transform.Find("Main Camera").transform.Find("winningText").gameObject;

            winningText.GetComponent<TextMesh>().text = "Player " + winningPlayer + " wins!" + "\n" + "\n" + "Press A to continue";
            winningText.GetComponent<MeshRenderer>().enabled = true;
            ended = true;
        }
    }
}
