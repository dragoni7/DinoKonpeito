
using System;

public static class SoundUtil
{
    public static float PercentageToDecibels(double percentage)
    {
        float scale = 20.0f;
        float divisor = 50.0f;
        return (float)(scale * Math.Log10(percentage / divisor));
    }
}