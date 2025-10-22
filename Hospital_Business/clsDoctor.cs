using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsDoctor : clsPerson
    {
        public int DoctorID { get; set; }
        public byte StartWorkDay { get; set; }
        public byte EndWorkDay { get; set; }
        public TimeSpan StartWorkHour { get; set; }
        public TimeSpan EndWorkHour { get; set; }
        public int ConsultationID { get; set; }
        public clsConsultation ConsultationInfo;
        public int UserID { get; set; }
        public clsUser User;

        public new enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public string GetDayName(byte DayNumber)
        {
            switch (DayNumber)
            {
                case 0:
                    return "Sunday";
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
            }
            return string.Empty;
        }

        public clsDoctor() : base()
        {
            this.DoctorID = 0;
            this.StartWorkDay = 1;
            this.EndWorkDay = 1;
            this.StartWorkHour = TimeSpan.MinValue;
            this.EndWorkHour = TimeSpan.MinValue;
            this.ConsultationID = -1;
            this.UserID = 1;
            _Mode = enMode.AddNew;
        }

        private clsDoctor(int doctorID, int personID, string FirstName, string LastName,
            string Phone, string Email, byte startWorkDay, byte endWorkDay,
            TimeSpan startWorkHour, TimeSpan endWorkHour, int consultationID, int userID, byte gender)
        {
            DoctorID = doctorID;
            this.PersonID = personID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            StartWorkDay = startWorkDay;
            EndWorkDay = endWorkDay;
            StartWorkHour = startWorkHour;
            EndWorkHour = endWorkHour;
            ConsultationID = consultationID;
            ConsultationInfo = clsConsultation.Find(consultationID);
            UserID = userID;
            User = clsUser.Find(userID);
            base.Gender = gender;
            _Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.StartWorkDay, this.EndWorkDay,
                this.StartWorkHour, this.EndWorkHour, this.ConsultationID);
            return this.DoctorID > 0;
        }

        private bool _UpdateDoctor() => clsDoctorData.UpdateDoctor(this.DoctorID, this.FirstName, this.LastName, this.Phone,
                this.Email, this.StartWorkDay, this.EndWorkDay, this.StartWorkHour, this.EndWorkHour,
                this.ConsultationID);

        public new bool Save()
        {
            base.Mode = (clsPerson.enMode)this._Mode;
            if (!base.Save())
                return false;

            switch (this._Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDoctor())
                        {
                            base.Mode = clsPerson.enMode.Update;
                            this._Mode = clsDoctor.enMode.Update;
                            return true;
                        }
                    }
                    break;
                case enMode.Update:
                    return _UpdateDoctor();
            }
            return false;
        }

        public static bool Delete(int DoctorID) => clsDoctorData.DeleteDoctor(DoctorID);

        public static int GetDocotrsCount() => clsDoctorData.GetDoctorsCount();

        public static async Task<DataTable> GetAllDoctorsAsync() => await clsDoctorData.GetAllDoctorsAsync();

        public static clsDoctor Find(int DoctorID)
        {
            int PersonID = 0;
            byte StartWorkDay = 0;
            byte EndWorkDay = 0;
            TimeSpan StartWorkHour = TimeSpan.MinValue;
            TimeSpan EndWorkHour = TimeSpan.MinValue;
            int ConsultationID = 0;
            int UserID = 0;

            if (clsDoctorData.Find(DoctorID, ref PersonID, ref StartWorkDay,
                ref EndWorkDay, ref StartWorkHour, ref EndWorkHour, ref ConsultationID, ref UserID))
            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsDoctor(DoctorID, PersonID, Person.FirstName, Person.LastName,
                    Person.Phone, Person.Email, StartWorkDay, EndWorkDay,
                    StartWorkHour, EndWorkHour, ConsultationID, UserID, Person.Gender);
            }
            return null;
        }

        public static clsDoctor FindByUserID(int UserID)
        {
            int DoctorID = 0;
            int PersonID = 0;
            byte StartWorkDay = 0;
            byte EndWorkDay = 0;
            TimeSpan StartWorkHour = TimeSpan.MinValue;
            TimeSpan EndWorkHour = TimeSpan.MinValue;
            int ConsultationID = 0;

            if (clsDoctorData.Find(UserID, ref DoctorID, ref PersonID, ref StartWorkDay,
                ref EndWorkDay, ref StartWorkHour, ref EndWorkHour, ref ConsultationID))
            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsDoctor(DoctorID, PersonID, Person.FirstName, Person.LastName,
                    Person.Phone, Person.Email, StartWorkDay, EndWorkDay,
                    StartWorkHour, EndWorkHour, ConsultationID, UserID, Person.Gender);
            }
            return null;
        }

        public async static Task<DataTable> GetTodaysAppointmentsForDoctor(int DoctorID) =>
            await clsDoctorData.GetTodaysAppointmentsForDoctor(DoctorID);

        public static int GetPatientsCountForDoctor(int DoctorID) => clsDoctorData.GetPatientsCountForDoctor(DoctorID);

        public static int GetAppointmentsCountForDoctor(int DoctorID) => clsDoctorData.GetAppointmentsCountForDoctor(DoctorID);

        public static int GetMedicalRecordsCountForDoctor(int DoctorID) => clsDoctorData.GetMedicalRecordsCountForDoctor(DoctorID);

        public bool DoesDoctorWorkOn(byte Day) =>
            (this.StartWorkDay > this.EndWorkDay)
            ? (Day >= StartWorkDay || Day <= EndWorkDay) // wraparound case
            : (Day >= StartWorkDay && Day <= EndWorkDay); // normal case
    }
}
