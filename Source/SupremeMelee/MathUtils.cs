namespace SupremeMelee;

public static class MathUtils
{
    public static float TwoWayScale(float first, float second)
    {
        float result;
        if (second > first)
        {
            result = first / second;
        }
        else
        {
            result = second / first;
        }

        return result;
    }
}