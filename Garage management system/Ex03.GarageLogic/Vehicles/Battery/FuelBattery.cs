using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelBattery : Battery
    {
        private eFuelType m_FuelType;

        internal FuelBattery(float i_MaxEnergyAmount,
                             float i_CurrentEnergyAmount, 
                             eFuelType i_FuelType)
        : base(i_MaxEnergyAmount, i_CurrentEnergyAmount)
        {
            m_FuelType = i_FuelType;
        }


        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        internal void Refuel(float i_AddFuelQuantity, eFuelType i_FuelType)
        {
            if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(
                    "The type of fuel was not match the vehicle");
            }

            try
            {
                FillEnergy(i_AddFuelQuantity);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($"{Keywords.k_FuelType}: {m_FuelType}");

            return sb.ToString();
        }
    }
}
