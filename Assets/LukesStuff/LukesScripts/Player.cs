using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public long id;
    public float movementSpeed = 10f;
    public float jumpHeight = 5.0f;
    public float fallingSpeed = 3.14f;
    private float sprintSpeed;
    private XboxController controller;
    private bool sprinting = false;
    private Rigidbody body;
    private Raycasting raycast;
    public int points = 0;
    public GameObject nameplateGO;
    public TextMesh nameplate;
    public float knockbackForce = 10f;
    public static bool open = false;
    [HideInInspector] public PlanePivot planePivot;

    public AudioClip snd_jump, snd_slap;
    public AudioClip[] snd_repair;
    private AudioSource src;

    private Animator animator;
    private bool isInteract = false;

    private float startTime = 0.0f;

    public float minX, minY = 0F;
    public float maxX, maxY = 1.0F;

    // Repair
    private float repairTime = 0, repairRate = 100;

    // starting value for the Lerp
    public float t = 0.0f;

    private MeshRenderer renderer;

    void Start()
    {
        sprintSpeed = movementSpeed;
        this.startTime = Time.time;
        this.body = this.gameObject.GetComponent<Rigidbody>();
        this.raycast = new Raycasting(transform);
        this.animator = this.transform.Find("Gerald").GetComponent<Animator>();
        this.renderer = GameObject.Find("cameraMasterPivot").transform.Find("cameraAnimatedPivot").Find("Zoom").Find("Main Camera").Find("Quad").GetComponent<MeshRenderer>();
        this.planePivot = GameObject.Find("cameraMasterPivot").GetComponent<PlanePivot>();
        this.src = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip clip)
    {
        this.src.clip = clip;
        this.src.Play();
    }

    void Update()
    {
        nameplateGO.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        if (this.controller.isStartPressed())
        {
            //pause(this.controller);
        }

        if (!open)
        {
            movement();

            RaycastHit forward = this.raycast.raycast(Vector3.forward);
            RaycastHit down = this.raycast.raycast(Vector3.down);
            if (this.controller.isAPressed() && !isFalling(down.distance))
            {
                this.jump();
            }

            //Slapping
            if (forward.collider.gameObject.tag == "Player" && this.controller.isXPressed())
            {
                if (forward.distance < 1f)
                {
                    // Play slap sound here
                    Rigidbody otherBody = forward.collider.gameObject.GetComponent<Rigidbody>();
                    Vector3 direction = forward.collider.gameObject.transform.position - transform.position;
                    float force = 400f;
                    float kick = 4f;
                    otherBody.AddForce(direction * force);
                    otherBody.velocity = new Vector3(0, Vector3.up.y * kick, 0);
                    src.volume = 0.5f;
                    playSound(snd_slap);
                }
                animator.SetBool("isSlapping", true);
            } else if(!this.controller.isXPressed())
            {
                animator.SetBool("isSlapping", false);
            }
        }
    }

    public void pause(XboxController controller)
    {
        if(open)
        {
            renderer.material.SetColor("_Color", new Color(0, 0, 0, 255));
        }
        else
        {
            renderer.material.SetColor("_Color", new Color(0, 0, 0, 0));
        }
        open = !open;
    }

    private bool isFalling(float distance)
    {
        return distance > 1.0f;
    }

    public void movement()
    {
        if (this.controller == null) return;
        this.sprinting = this.controller.isLSClickHeld();
        Vector2 h = new Vector2(this.controller.getLeftStickXAxis(), this.controller.getLeftStickYAxis());
        if (this.controller.getLeftStickXAxis() != 0 || this.controller.getLeftStickYAxis() != 0)
        {
            rotate();

            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;

            maxX = this.controller.getLeftStickXAxis();
            maxY = this.controller.getLeftStickYAxis();

            // animate the position of the game object...
            float speedX = Mathf.Lerp(minX, maxX, t);
            float speedY = Mathf.Lerp(minY, maxY, t);

            minX = speedX;
            minY = speedY;

            // .. and increase the t interpolater
            if ((t += 0.5f * Time.deltaTime) > 1.0f)
            {
                t = 1.0f;
            }
            else { t += .5f * Time.deltaTime; }


            if (t > 1.0f) { t = 0.0f; }

            float xx = (x += (speedX * getSpeed()) * Time.deltaTime);
            float zz = (z += (speedY * getSpeed()) * Time.deltaTime);

            if (!isInteract)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.8f) != true)
                {
                    transform.position = new Vector3(xx, y, zz);
                    animator.SetBool("isMoving", true);
                }
                else if (hit.collider.isTrigger)
                {
                    transform.position = new Vector3(xx, y, zz);
                    animator.SetBool("isMoving", true);
                }
            }

            
        }

        else if (h.sqrMagnitude == 0)
        {
            t = 0f;
            minX = 0;
            minY = 0;
            animator.SetBool("isMoving", false);
        }
    }

    public void rotate()
    {
        float x = this.controller.getLeftStickXAxis();
        float y = this.controller.getLeftStickYAxis();
        float angle;

        angle = Mathf.Atan2(x, y);

        if (x > 0.1 || y > 0.1 || x < -0.1 || y < -0.1)
        {
            transform.rotation = Quaternion.Euler(0, angle * Mathf.Rad2Deg, 0f);
        }
    }

    public void jump()
    {
        this.body.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        src.volume = 0.15f;
        playSound(snd_jump);
    }

    public void addPoints(int amt)
    {
        this.points += amt;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col == null) return;
        string tag = col.gameObject.tag;
        if (tag == "Interactable")
        {
            nameplate.text = "Hold X";
            int snd_index = 0;
            if(controller.isXPressed())
            {
                snd_index = Random.Range(0, snd_repair.Length);
                AudioClip clip = snd_repair[snd_index];
                src.volume = 0.3f;
                playSound(clip);
            }
            if (controller.isXHeld() && this.planePivot.altitude > 0)
            {
                this.repairTime++;
                this.nameplate.text = Mathf.Round(MathHelper.getPercentage(repairTime, repairRate)) + "%";
                if (this.repairTime >= this.repairRate)
                {
                    this.addPoints(Random.Range(100, 120));
                    Destroy(col.gameObject);
                    this.nameplate.text = "Repaired";
                    this.repairTime = 0;
                    this.repairRate = Random.Range(100, 400);
                    this.planePivot.altitude += MathHelper.getValueFromPercentage(this.planePivot.maxAltitude, 2);
                }
            }
        }
        else
        {
            repairTime = 0;
            repairRate = Random.Range(100, 400);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col == null) return;
        this.nameplate.text = "";
        
    }

    public void setID(long id)
    {
        this.id = id;

        this.nameplateGO = GameObject.Find("Nameplate_" + this.id);
        this.nameplate = this.nameplateGO.GetComponent<TextMesh>();
    }

    private float getSpeed()
    {
        return (sprinting ? sprintSpeed : movementSpeed) * 1.8f;
    }

    public void assignController(XboxController controller)
    {
        this.controller = controller;
        this.controller.setID(id);
    }

    public XboxController getController()
    {
        return this.controller;
    }
    
}
