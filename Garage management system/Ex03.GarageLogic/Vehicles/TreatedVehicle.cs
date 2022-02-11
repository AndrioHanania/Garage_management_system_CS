using System.Text;

namespace Ex03.GarageLogic
{
    public class TreatedVehicle
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eTreatedVehicleStatus m_CurrentStatusInGarage = eTreatedVehicleStatus.Fixing;

        internal TreatedVehicle(Vehicle i_Vehicle,
                                string i_OwnerName,
                                string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Vehicle = i_Vehicle;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public eTreatedVehicleStatus CurrentStatusInGarage
        {
            get
            {
                return m_CurrentStatusInGarage;
            }

            set
            {
                m_CurrentStatusInGarage = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }

        public enum eTreatedVehicleStatus
        {
            Fixing = 1,
            Fixed,
            Paid
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Keywords.k_OwnerName}: {m_OwnerName}");
            sb.AppendLine($"{Keywords.k_OwnerPhone}: {m_OwnerPhoneNumber}");
            sb.AppendLine($"{Keywords.k_CurrentStatusInGarage}: {m_CurrentStatusInGarage}");
            sb.AppendLine($"{Keywords.k_Vehicle}:");
            sb.AppendLine();
            sb.AppendLine($"{m_Vehicle}");

            return sb.ToString();
        }
    }
}