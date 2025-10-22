using Hospital_Data;
using Krypton.Toolkit;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsAppointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public clsDoctor DoctorInfo;
        public int PatientID {  get; set; }
        public clsPatient PatientInfo;
        public DateTime AppointmentDate {  get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string ReasonForVisit { get; set; }
        public byte Status { get; set; }

        public string StatusString
        {
            get
            {
                switch(Status)
                {
                    case 1:
                        return "Scheduled";
                    case 2:
                        return "Rescheduled";
                    case 3:
                        return "Cancelled";
                    case 4:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        private enum enMode { AddNew = 1, Update =2 }
        private enMode _Mode;

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.DoctorID = -1;
            this.PatientID = -1;
            this.AppointmentDate = DateTime.MinValue;
            this.AppointmentTime = TimeSpan.MinValue;
            this.ReasonForVisit = string.Empty;
            this.Status = 0;
            this._Mode = enMode.AddNew;
        }

        private clsAppointment(int AppointmentID, int DoctorID, int PatientID,
            DateTime AppointmentDate, TimeSpan AppointmentTime, string ReasonForVisit,
            byte Status)
        {
            this.AppointmentID = AppointmentID;
            this.DoctorID = DoctorID;
            this.DoctorInfo = clsDoctor.Find(DoctorID);
            this.PatientID = PatientID;
            this.PatientInfo = clsPatient.Find(PatientID);
            this.AppointmentDate = AppointmentDate;
            this.AppointmentTime =  AppointmentTime;
            this.ReasonForVisit = ReasonForVisit;
            this.Status = Status;
            this._Mode = enMode.Update;
        }

        public static int GetAppointmentsCount() => clsAppointmentData.GetAppointmentsCount();

        public static Task<DataTable> GetTodaysAppointments() => clsAppointmentData.GetTodaysAppointmentsAsync();

        public static Task<DataTable> GetAllAppointments() => clsAppointmentData.GetAllAppointments();

        private bool _AddNewAppointment()
        {
            this.AppointmentID = clsAppointmentData.AddNewAppointment(this.DoctorID, this.PatientID, this.AppointmentDate,
                this.AppointmentTime, this.ReasonForVisit);
            return this.AppointmentID != -1;
        }
        private bool _UpdateAppointment(int AppointmentID, DateTime AppointmentDate, TimeSpan AppointmentTime) =>
            clsAppointmentData.UpdateAppointment(AppointmentID, AppointmentDate, AppointmentTime);

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewAppointment())
                    {
                        this._Mode = enMode.Update;
                        this.Status = 1;
                        return true;
                    }
                    break;
                case enMode.Update:;
                    return this._UpdateAppointment(AppointmentID, AppointmentDate, AppointmentTime);
            }
            return false;
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int DoctorID = -1;
            int PatientID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            TimeSpan AppointmentTime = TimeSpan.MinValue;
            string ReasonForVisit = string.Empty;
            byte Status = 0;

            if (clsAppointmentData.Find(AppointmentID, ref DoctorID, ref PatientID, ref AppointmentDate,
                ref AppointmentTime, ref ReasonForVisit, ref Status))
                return new clsAppointment(AppointmentID, DoctorID, PatientID, AppointmentDate,
                    AppointmentTime, ReasonForVisit, Status);
            return null;
        }

        public static bool CancelAppointment(int AppointmentID) => clsAppointmentData.CancelAppointment(AppointmentID);

        public static bool HasAppointmentMedicalRecord(int AppointmentID) =>
            clsAppointmentData.HasAppointmentMedicalRecord(AppointmentID) == 1;

        public static bool UpdateAppointmentStatus(int AppointmentID, byte Status) => clsAppointmentData.UpdateAppointmentStatus(AppointmentID, Status);
    }
}
