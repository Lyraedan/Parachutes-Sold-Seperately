using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper
{

    public static float getPercentage(float value, float fraction)
    {
        return (value * 100) / fraction;
    }

    public static float getValueFromPercentage(float value, float percentage)
    {
        return (value * (percentage / 100));
    }

}
