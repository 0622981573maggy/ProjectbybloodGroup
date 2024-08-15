using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank_Video
{
    public partial class monthcheckdaily : Form
    {
        function fn = new function();
        public monthcheckdaily()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void monthcheckdaily_Load(object sender, EventArgs e)
        {
            string query = "select idcard, id, daymonthyear from healthdata";
            DataSet ds = fn.getData(query);
            DataTable healthDataTable = ds.Tables[0];

            if (!string.IsNullOrEmpty(comboBoxMonth.Text))
            {
                string queryNewDonor = "select idcard, name, surname, bloodgroup from tb_newdonor";
                DataSet dsNewDonor = fn.getData(queryNewDonor);
                DataTable newDonorDataTable = dsNewDonor.Tables[0];

                healthDataTable.Columns.Add("Name", typeof(string));
                healthDataTable.Columns.Add("Surname", typeof(string));
                healthDataTable.Columns.Add("BloodGroup", typeof(string));

                foreach (DataRow row in healthDataTable.Rows)
                {
                    DataRow[] matchingRows = newDonorDataTable.Select("idcard = '" + row["idcard"] + "'");

                    if (matchingRows.Length > 0)
                    {
                        row["Name"] = matchingRows[0]["name"];
                        row["Surname"] = matchingRows[0]["surname"];
                        row["BloodGroup"] = matchingRows[0]["bloodgroup"];
                    }
                }
            }
            // สร้างคอลัมน์ count ในตาราง healthDataTable
            healthDataTable.Columns.Add("Count", typeof(int));

            // เช็คว่ามีข้อมูลใน TextBox วันที่หรือไม่
            if (!string.IsNullOrEmpty(comboBoxMonth.Text))
            {
                // นับจำนวนแถวในตาราง healthDataTable
                int rowCount = healthDataTable.Rows.Count;

                // เพิ่มค่าจำนวนแถวในคอลัมน์ count สำหรับทุกแถว
                foreach (DataRow row in healthDataTable.Rows)
                {
                    row["Count"] = rowCount;
                }
            }

            dataGridView1.DataSource = healthDataTable;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            string selectedMonth = comboBoxMonth.SelectedItem.ToString();
            string selectedYear = comboBoxYear.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedMonth) && !string.IsNullOrEmpty(selectedYear))
            {
                // สร้าง SQL query ด้วยเงื่อนไขในการเลือกเดือนและปี
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard WHERE MONTH(hd.daymonthyear) = " + selectedMonth + " AND YEAR(hd.daymonthyear) = " + selectedYear + " GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // สร้าง SQL query ที่ไม่มีเงื่อนไข WHERE เพื่อดึงข้อมูลทั้งหมด
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }















        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            

        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMonth = comboBoxMonth.SelectedItem?.ToString();
            string selectedYear = comboBoxYear.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMonth) && !string.IsNullOrEmpty(selectedYear))
            {
                // สร้าง SQL query ด้วยเงื่อนไขในการเลือกเดือนและปี
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard WHERE MONTH(hd.daymonthyear) = " + selectedMonth + " AND YEAR(hd.daymonthyear) = " + selectedYear + " GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // สร้าง SQL query ที่ไม่มีเงื่อนไข WHERE เพื่อดึงข้อมูลทั้งหมด
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMonth = comboBoxMonth.SelectedItem?.ToString();
            string selectedYear = comboBoxYear.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMonth) && !string.IsNullOrEmpty(selectedYear))
            {
                // สร้าง SQL query ด้วยเงื่อนไขในการเลือกเดือนและปี
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard WHERE MONTH(hd.daymonthyear) = " + selectedMonth + " AND YEAR(hd.daymonthyear) = " + selectedYear + " GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // สร้าง SQL query ที่ไม่มีเงื่อนไข WHERE เพื่อดึงข้อมูลทั้งหมด
                string query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard GROUP BY nd.bloodgroup";

                // ส่งคำสั่ง SQL ไปดึงข้อมูล
                DataSet ds = fn.getData(query);

                // กำหนดข้อมูลให้กับ DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
