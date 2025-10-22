using Hospital_Data;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsPatient : clsPerson
    {
        public int PatientID { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }

        public enum enMode { AddNew = 1, Update =2 }
        private enMode _Mode;

        public clsPatient() : base()
        {
            this.PatientID = 0;
            this.NationalNo = string.Empty;
            this.DateOfBirth = DateTime.MinValue;
            this.Address = string.Empty;
            base.Gender = 0;
            this.EmergencyContact = string.Empty;
            this._Mode = enMode.AddNew;
            base.Mode = clsPerson.enMode.AddNew;
        }

        private clsPatient(int PersonID, string FirstName, string LastName, string Phone, string Email, int PatientID,
            string NationalNo, DateTime DateOfBirth, string Address, byte Gender, string EmergencyContact)
        {
            base.PersonID = PersonID;
            base.FirstName = FirstName;
            base.LastName = LastName;
            base.Phone = Phone;
            base.Email = Email;
            this.PatientID = PatientID;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            base.Gender = Gender;
            this.EmergencyContact = EmergencyContact;
            this._Mode = enMode.Update;
            base.Mode = clsPerson.enMode.Update;
        }


        public static Task<DataTable> GetAllPatientsAysnc() => clsPatientData.GetAllPatientsAsync();

        private bool _AddNewPatient()
        {
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID, this.NationalNo, this.DateOfBirth,
                this.Address, this.EmergencyContact);
            return this.PatientID != -1;
        }

        private bool _UpdatePatient() => clsPatientData.UpdatePatient(this.PatientID, this.NationalNo, this.DateOfBirth,
            this.Address, this.EmergencyContact);

        public override bool Save()
        {
            if(!base.Save())
                return false;

            switch (_Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewPatient())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdatePatient();
            }
            return false;
        }

        public static clsPatient Find(int PatientID)
        {
            int PersonID = 0;
            string NationalNo = string.Empty;
            DateTime DateOfBirth = DateTime.MinValue;
            string Address = string.Empty;
            string EmergencyContact = string.Empty;

            if (clsPatientData.Find(PatientID, ref PersonID, ref NationalNo, ref DateOfBirth, 
                ref Address, ref EmergencyContact))

            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsPatient(Person.PersonID, Person.FirstName, Person.LastName, Person.Phone, Person.Email,
                    PatientID, NationalNo, DateOfBirth, Address, Person.Gender, EmergencyContact);
            }
            return null;
        }

        public static clsPatient Find(string NationalNo)
        {
            int PersonID = 0;
            int PatientID = 0;
            DateTime DateOfBirth = DateTime.MinValue;
            string Address = string.Empty;
            string EmergencyContact = string.Empty;

            if (clsPatientData.Find(NationalNo, ref PatientID, ref PersonID, ref DateOfBirth,
                ref Address, ref EmergencyContact))
            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsPatient(Person.PersonID, Person.FirstName, Person.LastName, Person.Phone, Person.Email,
                    PatientID, NationalNo, DateOfBirth, Address, Person.Gender, EmergencyContact);
            }
            return null;
        }

        public static bool DeletePatient(int PatientID) => clsPatientData.DeletePatient(PatientID);

        public static bool DoesNationalNoExist(string NationalNo) => clsPatientData.DoesNationalNoExist(NationalNo);
    
        public static async Task<DataTable> GetPatientHistoryAsync(int PatientID, bool IsMedicalRecord) =>
            await clsPatientData.GetPatientMedicalRecordsHistoryAsync(PatientID, IsMedicalRecord);

        public static bool HasPatientMedicalRecords(int PatientID) => clsPatientData.HasPatientMedicalRecords(PatientID);

        public static bool HasPatientPrescriptions(int PatientID) => clsPatientData.HasPatientPrescriptions(PatientID);

        public static bool HasPatientAppointmentAt(int PatientID, DateTime AppointmentDate, TimeSpan AppointmentTime) =>
            clsPatientData.HasPatientAppointmentAt(PatientID, AppointmentDate, AppointmentTime);
    }
}
