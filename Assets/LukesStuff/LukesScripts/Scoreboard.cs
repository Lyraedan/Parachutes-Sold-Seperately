using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private EntityManager entityManager;
    private TextMesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        this.entityManager = GameObject.Find("EntityManager").GetComponent<EntityManager>();
        this.mesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        string message = "Scoreboard:\n";
        for (int i = 0; i < entityManager.numberOfPlayers; i++)
        {
            message += "Player " + (i + 1) + ": " + this.entityManager.getPlayerComponents()[i].points + "\n";
        }
        mesh.text = message;
    }
}
