using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StkGenCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //List All Table to display
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            SqlConnection _FbConnection = new SqlConnection();
            _FbConnection.ConnectionString = textBox2.Text;
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, _FbConnection);
            adapter.Fill(ds);
            //checkedListBox1.DataSource = ds.Tables[0];

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                checkedListBox1.Items.Add(item["TABLE_NAME"].ToString(), false);
            }
        }

        //Write file by path
        private void button2_Click(object sender, EventArgs e)
        {
            //string codeAfterGen = "dsgmdkl kfjkj ekjrefjriefjirjf er irj g";

            ClearAllFile(textBox1.Text);
            GenAspx("LogX");
        }

        //Delete All Find in folder
        private void ClearAllFile(string patch)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(patch);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            // File.Delete(path);
        }

        #region apsxCode

        //Gen Aspx File
        private void GenAspx(string Table)
        {
            string aspxHtml;
            //Begin Content===================================================
            aspxHtml = "<asp:Content ID='Content1' ContentPlaceHolderID='HeadContent' runat='server'>";
            writeFile(Table, aspxHtml);

            //style -----------------------------------------------------------
            aspxHtml = "<style type='text/css'>";
            writeFile(Table, aspxHtml);
            AddSpace(Table);
            aspxHtml = "</style>";
            writeFile(Table, aspxHtml);
            AddSpace(Table);
            //-----------------------------------------------------------------

            //Script ----------------------------------------------------------
            aspxHtml = " <script type='text/javascript'>";
            writeFile(Table, aspxHtml);

            aspxHtml = "$(document).ready(function() {";
            writeFile(Table, aspxHtml);

            AddSpace(Table);

            aspxHtml = "});";
            writeFile(Table, aspxHtml);
            aspxHtml = "</script>";
            writeFile(Table, aspxHtml);
            //-----------------------------------------------------------------

            aspxHtml = @"</asp:Content>";
            writeFile(Table, aspxHtml);
            AddSpace(Table);
            //======================================================End Content

            //Begin Content====================================================
            aspxHtml = @"<asp:Content ID='Content2' ContentPlaceHolderID='MainContent' runat='server'>";
            writeFile(Table, aspxHtml);
            AddSpace(Table);

            aspxHtml = "< asp:Panel ID ='pnlSave' runat='server'>";
            AddSpace(Table);
            aspxHtml = @"</asp:Content>";
            writeFile(Table, aspxHtml);
            AddSpace(Table);
            //======================================================End Content
        }

        private void AddSpace(string Table)
        {
            string space = "\r\n";
            writeFile(Table, space);
        }

        #endregion apsxCode

        public void writeFile(string Table, string content)
        {
            string path = textBox1.Text;
            // check if directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + Table + "_" + DateTime.Today.ToString("dd-MM-yy") + ".aspx";
            // check if file exist
            //File.Delete(path);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            // log the error now
            using (StreamWriter writer = File.AppendText(path))
            {
                string error = content;
                writer.WriteLine(error);
                //writer.WriteLine("==========================================");
                writer.Flush();
                writer.Close();
            }
            //  return userFriendlyError;
        }
    }
}