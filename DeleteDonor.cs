using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank_Video
{
    public partial class DeleteDonor : Form
    {
        function fn = new function();
        String query;
        public DeleteDonor()
        {
            InitializeComponent();
        }

        private void DeleteDonor_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            Int64 id = Int64.Parse(txtDonorID.Text);
            // สร้างคำสั่ง SQL เพื่อเลือกข้อมูลของผู้บริจาคโดยใช้ ID
            string query = "select * from tb_newdonor where id = " + id;
            DataSet ds = fn.getData(query);

            // ตรวจสอบว่ามีข้อมูลในชุดข้อมูลหรือไม่
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // กำหนดค่าให้กับ TextBox ตามข้อมูลที่ได้จากฐานข้อมูล
                txtName.Text = ds.Tables[0].Rows[0][1].ToString(); // เลข 0 หมายถึงเรียกใช้แถวแรก [1] คอลัมน์ที่สองของแถวนั้น ๆ
                txtSurname.Text = ds.Tables[0].Rows[0][2].ToString();
                txtID.Text = ds.Tables[0].Rows[0][3].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0][4].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0][5].ToString();
                txtDOB.Text = ds.Tables[0].Rows[0][6].ToString();
                txtPhon.Text = ds.Tables[0].Rows[0][7].ToString();
                txtage.Text = ds.Tables[0].Rows[0][8].ToString();
                txtGender.Text = ds.Tables[0].Rows[0][9].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][10].ToString();
                txtBloodGroup.Text = ds.Tables[0].Rows[0][11].ToString();
                txtOccutation.Text = ds.Tables[0].Rows[0][12].ToString();
                txtUnderlyingdisease.Text = ds.Tables[0].Rows[0][13].ToString();
                txtCity.Text = ds.Tables[0].Rows[0][14].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0][15].ToString();

                // ดึงข้อมูลรูปภาพ
                if (ds.Tables[0].Rows[0]["image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])ds.Tables[0].Rows[0]["image"];

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        // แปลง byte array เป็น Image
                        Image img = Image.FromStream(ms);
                        // แสดงรูปภาพใน PictureBox
                        pictureBox1.Image = img;
                    }
                }
                else
                {
                    // หากรูปภาพเป็นค่าว่าง กำหนดรูปภาพใน PictureBox เป็น null
                    pictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show("ไม่พบ Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจว่าจะลบข้อมูล ?", "Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK) ;
            {
                query = "delete from  tb_newdonor where id = " + txtDonorID.Text + "";
                fn.setDate(query);
            }
        }

        private void txtDonorID_TextChanged(object sender, EventArgs e)
        {
            if (txtDonorID.Text == "")
            {
                txtName.Clear();
                txtSurname.Clear();
                txtID.Clear();
                txtWeight.Clear();
                txtHeight.Clear();
                txtDOB.ResetText();
                txtPhon.Clear();
                txtage.Clear();
                txtGender.ResetText();
                txtEmail.Clear();
                txtBloodGroup.ResetText();
                txtOccutation.Clear();
                txtUnderlyingdisease.Clear();
                txtCity.Clear();
                txtAddress.Clear();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDonorID.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // อ่านไฟล์รูปภาพจากตำแหน่งที่เลือก
                Image originalImage = Image.FromFile(openFileDialog1.FileName);

                // ปรับขนาดของรูปภาพเพื่อให้เข้ากับขนาดของ PictureBox
                Image resizedImage = ResizeImage(originalImage, pictureBox1.Width, pictureBox1.Height);

                // กำหนดรูปภาพให้กับ PictureBox
                pictureBox1.Image = resizedImage;
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); // เพื่อดึงข้อมูลเกี่ยวกับ image encoders ทั้งหมดที่มีอยู่ในระบบ
            foreach (ImageCodecInfo codec in codecs) //ใช้การวนลูป foreach เพื่อตรวจสอบแต่ละ ImageCodecInfo ในอาร์เรย์ codecs ซึ่งแต่ละ ImageCodecInfo ถูกเก็บไว้ในตัวแปร codec ทีละตัว
            {
                if (codec.FormatID == format.Guid) //ตรวจสอบว่า FormatID ของ codec ตรงกับ Guid ของ format หรือไม่ 
                {
                    return codec;
                }
            }
            return null;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height); // สร้างออบเจ็กต์ Bitmap ใหม่ที่มีขนาดตามที่ระบุโดย width และ height
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // ซึ่งเป็นโหมดการประมวลผลภาพที่มีคุณภาพสูงและใช้สำหรับการปรับขนาดภาพให้มีความละเอียดและคุณภาพดีที่สุด
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
    }
}
