using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryDangerousMaterial;
        private float m_MaxCarryWeight;

        internal Truck(
            bool i_IsCarryDangerousMaterial, float i_MaxCarryWeight,
            string i_NameModel, string i_LicenseNumber,
            List<Wheel> i_Wheels, Battery i_Battery)
            : base(i_NameModel, i_LicenseNumber, i_Wheels, i_Battery)
        {
            m_IsCarryDangerousMaterial = i_IsCarryDangerousMaterial;
            m_MaxCarryWeight = i_MaxCarryWeight;
        }

        public bool IsCarryDangerousMaterial
        {
            get
            {
                return m_IsCarryDangerousMaterial;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return m_MaxCarryWeight;
            }
        }

        public override string ToString()
        {
           StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($"{Keywords.k_ItCarryDangerousMaterial}: {m_IsCarryDangerousMaterial}");
            sb.AppendLine($"{Keywords.k_MaxCarryWeight}: {m_MaxCarryWeight}");

            return sb.ToString();
        }
    }
}
