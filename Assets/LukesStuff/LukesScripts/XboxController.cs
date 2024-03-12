using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxController
{

    // Registered controller ID
    public long id;

    // Axises
    // For setup use Joycon 1
    private float LEFT_STICK_X_AXIS = Input.GetAxis("Horizontal_0");
    private float LEFT_STICK_Y_AXIS = Input.GetAxis("Vertical_0");
    private float RIGHT_STICK_X_AXIS = Input.GetAxis("RightStickHorizontal_0");
    private float RIGHT_STICK_Y_AXIS = Input.GetAxis("RightStickVertical_0"); // These may need to be flipped
    private float D_PAD_X_AXIS = Input.GetAxis("DPADHorizontal_0");
    private float D_PAD_Y_AXIS = Input.GetAxis("DPADVertical_0");
    private float LEFT_TRIGGER = Input.GetAxis("LeftTrigger_0");
    private float RIGHT_TRIGGER = Input.GetAxis("RightTrigger_0");
    private float TRIGGERS = Input.GetAxis("Triggers_0");

    //Buttons
    private KeyCode BUTTON_A = KeyCode.JoystickButton0;
    private KeyCode BUTTON_B = KeyCode.JoystickButton1;
    private KeyCode BUTTON_X = KeyCode.JoystickButton2;
    private KeyCode BUTTON_Y = KeyCode.JoystickButton3;
    private KeyCode BUTTON_LB = KeyCode.JoystickButton4;
    private KeyCode BUTTON_RB = KeyCode.JoystickButton5;
    private KeyCode BUTTON_BACK = KeyCode.JoystickButton6;
    private KeyCode BUTTON_START = KeyCode.JoystickButton7;
    private KeyCode BUTTON_LS_CLICK = KeyCode.JoystickButton8;
    private KeyCode BUTTON_RS_CLICK = KeyCode.JoystickButton9;

    // Update is called once per frame
    public void checkInput()
    {
        updateAxis();
    }

    public void updateAxis()
    {
        // LS
        this.LEFT_STICK_X_AXIS = Input.GetAxis("Horizontal_" + this.id);
        this.LEFT_STICK_Y_AXIS = Input.GetAxis("Vertical_" + this.id);
        // RS
        this.RIGHT_STICK_X_AXIS = Input.GetAxis("RightStickHorizontal_" + this.id);
        this.RIGHT_STICK_Y_AXIS = Input.GetAxis("RightStickVertical_" + this.id);
        // DPAD
        this.D_PAD_X_AXIS = Input.GetAxis("DPADHorizontal_" + this.id);
        this.D_PAD_Y_AXIS = Input.GetAxis("DPADVertical_" + this.id);
        // Triggers
        this.LEFT_TRIGGER = Input.GetAxis("LeftTrigger_" + this.id);
        this.RIGHT_TRIGGER = Input.GetAxis("RightTrigger_" + this.id);
        this.TRIGGERS = Input.GetAxis("Triggers_" + this.id);
    }

    // Getters
    public float getLeftStickXAxis()
    {
        return LEFT_STICK_X_AXIS;
    }

    public float getLeftStickYAxis()
    {
        return LEFT_STICK_Y_AXIS;
    }

    public float getDPadXAxis()
    {
        return D_PAD_X_AXIS;
    }

    public float getDPadYAxis()
    {
        return D_PAD_Y_AXIS;
    }

    // On pressed

    public bool isAPressed()
    {
        return Input.GetButtonDown("Fire1_" + this.id);
    }

    public bool isBPressed()
    {
        return Input.GetButtonDown("Fire2_" + this.id);
    }

    public bool isXPressed()
    {
        return Input.GetButtonDown("Fire3_" + this.id);
    }

    public bool isYPressed()
    {
        return Input.GetButtonDown("Jump_" + this.id);
    }

    public bool isLBPressed()
    {
        return Input.GetKeyDown(BUTTON_LB);
    }

    public bool isRBPressed()
    {
        return Input.GetKeyDown(BUTTON_RB);
    }

    public bool isBackPressed()
    {
        return Input.GetButtonDown("Back") ;
    }

    public bool isStartPressed()
    {
        return Input.GetButtonDown("Start");
    }

    public bool isLSClicked()
    {
        return Input.GetKeyDown(BUTTON_LS_CLICK);
    }

    public bool isRSClicked()
    {
        return Input.GetKeyDown(BUTTON_RS_CLICK);
    }

    // On held
    public bool isAHeld()
    {
        return Input.GetButton("Fire1_" + this.id);
    }

    public bool isBHeld()
    {
        return Input.GetButton("Fire2_" + this.id);
    }

    public bool isXHeld()
    {
        return Input.GetButton("Fire3_" + this.id);
    }

    public bool isYHeld()
    {
        return Input.GetButton("Jump_" + this.id);
    }

    public bool isStartHeld()
    {
        return Input.GetButton("Start");
    }

    public bool isBackHeld()
    {
        return Input.GetKey("Back");
    }

    public bool isLBHeld()
    {
        return Input.GetKey(BUTTON_LB);
    }

    public bool isRBHeld()
    {
        return Input.GetKey(BUTTON_RB);
    }

    public bool isLSClickHeld()
    {
        return Input.GetKey(BUTTON_LS_CLICK);
    }

    public bool isRSClickHeld()
    {
        return Input.GetKey(BUTTON_RS_CLICK);
    }

    public void setID(long id)
    {
        this.id = id;
        Debug.Log("Controller[" + id + "] assigned!");
    }

    public bool isConnected()
    {
        return false;
    }
}
