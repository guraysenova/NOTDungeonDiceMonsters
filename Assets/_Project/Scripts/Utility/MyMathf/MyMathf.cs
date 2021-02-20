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

    public static Vector3 ClampVector(Vector3 vector , VectorClamps clamps)
    {
        return new Vector3(Mathf.Clamp(vector.x, clamps.x.Min, clamps.x.Max), Mathf.Clamp(vector.y, clamps.y.Min, clamps.y.Max), Mathf.Clamp(vector.z, clamps.z.Min, clamps.z.Max));
    }

    public static Vector3 ClampEuler(Vector3 euler , VectorClamps clamps)
    {
        return new Vector3(ClampAngle(euler.x, clamps.x.Min, clamps.x.Max,clamps.x.ClampAround), ClampAngle(euler.y, clamps.y.Min, clamps.y.Max , clamps.y.ClampAround), ClampAngle(euler.z, clamps.z.Min, clamps.z.Max, clamps.z.ClampAround));
    }
}
