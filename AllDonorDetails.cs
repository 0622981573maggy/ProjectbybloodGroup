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
    public partial class AllDonorDetails : Form
    {
        function fn = new function();
        public AllDonorDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void AllDonorDetails_Load(object sender, EventArgs e)
        {
            String query = "select * from tb_newdonor";
            DataSet ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Regular);
            // ... ส่วนที่เหลือของโค้ดของคุณ


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm,new Rectangle(0,0,this.dataGridView1.Width,this.dataGridView1.Height));
            bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
            e.Graphics.DrawImage(bm, 0, 0);
            e.Graphics.DrawImage(bm,0,0);
            */

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "ALL Donor.pdf";

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
        public class HeaderFooter : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.TotalWidth = document.PageSize.Width;
                headerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                // สร้าง Chunk สำหรับชื่อหัวตาราง
                Chunk chunk = new Chunk("ALL Donor", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.BOLD));

                // กำหนดให้ Chunk อยู่ตรงกลาง
                Paragraph paragraph = new Paragraph(chunk);
                paragraph.Alignment = Element.ALIGN_CENTER;

                // เพิ่ม Chunk เข้าสู่เซลล์ของ PdfPTable
                PdfPCell headerCell = new PdfPCell(paragraph);
                headerCell.Border = PdfPCell.NO_BORDER;
                headerTable.AddCell(headerCell);

                // คำนวณตำแหน่ง x ที่จะให้ headerTable อยู่ตรงกลางของหน้ากระดาษ
                float xPosition = (document.PageSize.Width - headerTable.TotalWidth) / 2;

                // ขยับตำแหน่ง x ของ headerTable เพื่ออยู่ตรงกลางของหน้ากระดาษ และย้ายไปทางขวาอีก 1/10 ของความกว้างของหน้ากระดาษ
                xPosition += document.PageSize.Width / 2;

                // จัดวางตำแหน่งของหัวเอกสาร
                headerTable.WriteSelectedRows(0, -1, xPosition, document.Top, writer.DirectContent);
            }
        }
    }
}
