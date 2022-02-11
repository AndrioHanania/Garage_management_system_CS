using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_NameModel;
        private string m_LicenseNumber;
        private readonly List<Wheel> r_Wheels;
        private Battery m_Battery;

        protected Vehicle(
            string i_NameModel,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            Battery i_Battery)
        {
            m_NameModel=  i_NameModel;
            m_LicenseNumber = i_LicenseNumber;
            r_Wheels = i_Wheels;
            m_Battery = i_Battery;
        }

        public string NameModel
        {
            get
            {
               return m_NameModel;
            }
            set
            {
                m_NameModel = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
            set
            {
                m_LicenseNumber = value;
            }
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return Battery.CurrentEnergyAmount;
            }

            set
            {
                Battery.CurrentEnergyAmount = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public Battery Battery
        {
            get
            {
                return m_Battery;
            }
        }

        public override string ToString()
        {
            int           countWheel = 0;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Keywords.k_ModelName}: {m_NameModel}");
            sb.AppendLine($"{Keywords.k_LicenseNumber}: {m_LicenseNumber}");
            sb.AppendLine($"{Keywords.k_Battery}:");
            sb.AppendLine($"{m_Battery}");
            sb.AppendLine($"{Keywords.k_Wheels}:");
            foreach (Wheel wheel in r_Wheels)
            {
                sb.Append($"{Keywords.k_Wheel} {countWheel}: ");
                sb.AppendLine($"{wheel}");
                countWheel++;
            }

            return sb.ToString();
        }
    }
}
