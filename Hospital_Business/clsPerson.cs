using System;
using Hospital_Data;

namespace Hospital_Business
{
    public class clsPerson
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte Gender { get; set; } // 0 -> Male, 1 -> Female

        public string FullName
        { 
            get => FirstName + " " + LastName;  
        }


        public enum enMode { AddNew = 1, Update = 2 };
        protected enMode Mode;

        protected clsPerson(int PersonID, string FirstName, string LastName, string Phone, string Email, byte Gender)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this.Gender = Gender;
            this.Mode = enMode.Update;
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.Gender = 0;
            this.Mode = enMode.AddNew;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.Phone, this.Email, this.Gender);
            return this.PersonID > 0;
        }

        private bool _UpdatePerson() => clsPersonData.UpdatePerson(this.PersonID, this.FirstName,
                this.LastName, this.Phone, this.Email, this.Gender);

        public virtual bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if(this._AddNewPerson())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = string.Empty;
            string LastName = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            byte Gender = 0;

            if (clsPersonData.Find(PersonID, ref FirstName, ref LastName, ref Phone, ref Email, ref Gender))
                return new clsPerson(PersonID, FirstName, LastName, Phone,
                    Email, Gender);
            return null;
        }
    }
}
