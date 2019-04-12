using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using SmartShipment.UI.Common;
using SmartShipment.UI.Views;
using Button = System.Windows.Forms.Button;
using CheckBox = System.Windows.Forms.CheckBox;
using ComboBox = System.Windows.Forms.ComboBox;
using Control = System.Windows.Forms.Control;
using RadioButton = System.Windows.Forms.RadioButton;
using TextBox = System.Windows.Forms.TextBox;


namespace SmartShipment.UI.Forms
{
    public partial class SmartShipmentSettingsDialog : SmartShipmentBaseForm, ISmartSettingsDialogView
    {
        public event Action OnSettingsSave;
        public event Action OnSettingsCancel;
        public event Action OnTestLoginClick;
        public event Action OnFormLoad;
        
        public Form Form => this;

        public TextBox TextAcumaticaLogin => textAcumaticaLogin;
        public TextBox TextAcumaticaPassword => textAcumaticaPassword;
        public TextBox TextAcumaticaCompany => textAcumaticaCompany;
        public TextBox TextAcumaticaBaseUrl => textBoxBaseUrl;
        public TextBox TextAcumaticaDefaultBoxId => textBoxAcumaticaDefaultBoxId;
        public CheckBox AcumaticaConfirmShipments => checkBoxAcumaticaConfirmShipment;
        public CheckBox UpsAddUpdateAddressBook => checkBoxUpsAddUpdateAddressBook;
        public CheckBox FedexAddUpdateAddressBook => checkBoxFedexAddUpdateAddressBook;
        public Button TestButton => btnAcumaticaTestLogin;
        public ComboBox GeneralTransferSpeed => comboBoxTransferSpeed;


        public SmartShipmentSettingsDialog()
        {
            InitializeComponent();
           
            SetControlsVisible();
            SetTransferSpeedItems();         
            btnAcumaticaTestLogin.Click += (sender, args) => Invoke(OnTestLoginClick);
            btnOk.Click += (sender, args) => Invoke(OnSettingsSave);
            btnCancel.Click += (sender, args) => Invoke(OnSettingsCancel);      
            Load += (sender, args) => Invoke(OnFormLoad);
        }

        protected override void OnLoad(EventArgs e)
        {
           base.OnLoad(e);
            if (comboBoxTransferSpeed.SelectedIndex == -1)
            {
                comboBoxTransferSpeed.SelectedIndex = 1;
            }
        }

        private void SetTransferSpeedItems()
        {
            var items = new BindingList<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Slow", 1500),
                new KeyValuePair<string, int>("Medium", 1000),
                new KeyValuePair<string, int>("Fast", 500)
            };

            comboBoxTransferSpeed.DataSource = items;
            comboBoxTransferSpeed.ValueMember = "Value";
            comboBoxTransferSpeed.DisplayMember = "Key";
        }

        public new void Show()
        {
            ShowDialog();
        }

        public void SetFormAttributes()
        {
            Text = GetAssemblyInformation(Text);            
        }

        public void SetControlsVisible()
        {

        }

        private string GetAssemblyInformation(string formTitle)
        {
            var currentAssembly = typeof(ApplicationMain).Assembly;
            var version = "0.0.0.0";
            var title = "Generic application";

            var attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
            if (attributes.Any())
            {
                version = ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }

            attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attributes.Any())
            {
                title = ((AssemblyTitleAttribute)attributes[0]).Title;
            }

            return formTitle.Replace("{applicationTitle}", title).Replace("{version}", version);
        }
    }
}
