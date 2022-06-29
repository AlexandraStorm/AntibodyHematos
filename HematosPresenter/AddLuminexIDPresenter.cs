using HematosBO;
using System.Reflection;

namespace HematosPresenter
{
    public class AddLuminexIDPresenter
    {
        private readonly ILuminexIDForm _view;

        public AddLuminexIDPresenter(ILuminexIDForm view)
        {
            _view = view;
            SetLabels();
        }

        private void SetLabels()
        {
            System.Resources.ResourceManager myManager = new System.Resources.ResourceManager("HematosPresenter.Resources.LuminexDataManagementResource", Assembly.GetExecutingAssembly());
            _view.btnCancel = myManager.GetString("ButtonCancel");
            _view.btnSave = myManager.GetString("ButtonSave");
            _view.lblSerialNum = myManager.GetString("LabelSerialNumber");
            _view.lblLuminexID = myManager.GetString("LabelLuminexID");
            _view.lblLuminexServer = myManager.GetString("LabelLuminexServer");
            _view.FormTitle = myManager.GetString("FormTitle");
        }

        //public void ClearLuminexDataForm()
        //{
        //    var _with1 = _view;
        //    _with1.txtLuminexID.Text = string.Empty;
        //    _with1.txtLuminexServer.Text = string.Empty;
        //    _with1.txtSerialNumber.Text = string.Empty;
        //}

        public int SaveLuminexData()
        {
            LuminexDataBO luminex = new LuminexDataBO() { SerialNumber = _view.txtSerialNumber.Text, LuminexID = _view.txtLuminexID.Text, LuminexServer = _view.txtLuminexServer.Text };

            int result = luminex.InsertLuminexData("Supervisor");

            luminex = null;
            return result;
        }
    }
}