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
    public partial class UpdateDonorDetails : Form
    {
        function fn = new function();
        public UpdateDonorDetails()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // สร้างตัวแปรเพื่อเก็บค่าที่ผู้ใช้ป้อน
            string input = txtage.Text;

            // ตรวจสอบว่าข้อมูลที่ผู้ใช้ป้อนเป็นตัวเลขหรือไม่
            if (!int.TryParse(input, out int age))
            {
                // หากไม่ใช่ตัวเลข แสดงข้อความแจ้งเตือน
                MessageBox.Show("โปรดป้อนตัวเลขเท่านั้น", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // ล้างช่องข้อมูล
                txtage.Text = "";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query;
            string idInput = txtDonorID.Text;

            // ตรวจสอบว่าข้อมูลที่ป้อนเข้ามาเป็นเลขบัตรประจำตัวประชาชนหรือไม่
            bool isIdCard = idInput.Length == 13 && idInput.All(char.IsDigit);

            if (isIdCard)
            {
                // สร้างคำสั่ง SQL เพื่อเลือกข้อมูลของผู้บริจาคโดยใช้เลขบัตรประจำตัวประชาชน
                query = "SELECT * FROM tb_newdonor WHERE idcard = '" + idInput + "'";
            }
            else
            {
                // สร้างคำสั่ง SQL เพื่อเลือกข้อมูลของผู้บริจาคโดยใช้ ID
                long id;
                if (!long.TryParse(idInput, out id))
                {
                    MessageBox.Show("รูปแบบของข้อมูลไม่ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                query = "SELECT * FROM tb_newdonor WHERE id = " + id;
            }

            DataSet ds = fn.getData(query);

            // ตรวจสอบว่ามีข้อมูลในชุดข้อมูลหรือไม่
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // กำหนดค่าให้กับ TextBox ตามข้อมูลที่ได้จากฐานข้อมูล
                DataRow row = ds.Tables[0].Rows[0];
                txtName.Text = row["name"].ToString();
                txtSurname.Text = row["surname"].ToString();
                txtID.Text = row["idcard"].ToString();
                txtWeight.Text = row["weight"].ToString();
                txtHeight.Text = row["height"].ToString();
                txtDOB.Text = row["daymonthyear"].ToString();
                txtPhon.Text = row["phon"].ToString();
                txtage.Text = row["age"].ToString();
                txtGender.Text = row["gender"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtBloodGroup.Text = row["bloodgroup"].ToString();
                txtOccutation.Text = row["occutation"].ToString();
                txtUnderlyingdisease.Text = row["underlyingdisease"].ToString();
                txtCity.Text = row["city"].ToString();
                txtAddress.Text = row["address"].ToString();

                // ดึงข้อมูลรูปภาพ
                if (row["image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["image"];

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
                MessageBox.Show("ไม่พบข้อมูล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label1.Visible = false;


        }

        private void txtDonorID_TextChanged(object sender, EventArgs e)
        {
            if(txtDonorID.Text == "")
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
                pictureBox1.ResetText();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDonorID.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ตรวจสอบค่าที่เลือกใน ComboBox ว่าเป็นค่าที่ถูกต้องหรือไม่
            if (txtBloodGroup.Items.Contains(txtBloodGroup.Text))
            {
                byte[] imageBytes = null;
                bool isImageUpdated = false; // Flag เพื่อระบุว่ารูปภาพมีการเปลี่ยนแปลงหรือไม่

                // ตรวจสอบว่ามีรูปภาพที่เลือกจาก PictureBox
                if (pictureBox1.Image != null)
                {
                    // ระบุ encoder ที่ถูกต้อง
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    // ระบุ encoder parameter เพื่อปรับคุณภาพรูปภาพ
                    EncoderParameters parameters = new EncoderParameters(1);
                    parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // ปรับคุณภาพรูปภาพให้เป็น 100%

                    try
                    {
                        // แปลงรูปภาพให้อยู่ในรูปแบบของ byte array ด้วย encoder ที่ระบุ
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBox1.Image.Save(ms, jpgEncoder, parameters);
                            imageBytes = ms.ToArray();
                        }

                        // ตั้งค่า isImageUpdated เป็น true เมื่อมีการเปลี่ยนแปลงรูปภาพ
                        isImageUpdated = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while saving the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // สร้างคำสั่ง SQL เพื่ออัปเดตข้อมูล
                string query = "UPDATE tb_newdonor SET name = '" + txtName.Text + "', surname = '" + txtSurname.Text + "', idcard = '" + txtID.Text + "', weight = '" + txtWeight.Text + "', height = '" + txtHeight.Text + "', daymonthyear = '" + txtDOB.Text + "', phon = '" + txtPhon.Text + "', age = '" + txtage.Text + "', gender = '" + txtGender.Text + "', email = '" + txtEmail.Text + "', bloodgroup = '" + txtBloodGroup.Text + "', occutation = '" + txtOccutation.Text + "', underlyingdisease = '" + txtUnderlyingdisease.Text + "', city = '" + txtCity.Text + "', address = '" + txtAddress.Text + "'";

                // เพิ่มการอัปเดตรูปภาพใน query เมื่อมีการเปลี่ยนแปลงรูปภาพ
                if (isImageUpdated)
                {
                    query += ", image = @image";
                }

                query += " WHERE id = " + txtDonorID.Text;

                try
                {
                    // เรียกใช้เมธอดที่มีการส่งพารามิเตอร์
                    fn.setDate(query, isImageUpdated ? imageBytes : null);
                    // ทำการอัปเดตข้อมูลโดยให้รีโหลดหน้าต่าง UpdateDonorDetails
                    UpdateDonorDetails_Load(this, null);
                    // เคลียร์รูปภาพที่แสดงใน pictureBox1
                    pictureBox1.Image = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating the donor details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือก Blood Group ที่ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private void UpdateDonorDetails_Load(object sender, EventArgs e)
        {
            txtDonorID.Clear();
        }

        private void txtBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าช่องข้อมูลไม่ว่าง
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                // วนลูปตรวจสอบทุกตัวอักษรในช่องข้อมูล
                foreach (char c in txtName.Text)
                {
                    // ถ้าไม่ใช่ตัวอักษรและไม่ใช่สระและไม่ใช่วรรณยุกต์และไม่ใช่วรรยุกต์ ('่', '้', '๊', '๋')
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '์' && c != '่' && c != '้' && c != '๊' && c != '๋' && c != 'ี' && c != 'ื' && c != 'ั' && c != 'ุ' && c != 'ู')
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวอักษรหรือสระหรือวรรณยุกต์เท่านั้น
                        MessageBox.Show("กรุณาใส่ตัวอักษรเท่านั้น", "ชื่อ!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // ลบตัวอักษรที่ไม่ใช่ตัวอักษรหรือสระหรือวรรณยุกต์ออกจากช่องข้อมูล
                        txtName.Text = txtName.Text.Remove(txtName.Text.IndexOf(c), 1);
                        // ตั้งตำแหน่งของเคอร์เซอร์เป็นตำแหน่งท้ายสุด
                        txtName.Select(txtName.Text.Length, 0);
                        // จบการทำงานของเมธอด
                        return;
                    }
                }
            }
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าช่องข้อมูลไม่ว่าง
            if (!string.IsNullOrEmpty(txtSurname.Text))
            {
                // วนลูปตรวจสอบทุกตัวอักษรในช่องข้อมูล
                foreach (char c in txtSurname.Text)
                {
                    // ถ้าไม่ใช่ตัวอักษรและไม่ใช่สระและไม่ใช่วรรณยุกต์และไม่ใช่วรรยุกต์ ('่', '้', '๊', '๋')
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '์' && c != '่' && c != '้' && c != '๊' && c != '๋' && c != 'ี' && c != 'ื' && c != 'ั' && c != 'ุ' && c != 'ู')
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวอักษรหรือสระหรือวรรณยุกต์เท่านั้น
                        MessageBox.Show("กรุณาใส่ตัวอักษรเท่านั้น", "ชื่อ!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // ลบตัวอักษรที่ไม่ใช่ตัวอักษรหรือสระหรือวรรณยุกต์ออกจากช่องข้อมูล
                        txtSurname.Text = txtSurname.Text.Remove(txtSurname.Text.IndexOf(c), 1);
                        // ตั้งตำแหน่งของเคอร์เซอร์เป็นตำแหน่งท้ายสุด
                        txtSurname.Select(txtSurname.Text.Length, 0);
                        // จบการทำงานของเมธอด
                        return;
                    }
                }
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่า textbox ว่างหรือไม่
            if (string.IsNullOrEmpty(txtID.Text))
            {
                return;
            }

            // ตรวจสอบว่า textbox มีตัวอักษร 13 ตัวหรือไม่
            if (txtID.Text.Length > 13)
            {
                // แสดงข้อความแจ้งเตือน
                MessageBox.Show("กรุณาใส่ตัวเลข 13 หลักเท่านั้น");

                // ลบตัวอักษรเกินออก
                txtID.Text = txtID.Text.Substring(0, 13);
                return;
            }

            // ตรวจสอบว่า textbox มีตัวเลขเท่านั้น
            foreach (char c in txtID.Text)
            {
                if (!char.IsDigit(c))
                {
                    // แสดงข้อความแจ้งเตือน
                    MessageBox.Show("กรุณาใส่ตัวเลขเท่านั้น");

                    // ลบข้อความใน textbox
                    txtID.Text = "";
                    return;
                }
            }

            // ข้อความใน textbox ถูกต้อง
            // ...
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            /// ตรวจสอบว่าช่องข้อมูลไม่ว่าง
            if (!string.IsNullOrEmpty(txtWeight.Text))
            {
                // วนลูปตรวจสอบทุกตัวอักษรในช่องข้อมูล
                foreach (char c in txtWeight.Text)
                {
                    // ถ้าไม่ใช่ตัวเลข
                    if (!char.IsDigit(c))
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวเลข
                        MessageBox.Show("กรุณาใส่ตัวเลขเท่านั้น", "น้ำหนัก !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจากช่องข้อมูล
                        txtWeight.Text = txtWeight.Text.Remove(txtWeight.Text.IndexOf(c), 1);
                        // ตั้งตำแหน่งของเคอร์เซอร์เป็นตำแหน่งท้ายสุด
                        txtWeight.Select(txtWeight.Text.Length, 0);
                        // จบการทำงานของเมธอด
                        return;
                    }
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            /// ตรวจสอบว่าช่องข้อมูลไม่ว่าง
            if (!string.IsNullOrEmpty(txtHeight.Text))
            {
                // วนลูปตรวจสอบทุกตัวอักษรในช่องข้อมูล
                foreach (char c in txtHeight.Text)
                {
                    // ถ้าไม่ใช่ตัวเลข
                    if (!char.IsDigit(c))
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวเลข
                        MessageBox.Show("กรุณาใส่ตัวเลขเท่านั้น", "ส่วนสูง !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจากช่องข้อมูล
                        txtHeight.Text = txtHeight.Text.Remove(txtHeight.Text.IndexOf(c), 1);
                        // ตั้งตำแหน่งของเคอร์เซอร์เป็นตำแหน่งท้ายสุด
                        txtHeight.Select(txtHeight.Text.Length, 0);
                        // จบการทำงานของเมธอด
                        return;
                    }
                }
            }
        }

        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPhon_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่า textbox ว่างหรือไม่
            if (string.IsNullOrEmpty(txtPhon.Text))
            {
                return;
            }

            // ตรวจสอบว่า textbox มีตัวอักษร 10 ตัวหรือไม่
            if (txtPhon.Text.Length > 10)
            {
                // แสดงข้อความแจ้งเตือน
                MessageBox.Show("กรุณาใส่ตัวเลข 10 หลักเท่านั้น ", "เบอร์โทรศัพท์!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ลบตัวอักษรเกินออก
                txtPhon.Text = txtPhon.Text.Substring(0, 10);
                return;
            }

            // ตรวจสอบว่า textbox มีตัวเลขเท่านั้น
            foreach (char c in txtPhon.Text)
            {
                if (!char.IsDigit(c))
                {
                    // แสดงข้อความแจ้งเตือน
                    MessageBox.Show("กรุณาใส่ตัวเลขเท่านั้น ", "เบอร์โทรศัพท์!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // ลบข้อความใน textbox
                    txtPhon.Text = "";
                    return;
                }
            }

            // ข้อความใน textbox ถูกต้อง
            // ...
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
            label1.Visible = false; // หรือ label1.Text = "";

        }
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
    }
    
}
