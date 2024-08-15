using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank_Video
{
    public partial class Searchaginhealdata : Form
    {
        function fn = new function();
        string query;
        public Searchaginhealdata()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Searchaginhealdata_Load(object sender, EventArgs e)
        {
            // เรียกข้อมูลจากตาราง healthdata
            string query = "select idcard, id, daymonthyear from healthdata";
            DataSet ds = fn.getData(query);
            DataTable healthDataTable = ds.Tables[0];

            // เรียกข้อมูลจากตาราง tb_newdonor
            string queryNewDonor = "select idcard, name, surname from tb_newdonor";
            DataSet dsNewDonor = fn.getData(queryNewDonor);
            DataTable newDonorDataTable = dsNewDonor.Tables[0];

            // เพิ่มคอลัมน์ name และ surname จาก DataTable ของ tb_newdonor ลงใน DataTable ของ healthdata
            DataColumn nameColumn = newDonorDataTable.Columns["name"]; // เก็บข้อมูลคอลัมน์ "name" และ "surname" จาก newDonorDataTable
            DataColumn surnameColumn = newDonorDataTable.Columns["surname"];
            healthDataTable.Columns.Add("Name", nameColumn.DataType);
            healthDataTable.Columns.Add("Surname", surnameColumn.DataType);

            foreach (DataRow row in healthDataTable.Rows)
            {
                DataRow[] matchingRows = newDonorDataTable.Select("idcard = '" + row["idcard"] + "'"); //เมธอด Select ของ newDonorDataTable เพื่อค้นหาแถวที่มีค่า idcard ตรงกับค่า idcard ของแถวปัจจุบันใน healthDataTable

                if (matchingRows.Length > 0)
                {
                    row["Name"] = matchingRows[0]["name"]; //"Name" ของแถวปัจจุบันใน healthDataTable ให้มีค่าเท่ากับค่าของคอลัมน์ "name" ใน newDonorDataTable
                    row["Surname"] = matchingRows[0]["surname"];
                }
            }

            // กำหนด DataSource ของ dataGridView1 เป็น DataTable ของ healthdata
            dataGridView1.DataSource = healthDataTable;

            // กำหนดฟอนต์สำหรับ dataGridView1
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);


        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            string query;
            DataSet ds;

            if (txtID.Text != "")
            {
                query = "select hd.idcard, hd.id, hd.daymonthyear, nd.name, nd.surname from healthdata hd inner join tb_newdonor nd on hd.idcard = nd.idcard where hd.idcard Like '" + txtID.Text + "%'";
            } //txtID.Text ไม่ใช่ค่าว่าง จะสร้างคำสั่ง SQL ที่ใช้ LIKE เพื่อค้นหาข้อมูลจากตาราง healthdata (hd) และ tb_newdonor (nd) โดยรวมแถวที่ idcard ใน healthdata ตรงกับค่าใน txtID.Text และเริ่มต้นด้วยข้อความใน txtID.Text
            else
            {
                query = "select hd.idcard, hd.id, hd.daymonthyear, nd.name, nd.surname from healthdata hd inner join tb_newdonor nd on hd.idcard = nd.idcard";
            } //หาก txtID.Text เป็นค่าว่าง (""), จะสร้างคำสั่ง SQL ที่ไม่มีเงื่อนไข WHERE เพื่อดึงข้อมูลทั้งหมดจากตาราง healthdata (hd) และ tb_newdonor (nd) ที่มีการจับคู่ idcard

            ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Agingealdata.pdf";

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
    }
}
