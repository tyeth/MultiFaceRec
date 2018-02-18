using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace MultiFaceRec
{
    public partial class frmSettings : Form
    {
        private bool _changes = false;

        private bool unsavedChanges
        {
            get { return _changes; }
            set
            {
                _changes = value;
                btnSave.Enabled = value == true;
            }
        }

        public frmSettings()
        {
            InitializeComponent();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdatePrivacyList();
            UpdateDbSettings();
            unsavedChanges = false;
            this.Close();
        }

        private void UpdateDbSettings()
        {
            _senderFrm.settingsCollection.DeleteMany(x => x.Key == "PrivacyList");
            foreach (var VARIABLE in listBox1.Items)
            {
                _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("PrivacyList", VARIABLE.ToString()));
            }

            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "MongoDbUrl");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("MongoDbUrl", txtMongoUrl.Text));
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "MongoDbName");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("MongoDbName", txtDatabaseName.Text));
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Settings");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Settings", txtSettings.Text));
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Trusted");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Trusted", txtTrusted.Text));
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Scanned");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Scanned", txtScanned.Text));
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Villains");
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Villains", txtVillains.Text));

            if (txtSettings.Text != "settings" || txtMongoUrl.Text != "mongodb://localhost:27017" || txtDatabaseName.Text != "faces")
            {
                //Write to new server / database / collection

                // Refresh FrmPrincipal Data Connection Settings and reinitialise.
                _senderFrm.MongoUrl = txtMongoUrl.Text;
                _senderFrm.MongoDb = txtDatabaseName.Text;
                _senderFrm.MongoSettingsCollection = txtSettings.Text;
                _senderFrm.MongoTrustedCollection = txtTrusted.Text;
                _senderFrm.MongoScannedCollection = txtScanned.Text;
                _senderFrm.MongoVillainsCollection = txtVillains.Text;

            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

        internal FrmPrincipal _senderFrm;

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            var mylist = _senderFrm.privacyList;
            var list = mylist.ToArray<object>();
            if (listBox1.Items.Count != list.Length)
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(list);
            }

            txtMongoUrl.Text = _senderFrm.MongoUrl;
            txtDatabaseName.Text = _senderFrm.MongoDb;
            txtSettings.Text = _senderFrm.MongoSettingsCollection;
            txtTrusted.Text = _senderFrm.MongoTrustedCollection;
            txtScanned.Text = _senderFrm.MongoScannedCollection;
            txtVillains.Text = _senderFrm.MongoVillainsCollection;
            unsavedChanges = false;
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (unsavedChanges)
            {
                var ret = MessageBox.Show("Unsaved changes, are you sure you want to discard the changes and close this window?", "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (ret == DialogResult.No) e.Cancel = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.Items.Contains(comboBox1.Text) == false)
            {
                listBox1.Items.Add(comboBox1.Text);
                comboBox1.Text = "";
                unsavedChanges = true;

            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                button1_Click_1(this, new EventArgs());
            }
        }


        private void UpdatePrivacyList()
        {

            _senderFrm.privacyList.Clear();
            foreach (var item in listBox1.Items)
            {
                _senderFrm.privacyList.Add(item.ToString());
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                button1_Click_1(this, new EventArgs());
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeletePrivacyItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 1) return;
            var item = listBox1.SelectedItem.ToString();
            listBox1.Items.Remove(item);
            unsavedChanges = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0) btnDeletePrivacyItem.Enabled = true;
            else btnDeletePrivacyItem.Enabled = false;
        }

        private void btnResetPrivacyList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(new List<string>()
            {
                "outlook",
                "microsoftedgecp",
                "microsoftedge",
                "edge",
                "firefox",
                "chrome",
                "winword",
                "msteams",
                "teams",
                "skype",
                "hangouts",
                "thunderbird",
                "eudora",
                "mail",
                "hxoutlook"
            }.ToArray<object>());
            unsavedChanges = true;
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (listBox1.SelectedItems.Count != 1) return;
                var item = listBox1.SelectedItem.ToString();
                listBox1.Items.Remove(item);
                unsavedChanges = true;
            }
        }


        private void txtMongoUrl_TextChanged(object sender, EventArgs e)
        {
            if (txtMongoUrl.Text != _senderFrm.MongoUrl) unsavedChanges = true;

        }

        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            if (txtDatabaseName.Text != _senderFrm.MongoDb) unsavedChanges = true;

        }

        private void txtTrusted_TextChanged(object sender, EventArgs e)
        {
            if (txtTrusted.Text != _senderFrm.MongoTrustedCollection) unsavedChanges = true;

        }

        private void txtSettings_TextChanged(object sender, EventArgs e)
        {
            if (txtSettings.Text != _senderFrm.MongoSettingsCollection) unsavedChanges = true;

        }

        private void txtScanned_TextChanged(object sender, EventArgs e)
        {
            if (txtScanned.Text != _senderFrm.MongoScannedCollection) unsavedChanges = true;

        }

        private void txtVillains_TextChanged(object sender, EventArgs e)
        {
            if (txtVillains.Text != _senderFrm.MongoVillainsCollection) unsavedChanges = true;

        }
    }
}
