namespace HematosPresenter
{
    public interface ILuminexIDForm
    {
        string FormTitle { set; }

        string lblSerialNum { set; }

        string lblLuminexID { set; }

        string lblLuminexServer { set; }

        string btnCancel { set; }

        string btnSave { set; }

        bool ReturnCancel { get; set; }

        string BatchID { get; }

        DevExpress.XtraEditors.TextEdit txtSerialNumber { get; set; }

        DevExpress.XtraEditors.TextEdit txtLuminexID { get; set; }

        DevExpress.XtraEditors.TextEdit txtLuminexServer { get; set; }
    }
}