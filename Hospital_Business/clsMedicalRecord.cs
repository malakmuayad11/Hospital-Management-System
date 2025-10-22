using Hospital_Data;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsMedicalRecord
    {
        public int MedicalRecordID { get; set; }
        public int AppointmentID { get; set; }
        public clsAppointment AppointmentInfo;
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string MedicalRecordNotes { get; set; }

        public enum enMode { AddNew = 1, Update = 2}
        private enMode _Mode;

        private clsMedicalRecord(int MedicalRecordID, int AppointmentID, string Symptoms,
            string Diagnosis, string MedicalRecordNotes)
        {
            this.MedicalRecordID = MedicalRecordID;
            this.AppointmentID = AppointmentID;
            AppointmentInfo = clsAppointment.Find(AppointmentID);
            this.Symptoms = Symptoms;
            this.Diagnosis = Diagnosis;
            this.MedicalRecordNotes = MedicalRecordNotes;
            _Mode = enMode.Update;
        }

        public clsMedicalRecord()
        {
            this.MedicalRecordID = -1;
            this.AppointmentID = -1;
            this.Symptoms = string.Empty;
            this.Diagnosis = string.Empty;
            this.MedicalRecordNotes = null;
            this._Mode = enMode.AddNew;
        }

        public static Task<DataTable> GetAllMedicalRecordsAysnc() => clsMedicalRecordData.GetAllMedicalRecordsAsync();

        private bool _AddNewMedicalRecord()
        {
            this.MedicalRecordID = clsMedicalRecordData.AddNewMedicalRecord(this.AppointmentID, this.Symptoms,
                this.Diagnosis, this.MedicalRecordNotes);
            return this.MedicalRecordID != -1;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewMedicalRecord())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static clsMedicalRecord Find(int MedicalRecordID)
        {
            int AppointmentID = -1;
            string Symptoms = string.Empty;
            string Diagnosis = string.Empty;
            string MedicalRecordNotes = null;

            if (clsMedicalRecordData.Find(MedicalRecordID, ref AppointmentID, ref Symptoms,
                ref Diagnosis, ref MedicalRecordNotes))
                return new clsMedicalRecord(MedicalRecordID, AppointmentID, Symptoms, Diagnosis,
                    MedicalRecordNotes);
            return null;
        }

        public static clsMedicalRecord FindByAppointmentID(int AppointmentID)
        {
            int MedicalRecordID = -1;
            string Symptoms = string.Empty;
            string Diagnosis = string.Empty;
            string MedicalRecordNotes = null;

            if (clsMedicalRecordData.FindByAppointmentID(AppointmentID, ref MedicalRecordID, ref Symptoms,
                ref Diagnosis, ref MedicalRecordNotes))
                return new clsMedicalRecord(MedicalRecordID, AppointmentID, Symptoms, Diagnosis,
                    MedicalRecordNotes);
            return null;
        }
    }
}
