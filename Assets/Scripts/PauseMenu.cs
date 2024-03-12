using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    XboxController controller;

    int selected = 0;

    string[] labels = { "Resume", "Back to menu" };

    private int pressTimer = 0;

    private TextMesh mesh;

    public void open()
    {
        mesh = gameObject.AddComponent<TextMesh>();

    }

    public void interact(XboxController controller)
    {
        controller.updateAxis();

        string[] display = { "Resume", "Back to menu" };
        if (controller.getDPadYAxis() == 1 || controller.getLeftStickYAxis() == 1)
        {
            if (pressTimer == 0) selected--;
            pressTimer = 1;
        }
        else if (controller.getDPadYAxis() == -1 || controller.getLeftStickYAxis() == -1)
        {
            if (pressTimer == 0) selected++;
            pressTimer = 1;
        }
        else
        {
            pressTimer = 0;
        }
        if (selected < 0) selected = labels.Length - 1;
        else if (selected >= labels.Length) selected = 0;
        for (int i = 0; i < labels.Length; i++)
        {
            if (i != selected) display[i] = labels[i];
        }
        display[selected] = labels[selected] + " <";

        mesh.text = display[0] + "\n" + display[1] + "\n" + display[2];

        bool clicked = controller.isAPressed();

        if (controller.isAPressed())
        {
            switch (selected)
            {
                case 0:
                    Debug.Log("Resume");
                    break;
                case 1:
                    Debug.Log("Back to menu");
                    break;
            }
        }

    }
}
