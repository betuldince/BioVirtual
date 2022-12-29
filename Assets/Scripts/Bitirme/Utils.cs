
using System;
using System.Collections.Generic;

public class Utils
{
    public static float CalculateStandardDeviation(List<float> data, float mean)
    {
        int L = data.Count;
        float SD = 0.0f;

        for (int i = 0; i < L; ++i)
        {
            SD += (data[i] - mean) * (data[i] - mean);
        }
        return (float)Math.Sqrt(SD / L);
    }

    public static float CalculateMean(List<float> data)
    {
        int L = data.Count;
        float sum = 0.0f;

        for (int i = 0; i < L; ++i)
        {
            sum += data[i];
        }

        return sum / L; ;

    }
}
