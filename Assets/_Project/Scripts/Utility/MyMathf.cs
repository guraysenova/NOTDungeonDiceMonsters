using UnityEngine;

public class MyMathf
{
    public static float ClampAngle(float currentValue, float minAngle, float maxAngle, float clampAroundAngle = 0)  // Used for clamping the rotation of an object between 2 angles
    {
        float angle = currentValue - (clampAroundAngle + 180);

        while (angle < 0)
        {
            angle += 360;
        }

        angle = Mathf.Repeat(angle, 360);

        return Mathf.Clamp(angle - 180, minAngle, maxAngle) + 360 + clampAroundAngle;
    }
}