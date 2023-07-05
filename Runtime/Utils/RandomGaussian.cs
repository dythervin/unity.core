using Unity.Mathematics;

namespace Dythervin.Core.Utils
{
    public static class RandomGaussian
    {
        public static float Range(float minValue, float maxValue)
        {
            return Get((maxValue + minValue) / 2, math.abs(maxValue - minValue));
        }

        public static float Get(float mean = 0.5f, float range = 0.5f)
        {
            float u, s;

            do
            {
                u = 2.0f * UnityEngine.Random.value - 1.0f;
                float v = 2.0f * UnityEngine.Random.value - 1.0f;
                s = u * u + v * v;
            } while (s >= 1.0f);

            // Standard Normal Distribution
            float std = u * math.sqrt(-2.0f * math.log(s) / s);

            // Normal Distribution centered between the min and max value
            // and clamped following the "three-sigma rule"
            float minValue = mean - range;
            float maxValue = mean + range;
            float sigma = (math.max(maxValue, minValue) - mean) / 3.0f;
            return math.clamp(std * sigma + mean, minValue, maxValue);
        }


        public static float Range(this System.Random random, float minValue, float maxValue)
        {
            return Get(random, (maxValue + minValue) / 2, math.abs(maxValue - minValue));
        }

        public static float Get(this System.Random random, float mean = 0.5f, float range = 0.5f)
        {
            float u, s;

            do
            {
                u = 2.0f * (float)random.NextDouble() - 1.0f;
                float v = 2.0f * (float)random.NextDouble() - 1.0f;
                s = u * u + v * v;
            } while (s >= 1.0f);

            // Standard Normal Distribution
            float std = u * math.sqrt(-2.0f * math.log(s) / s);

            // Normal Distribution centered between the min and max value
            // and clamped following the "three-sigma rule"
            float minValue = mean - range;
            float maxValue = mean + range;
            float sigma = (math.max(maxValue, minValue) - mean) / 3.0f;
            return math.clamp(std * sigma + mean, minValue, maxValue);
        }
    }
}