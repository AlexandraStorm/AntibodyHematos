using System;

namespace HematosPresenter
{
    public interface ILuminexDataManagement
    {
        string FormTitle { set; }

        string lblSerialNum { set; }

        string lblLuminexID { set; }

        string lblLuminexServer { set; }

        string btnClear { set; }

        string btnSave { set; }

        string btnSelect { set; }

        //DevExpress.XtraEditors.TextEdit txtLumID { get; set; }
        DevExpress.XtraEditors.TextEdit txtSerialNumber { get; set; }

        DevExpress.XtraEditors.TextEdit txtLuminexID { get; set; }

        DevExpress.XtraEditors.TextEdit txtLuminexServer { get; set; }

        DevExpress.XtraGrid.GridControl gridLuminexData { get; set; }

        event EventHandler gridRowSelection;
    }
}