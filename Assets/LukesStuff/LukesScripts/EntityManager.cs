using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{

    public GameObject playerBase;
    private List<GameObject> playerObjects = new List<GameObject>();
    private List<Player> playerComponents = new List<Player>();
    public Texture[] playerMaterial;

    public int numberOfPlayers = 2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            addPlayer();
        }
    }

    void Update()
    {
                 
        foreach(Player p in playerComponents) 
        {
            p.getController().checkInput();
        } 
    }

    public void addPlayer()
    {
        if (playerObjects.Count >= 4) return;
        int id = playerObjects.Count;
        GameObject added = Instantiate(playerBase, new Vector3(transform.position.x + (id * 3f), transform.position.y, transform.position.z), Quaternion.identity);
        Player player = added.GetComponent<Player>();
        player.setID(id);
        player.assignController(new XboxController());
        /* ID: Get input from the correct joycon using the ID system.
            0 - Joycon 1
            1 - Joycon 2
            2 - Joycon 3
            3 - Joycon 4;
         */
        player.getController().updateAxis();

        //assign player material
        SkinnedMeshRenderer meshMaterial = added.transform.Find("Gerald").Find("guy").GetComponent<SkinnedMeshRenderer>();
        meshMaterial.materials[1].mainTexture = playerMaterial[id];

        added.name = "Player_" + id;
        playerObjects.Add(added);
        playerComponents.Add(player);
    }

    public List<GameObject> getPlayerObjects()
    {
        return this.playerObjects;
    }

    public List<Player> getPlayerComponents()
    {
        return this.playerComponents;
    }

}
