using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_NameManufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        internal Wheel(string i_NameManufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_NameManufacturer = i_NameManufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public void Blowing(float i_AddAirPressure)
        {
            if(m_CurrentAirPressure + i_AddAirPressure <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AddAirPressure;
            }
            else
            {
                throw new ArgumentException(
                    "You have passed the max pressure of the tire");
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Keywords.k_WheelsManufacturer}: {m_NameManufacturer}");
            sb.AppendLine($"{Keywords.k_CurrentWheelsPressure}: {m_CurrentAirPressure}");
            sb.AppendLine($"{Keywords.k_MaxWheelsPressure}: {m_MaxAirPressure}");

            return sb.ToString();
        }
    }
}
