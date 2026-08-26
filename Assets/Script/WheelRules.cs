public static class WheelRules
{
    public static WheelType GetWheelType(int zoneNumber)
    {
        
        if (zoneNumber % 5 == 0 && zoneNumber % 30 != 0)
        {
            return WheelType.Silver;
        }
        else if (zoneNumber  % 30 == 0)
        {
            return WheelType.Gold;
        }
        else
        {
            return WheelType.Bronze;
        }
    }
}