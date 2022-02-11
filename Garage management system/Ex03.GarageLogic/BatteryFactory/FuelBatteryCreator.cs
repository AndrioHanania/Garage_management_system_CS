namespace Ex03.GarageLogic
{
    public class FuelBatteryCreator
    {
        public static Battery Create(float i_MaxEnergyAmount,
                      float i_CurrentEnergyAmount,
                      FuelBattery.eFuelType i_FuelType)
        {
            Battery battery = new FuelBattery(i_MaxEnergyAmount,
                i_CurrentEnergyAmount, i_FuelType);

            return battery;
        }
    }
}