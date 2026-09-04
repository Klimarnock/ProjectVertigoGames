public static class WheelRules
{
    
    
    
    public static WheelType GetWheelType(int zoneNumber)
    {
        if (zoneNumber % 30 != 0 && zoneNumber % 5 != 0) { return WheelType.Bronze; }


        else if (zoneNumber % 5 == 0 && zoneNumber % 30 != 0) { return WheelType.Silver; }

        else
        { return WheelType.Gold; }
    }
}
