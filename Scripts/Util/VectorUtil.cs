using Godot;

public static class VectorUtil
{
    public static Vector2 RandPointInCircle(float radius)
    {
        float r = (float)Mathf.Sqrt(GD.RandRange(0.0f, 1.0f)) * radius;
        float t = (float)GD.RandRange(0.0f, 1.0f) * Mathf.Tau;
        return new Vector2((float)(r * Mathf.Cos(t)), (float)(r * Mathf.Sin(t)));
    }
}
