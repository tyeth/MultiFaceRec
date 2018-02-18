using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiFaceRec
{
    public partial class frmSettings : Form
    {
        private bool unsavedChanges = false;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UpdatePrivacyList();
            UpdateDbSettings();
            this.Close();
        }

        private void UpdateDbSettings()
        {
            throw new NotImplementedException();
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
                button1_Click_1(this,new EventArgs());
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
            if (e.KeyCode == Keys.Enter  || e.KeyCode==Keys.Return)
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
        {   listBox1.Items.Clear();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
