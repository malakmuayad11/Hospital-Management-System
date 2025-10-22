using Hospital_Data;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsConsultation
    {
        public int ConsultationID { get; set; }
        public string ConsultationName { get; set; }    
        public decimal ConsultationFee { get; set; }

        public string Specialty { get; set; }

        public clsConsultation() { }

        private clsConsultation(int ConsultationID, string ConsultationName, decimal ConsultationFee, string specialty)
        {
            this.ConsultationID = ConsultationID;
            this.ConsultationName = ConsultationName;
            this.ConsultationFee = ConsultationFee;
            Specialty = specialty;
        }

        public static async Task<DataTable> GetAllConsultationsAsync() => await clsConsultationData.GetAllConsultationsAsync();

        public static clsConsultation Find(int ConsultationID)
        {
            string ConsultationName = string.Empty;
            decimal ConsultationFee = -1;
            string Specialty = string.Empty;    

            if (clsConsultationData.Find(ConsultationID, ref ConsultationName, ref ConsultationFee, ref Specialty))
                return new clsConsultation(ConsultationID, ConsultationName, ConsultationFee, Specialty);
            return null;
        }

        public static async Task<DataTable> GetAllSpecialities() => await clsConsultationData.GetAllSpecialitiesAsync();
    }
}
