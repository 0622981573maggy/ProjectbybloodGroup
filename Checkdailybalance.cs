using Guna.UI2.WinForms.Suite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace BloodBank_Video
{
    public partial class Checkdailybalance : Form
    {
        function fn = new function();
        public Checkdailybalance()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Checkdailybalance_Load(object sender, EventArgs e)
        {
            string query = "select idcard, id, daymonthyear from healthdata";
            DataSet ds = fn.getData(query);
            DataTable healthDataTable = ds.Tables[0];

            if (!string.IsNullOrEmpty(txtDate.Text))
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
            if (!string.IsNullOrEmpty(txtDate.Text))
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string query;
            DataSet ds;

            if (txtDate.Text != "")
            {
                // เมื่อ txtDate.Text ไม่เป็นค่าว่าง สร้าง SQL query ด้วยเงื่อนไขในการเลือกวันที่
                query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard WHERE hd.daymonthyear = '" + txtDate.Text + "' GROUP BY nd.bloodgroup";
            }
            else
            {
                // เมื่อ txtDate.Text เป็นค่าว่าง สร้าง SQL query ที่ไม่มีเงื่อนไข WHERE เพื่อดึงข้อมูลทั้งหมด
                query = "SELECT nd.bloodgroup, COUNT(*) AS Count FROM healthdata hd INNER JOIN tb_newdonor nd ON hd.idcard = nd.idcard GROUP BY nd.bloodgroup";
            }

            // ส่งคำสั่ง SQL ไปดึงข้อมูล
            ds = fn.getData(query);

            // กำหนดข้อมูลให้กับ DataGridView
            dataGridView1.DataSource = ds.Tables[0];

            // กำหนดรูปแบบแสดงผลของ DataGridView
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);





        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Histrory.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream pdfFileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            Document pdfDocument = new Document(PageSize.A4.Rotate()); // เปลี่ยนเป็นแนวนอน
                            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, pdfFileStream);

                            // สร้างฟอนต์ภาษาไทย
                            BaseFont thaiFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                            iTextSharp.text.Font thaiFontForCell = new iTextSharp.text.Font(thaiFont, 12, iTextSharp.text.Font.NORMAL);

                            pdfDocument.Open();

                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 5; // เพิ่มระยะห่างรอบขอบเซลล์
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_CENTER; // จัดแนวตารางกึ่งกลาง

                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.Name, thaiFontForCell));
                                pCell.HorizontalAlignment = Element.ALIGN_CENTER; // จัดแนวตัวอักษรกึ่งกลาง
                                pTable.AddCell(pCell);
                            }

                            foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                            {
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    DataGridViewCell dcell = viewRow.Cells[i];
                                    if (dcell.Value != null)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(dcell.Value.ToString(), thaiFontForCell));
                                        cell.HorizontalAlignment = Element.ALIGN_CENTER; // จัดแนวตัวอักษรกึ่งกลาง
                                        pTable.AddCell(cell);
                                    }
                                    else
                                    {
                                        pTable.AddCell(new Phrase("", thaiFontForCell));
                                    }
                                }
                            }

                            pdfDocument.Add(pTable);
                            pdfDocument.Close();
                        }
                        MessageBox.Show("ส่งออกข้อมูลสำเร็จ", "ข้อมูล");
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("ไม่พบไฟล์: " + ex.Message, "ข้อผิดพลาด");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในขณะส่งออกข้อมูล: " + ex.Message, "ข้อผิดพลาด");
                    }
                }
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูล", "ข้อมูล");
            }
            //////
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
