using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricBattery : Battery
    {
        internal ElectricBattery(float i_MaxEnergyAmount, float i_CurrentEnergyAmount)
        :base(i_MaxEnergyAmount, i_CurrentEnergyAmount) {}

        internal void Recharge(float i_ChargeTime)
        {
            try
            {
               FillEnergy(i_ChargeTime);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");

            return sb.ToString();
        }
    }
}
