using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson.Serialization;
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
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i,true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdatePrivacyList();
            UpdateDbSettings();
            unsavedChanges = false;
            this.Close();
        }

        private void UpdateDbSettings(bool ignore=false)
        {
            try
            {

            _senderFrm.settingsCollection.DeleteMany(x => x.Key == "PrivacyList");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "MongoDbUrl");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "MongoDbName");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Settings");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Trusted");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Scanned");
            _senderFrm.settingsCollection.DeleteOne(x => x.Key == "Villains");
            }catch(Exception e) { }
            foreach (var VARIABLE in listBox1.Items)
            {
                _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("PrivacyList", VARIABLE.ToString()));
            }

            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("MongoDbUrl", txtMongoUrl.Text));
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("MongoDbName", txtDatabaseName.Text));
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Settings", txtSettings.Text));
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Trusted", txtTrusted.Text));
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Scanned", txtScanned.Text));
            _senderFrm.settingsCollection.InsertOne(new KeyValuePair<string, string>("Villains", txtVillains.Text));

            var settingsListBoxItems = GetListBoxSettings(checkedListBox1.Items);

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (settingsListBoxItems.Count(x => x.Key == checkedListBox1.Items[i].ToString()) > 0)
                {
                    _senderFrm.settingsCollection.DeleteOne(x => x.Key == checkedListBox1.Items[i].ToString());
                }
                _senderFrm.settingsCollection.InsertOne(
                    new KeyValuePair<string, string>(
                        checkedListBox1.Items[i].ToString(),
                        checkedListBox1.CheckedIndices.Contains(i)
                            ?  "1"  :  "0"
                        )
                    );
            }


            if (ignore==false ||
                txtSettings.Text != _senderFrm.MongoSettingsCollection ||
                txtMongoUrl.Text != _senderFrm.MongoUrl ||
                txtDatabaseName.Text != _senderFrm.MongoDb ||
                txtScanned.Text!= _senderFrm.MongoScannedCollection ||
                txtVillains.Text!= _senderFrm.MongoVillainsCollection ||
                txtTrusted.Text!= _senderFrm.MongoTrustedCollection
                )
            {
                // Refresh FrmPrincipal Data Connection Settings and reinitialise.
                _senderFrm.MongoUrl = txtMongoUrl.Text;
                _senderFrm.MongoDb = txtDatabaseName.Text;
                _senderFrm.MongoSettingsCollection = txtSettings.Text;
                _senderFrm.MongoTrustedCollection = txtTrusted.Text;
                _senderFrm.MongoScannedCollection = txtScanned.Text;
                _senderFrm.MongoVillainsCollection = txtVillains.Text;
                _senderFrm.InitialiseDb();
                //Write to new server / database / collection
                UpdateDbSettings(ignore = true);
                _senderFrm.InitialiseDb();
                _senderFrm.LoadSettings();

                //TODO: reinitialise TrainedImages and Labels and FacesCounter etc.
                //NEED TO REINITIALISE TRAINEDIMAGES
                try
                {
                    _senderFrm.LoadTrainedFacesForStartup();
                }
                catch (DataException de)
                {
                    MessageBox.Show($"No Images on chosen database ({txtDatabaseName.Text}) in chosen collection ({txtSettings.Text}) using '{txtMongoUrl.Text}'");
                }
                
               

               
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

            var settingsListBoxItems = GetListBoxSettings(checkedListBox1.Items);
            var selectedIndices = checkedListBox1.CheckedIndices;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var dbSettingForItem = settingsListBoxItems.FirstOrDefault(x => x.Key == checkedListBox1.Items[i].ToString());
                if (dbSettingForItem.Value!=new KeyValuePair<string,string>().Value)
                { //Non-default value, something worth updating
                      checkedListBox1.SetItemChecked( i,(bool)( dbSettingForItem.Value == "1"));
                }
                
            }
            checkedListBox1.Refresh();

            unsavedChanges = false;
        }


        private IList<KeyValuePair<string, string>> GetListBoxSettings(ListBox.ObjectCollection keysEnumerable)
        {
            var list = new List<string>(keysEnumerable.Count);
            foreach (var keyObject in keysEnumerable)
            {
                list.Add(keyObject.ToString());
            }
            return GetListBoxSettings(list);
        }

        private IList<KeyValuePair<string, string>> GetListBoxSettings(IEnumerable<string> keysEnumerable)
        {
            var ret = new List<KeyValuePair<string, string>>();
            var filterBuilder = Builders<KeyValuePair<string, string>>.Filter;
            var filterarr = new List<FilterDefinition<KeyValuePair<string,string>>>( );
            foreach (var item in keysEnumerable)
            {
                filterarr.Add(filterBuilder.Eq<string>("k", item.ToString()));
            }

            var filter = filterBuilder.Or(filterarr);
       

            var projection = Builders<KeyValuePair<string, string>>.Projection
                    .Exclude("_id")
                    .Include("k")
                    .Include("v")
              
                ;

            var list = _senderFrm.settingsCollection.Find(filter).Project(projection).ToList();//.ToList<KeyValuePair<string,string>>();///*.Project(projection)*/.FirstOrDefault();

           ret= list.Select(x=> BsonSerializer.Deserialize<KeyValuePair<string, string>>(x)).ToList();
            return ret;
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (unsavedChanges)
            {
                var ret = MessageBox.Show("Unsaved changes, are you sure you want to discard the changes and close this window?", "Discard Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (ret == DialogResult.No) e.Cancel = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
                btnAdd_Click(this, new EventArgs());
                comboBox1.Focus();
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
                btnAdd_Click(this, new EventArgs());
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

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (btnSave.Focused)
            {
                btnAdd.Focus();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            //var control = sender as Control;
            //if (control != comboBox1)
            //{


            //    if (!btnSave.Focused) btnSave.Focus();
            //    control.Focus();
            //}
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //TODO: Check with FrmPrincipal if values are different and set unsavedChanges accordingly.
            unsavedChanges = true;
        }
    }
}
