namespace Common.Pagination
{
    public class Pagination
    {
        public static bool CheckMinMax(decimal minValue, decimal maxValue)
        {
            if(minValue < 0)
            {
                return true;
            }
            if(maxValue < 0)
            {
                return true;
            }
            if(minValue > maxValue && maxValue != 0)
            {
                return true;
            }
            return false;
        }

    }
}
