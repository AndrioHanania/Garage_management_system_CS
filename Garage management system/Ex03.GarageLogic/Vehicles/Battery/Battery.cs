using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Battery
    {
        private float m_MaxEnergyAmount;
        private float m_CurrentEnergyAmount;

        protected Battery(float i_MaxEnergyAmount, float i_CurrentEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }

            set
            {
                m_CurrentEnergyAmount = value;
            }
        }

        protected void FillEnergy(float i_AddEnergy)
        {
            if (m_CurrentEnergyAmount + i_AddEnergy <= m_MaxEnergyAmount)
            {
                m_CurrentEnergyAmount += i_AddEnergy;
            }
            else
            {
                throw new ArgumentException(
                    "You have overtake the max energy in the battery");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Keywords.k_MaxEnergyAmount}: {m_MaxEnergyAmount}");
            sb.AppendLine($"{Keywords.k_CurrentEnergyAmount}: {m_CurrentEnergyAmount}");

            return sb.ToString();
        }

        public enum eEnergyType
        {
            Electic = 1,
            Fuel
        }
    }
}