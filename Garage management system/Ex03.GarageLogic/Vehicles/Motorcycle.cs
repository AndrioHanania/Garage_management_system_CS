using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        internal Motorcycle(eLicenseType i_LicenseType,
                            int i_EngineVolume,
                            string i_NameModel,
                            string i_LicenseNumber,
                            List<Wheel> i_Wheels,
                            Battery i_Battery)
            : base(i_NameModel, i_LicenseNumber, i_Wheels, i_Battery)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        public enum eLicenseType
        {
            A = 1,
            B1,
            AA,
            BB
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($"{Keywords.k_LicenseType}: {m_LicenseType}");
            sb.AppendLine($"{Keywords.k_EngineVolume}: {m_EngineVolume}");

            return sb.ToString();
        }
    }
}
