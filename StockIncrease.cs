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
    public partial class StockIncrease : Form
    {
        function fn = new function();
        String query;
        public StockIncrease()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            
            query = "update stock set quantity=quantity+ " +txtUnits.Text+ " where bloodgroup = '" + txtBloodGroup.Text + "'";
            fn.setDate(query);
            StockIncrease_Load(this, null);
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);

        }

        private void StockIncrease_Load(object sender, EventArgs e)
        {
            query = "select * from stock";
            DataSet ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Result.pdf";

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
