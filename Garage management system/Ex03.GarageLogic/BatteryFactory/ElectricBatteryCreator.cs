namespace Ex03.GarageLogic
{
    public class ElectricBatteryCreator
    {
        public static Battery Create(float i_MaxEnergyAmount,
                                     float i_CurrentEnergyAmount)
        {
            Battery battery = new ElectricBattery(i_MaxEnergyAmount,
                i_CurrentEnergyAmount);

            return battery;
        }
    }
}