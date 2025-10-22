using Hospital_Data;

namespace Hospital_Business
{
    public class clsPrescription
    {
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public clsAppointment AppointmentInfo;
        public string MedicationName { get; set; }
        public string Dosage {  get; set; }
        public byte DurationDays { get; set; }
        public byte? DurationMonths { get; set; }

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public clsPrescription()
        {
            this.PrescriptionID = -1;
            this.AppointmentID = -1;
            this.MedicationName = null;
            this.Dosage = string.Empty;
            this.DurationDays = 0;
            this.DurationMonths = null;
            this._Mode = enMode.AddNew;
        }

        private clsPrescription(int prescriptionID, int appointmentID, string medicationName,
            string dosage, byte durationDays, byte? durationMonths)
        {
            PrescriptionID = prescriptionID;
            AppointmentID = appointmentID;
            AppointmentInfo = clsAppointment.Find(appointmentID);
            MedicationName = medicationName;
            Dosage = dosage;
            DurationDays = durationDays;
            DurationMonths = durationMonths;
            _Mode = enMode.Update;
        }

        private bool _AddNewPrescription()
        {
            this.PrescriptionID = clsPrescriptionData.AddNewPrescription(this.AppointmentID, this.MedicationName,
                this.Dosage, this.DurationDays, this.DurationMonths);
            return this.PrescriptionID != -1;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewPrescription())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static clsPrescription FindByPrescriptionID(int PrescriptionID)
        {
            int AppointmentID = -1;
            string MedicationName = string.Empty;
            string Dosage = string.Empty;
            byte DurationDays = 0;
            byte? DurationMonths = null;

            if (clsPrescriptionData.FindByPrescriptionID(PrescriptionID, ref AppointmentID, ref MedicationName, ref Dosage,
                ref DurationDays, ref DurationMonths))
                return new clsPrescription(PrescriptionID, AppointmentID, MedicationName,
                    Dosage, DurationDays, DurationMonths);
            return null;
        }

        public static clsPrescription FindByAppointmentID(int AppointmentID)
        {
            int PrescriptionID = -1;
            string MedicationName = string.Empty;
            string Dosage = string.Empty;
            byte DurationDays = 0;
            byte? DurationMonths = null;

            if (clsPrescriptionData.FindByAppointmentID(AppointmentID, ref PrescriptionID, ref MedicationName, ref Dosage,
                ref DurationDays, ref DurationMonths))
                return new clsPrescription(PrescriptionID, AppointmentID, MedicationName,
                    Dosage, DurationDays, DurationMonths);
            return null;
        }
    }
}
