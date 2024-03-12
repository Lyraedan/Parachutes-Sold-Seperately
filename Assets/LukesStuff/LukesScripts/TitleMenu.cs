using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    XboxController controller;

    int selected = 0;

    string[] labels = { "Play 2 player", "Play 4 player", "Exit" };

    private int pressTimer = 0;

    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    public TextMesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        controller = new XboxController();
        controller.setID(0);

        mesh.text = labels[0] + "\n" + labels[1] + "\n" + labels[2];

        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        scenePaths = myLoadedAssetBundle.GetAllScenePaths();

    }

    // Update is called once per frame
    void Update()
    {
        controller.updateAxis();

        string[] display = { "Play 2 player", "Play 4 player", "Exit" };
        if (controller.getDPadYAxis() == 1 || controller.getLeftStickYAxis() == 1)
        {
            if (pressTimer == 0) selected--;
            pressTimer = 1;
        }
        else if(controller.getDPadYAxis() == -1 || controller.getLeftStickYAxis() == -1)
        {
            if (pressTimer == 0) selected++;
            pressTimer = 1;
        } else
        {
            pressTimer = 0;
        }
        if (selected < 0) selected = labels.Length - 1;
        else if (selected >= labels.Length) selected = 0;
        for(int i = 0; i < labels.Length; i++)
        {
            if(i != selected) display[i] = labels[i];
        }
        display[selected] = labels[selected] + " <";

        mesh.text = display[0] + "\n" + display[1] + "\n" + display[2];

        bool clicked = controller.isAPressed();

        if (controller.isAPressed())
        {
            switch (selected)
            {
                case 0:
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    break;
                case 1:
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                    break;
                case 2:
                    //SceneManager.LoadScene(scenePaths[3], LoadSceneMode.Single);
                    break;
                

            }
        }

    }
}
