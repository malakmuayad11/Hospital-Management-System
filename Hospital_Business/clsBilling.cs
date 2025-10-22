using Hospital_Data;
using System.Data;
using System.Threading.Tasks;

namespace Hospital_Business
{
    public class clsBilling
    {
        public int BillingID { get; set; }
        public int AppointmentID { get; set; }

        public clsAppointment AppointmentInfo;
        public decimal ConsulationFee { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal TotalAmount { get => ConsulationFee + (AdditionalCharges ?? 0); }
        public bool IsPaid { get; set; }
        public byte? PaymentMethod { get; set; }

        public string PaymentMethodString
        {
            get
            {
                switch(PaymentMethod)
                {
                    case 0:
                        return "Credit Card";
                    case 1:
                        return "Cash";
                }
                return null;
            }
        }

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public clsBilling()
        {
            this.BillingID = -1;
            this.AppointmentID = -1;
            this.ConsulationFee = -1;
            this.AdditionalCharges = null;
            this.IsPaid = false;
            this._Mode = enMode.AddNew;
        }

        private clsBilling(int BillingID, int AppointmentID, decimal ConsultationFee, decimal? AdditionalFee,  bool IsPaid, byte? PaymentMethod)
        {
            this.BillingID = BillingID;
            this.AppointmentID = AppointmentID;
            AppointmentInfo = clsAppointment.Find(AppointmentID);
            this.ConsulationFee = ConsultationFee;
            this.AdditionalCharges = AdditionalFee;
            this.IsPaid = IsPaid;
            this._Mode = enMode.Update;
            this.PaymentMethod = PaymentMethod;
        }
        public static Task<DataTable> GetAllBillingsAysnc() => clsBillingData.GetAllBillingsAsync();
        public bool UpdateBillingCharges(int BillingID, decimal? AdditionalCharges) =>
            clsBillingData.UpdateBillingCharges(BillingID, AdditionalCharges);

        public bool UpdateBillingPaymentStatus(int BillingID, bool IsPaid, byte? PaymentMethod) =>
            clsBillingData.UpdateBillingPaymentStatus(BillingID, IsPaid, PaymentMethod);

        public static clsBilling Find(int BillingID)
        {
            int AppointmentID = -1;
            decimal ConsultationFee = -1;
            decimal? AdditionalFee = null;
            bool IsPaid = false;
            byte? PaymentMethod = null;

            if (clsBillingData.Find(BillingID, ref AppointmentID, ref ConsultationFee, ref AdditionalFee, ref IsPaid, ref PaymentMethod))
                return new clsBilling(BillingID, AppointmentID, ConsultationFee, AdditionalFee, IsPaid, PaymentMethod);
            return null;
        }
    }
}
