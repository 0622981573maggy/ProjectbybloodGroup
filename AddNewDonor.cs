using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using Guna.UI2.WinForms.Suite;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace BloodBank_Video
{
    public partial class AddNewDonor : Form
    {
        function fn = new function();
        
        public AddNewDonor()
        {
            InitializeComponent();
            textBox1.Visible = false;//เพศเพิ่มเติม
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AddNewDonor_Load(object sender, EventArgs e)
        {
            string query = "(SELECT max(id) FROM `tb_newdonor`)";
            DataSet ds = fn.getData(query);
            int count = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            labelNewID.Text = (count + 1).ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtSurname.Text != "" && txtID.Text != "" && txtWeight.Text != "" && txtHeight.Text != "" && txtDOB.Text != "" && txtPhon.Text != "" && txtage.Text != "" && txtGender.Text != "" && txtEmail.Text != "" && txtBloodGroup.Text != "" && txtOccutation.Text != "" && txtUnderlyingdisease.Text != "" && txtCity.Text != "" && txtAddress.Text != "") // ตรวจสอบว่ามีข้อมูลไหม
            {
                string dname = txtName.Text;
                string sname = txtSurname.Text;
                string idcard = txtID.Text;
                string idcardd = txtID.Text;
                Int64 weight = Int64.Parse(txtWeight.Text);
                Int64 height = Int64.Parse(txtHeight.Text);
                string dob = txtDOB.Text;
                string mobile = txtPhon.Text;
                Int64 age = Int64.Parse(txtage.Text);
                string gender = txtGender.Text;
                string email = txtEmail.Text;
                string bloodgroup = txtBloodGroup.SelectedItem.ToString();
                string occutation = txtOccutation.Text;
                string underlyingdisease = txtUnderlyingdisease.Text;
                string city = txtCity.Text;
                string address = txtAddress.Text;
                string bloodtype = txtblood.Text;


                string dataToSave = "";

                // ตรวจสอบว่าเลือก "อื่นๆ" หรือไม่
                if (txtGender.SelectedItem != null && txtGender.SelectedItem.ToString() == "อื่นๆ")
                {
                    // ถ้าเลือก "อื่นๆ" ให้บันทึกข้อมูลจาก TextBox
                    dataToSave = textBox1.Text;
                }
                else
                {
                    // ถ้าไม่ได้เลือก "อื่นๆ" ให้บันทึกข้อมูลจาก ComboBox
                    dataToSave = txtGender.SelectedItem.ToString();
                }

                // ทำต่อไปกับข้อมูลที่ต้องการบันทึก

                // ตรวจสอบน้ำหนักถ้ามีค่าน้อยกว่า 45
                if (weight < 45)
                {
                    MessageBox.Show("ห้ามใส่น้ำหนักต่ำกว่า 45", "น้ำหนัก !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // ลบตัวหลังสุดที่เพิ่มเข้ามา
                    txtWeight.Text = txtWeight.Text.Remove(txtWeight.Text.Length - 1);
                    // ตั้งตำแหน่งเคอร์เซอร์ที่ตำแหน่งสุดท้าย
                    txtWeight.Select(txtWeight.Text.Length, 0);
                }
                else
                {
                    byte[] imageBytes = null;

                    // ตรวจสอบว่ามีรูปภาพที่เลือกจาก PictureBox
                    if (pictureBox1.Image != null)
                    {
                        // แปลงรูปภาพให้อยู่ในรูปแบบของ byte array
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // บันทึกรูปภาพโดยระบุรูปแบบของรูปภาพ (encoder)
                            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            imageBytes = ms.ToArray();
                        }
                    }

                    // ตรวจสอบว่ามีข้อมูลรูปภาพที่ถูกแปลงเป็น byte array หรือไม่
                    if (imageBytes != null)
                    {
                        string genderValue = txtGender.SelectedItem.ToString();
                        if (genderValue == "อื่นๆ")
                        {
                            genderValue = textBox1.Text; // ใช้ข้อมูลจาก TextBox แทนคำว่า "อื่นๆ"
                        }

                        string query = "INSERT INTO tb_newdonor (name, surname, idcard, weight, height, daymonthyear, phon, age, gender, email, bloodgroup, occutation, underlyingdisease, city, address, bloodtype , image) VALUES ('" + dname + "','" + sname + "','" + idcard + "','" + weight + "','" + height + "','" + dob + "','" + mobile + "','" + age + "','" + genderValue + "','" + email + "','" + bloodgroup + "','" + occutation + "','" + underlyingdisease + "','" + city + "','" + address + "','" + bloodtype + "', @image)";

                        // เรียกใช้เมธอดที่มีการส่งพารามิเตอร์
                        fn.setDate(query, imageBytes);

                        string senderEmail = "maggy.gygy@gmail.com"; // ใส่ email ของคุณ
                        string appPassword = "zziu hkfe udrz ubxr"; // ใส่ App Password ของคุณ
                        string receiverEmail = txtEmail.Text;
                        string subject = "ขอบคุณจากใจจริงครับ ที่มาบริจาคโลหิตครั้งที่ 1 การให้ครั้งนี้ช่วยต่อชีวิตเพื่อนมนุษย์ได้มากมาย";

                        // สร้างเนื้อหาอีเมลด้วยข้อมูลเพิ่มเติม
                        string body = $"พวกเราขอใช้โอกาสนี้ กล่าวคำขอบคุณ คุณ {dname} {sname} กลุ่มเลือด {bloodgroup} อย่างสุดซึ้งจากใจจริง แด่คุณผู้มีน้ำใจอันประเสริฐ ที่มาบริจาคโลหิตอันล้ำค่า การให้ครั้งนี้ เปรียบเสมือนของขวัญที่ยิ่งใหญ่ ช่วยต่อลมหายใจ มอบโอกาส และสร้างรอยยิ้มให้กับผู้ป่วยที่กำลังต่อสู้กับโรคร้าย โลหิตของทุกหยด เปี่ยมไปด้วยพลังแห่งชีวิต ช่วยให้พวกเขาผ่านพ้นช่วงเวลาที่ยากลำบาก";

                        // สร้างอ็อบเจ็กต์ MailMessage
                        MailMessage mail = new MailMessage(senderEmail, receiverEmail, subject, body);

                        // กำหนด SMTP server ของ Gmail และพอร์ต
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // smtp.gmail.com: SMTP server ของ Gmail // 587: พอร์ตที่ใช้สำหรับ SMTP server ของ Gmail

                        // กำหนดข้อมูลการเข้าสู่ระบบ SMTP
                        client.Credentials = new NetworkCredential(senderEmail, appPassword);
                        client.EnableSsl = true; //EnableSsl: เปิดใช้งานการเข้ารหัส SSL

                        try
                        {
                            // ส่งอีเมล์
                            client.Send(mail);
                            Console.WriteLine("Email sent successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to send email: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
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
            txtblood.ResetText();

        }

        private void txtBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าช่องข้อมูลไม่ว่าง
            if (!string.IsNullOrEmpty(txtWeight.Text))
            {
                bool isNumeric = true;

                // วนลูปตรวจสอบทุกตัวอักษรในช่องข้อมูล
                foreach (char c in txtWeight.Text)
                {
                    // ถ้าไม่ใช่ตัวเลข
                    if (!char.IsDigit(c))
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวเลข
                        MessageBox.Show("กรุณาใส่เฉพาะตัวเลข", "น้ำหนัก !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isNumeric = false;
                        // ลบตัวอักษรที่ไม่ใช่ตัวเลขออกจากช่องข้อมูล
                        txtWeight.Text = txtWeight.Text.Remove(txtWeight.Text.IndexOf(c), 1);
                        // ตั้งตำแหน่งของเคอร์เซอร์เป็นตำแหน่งท้ายสุด
                        txtWeight.Select(txtWeight.Text.Length, 0);
                        // จบการทำงานของเมธอด
                        return;
                    }
                }

                // ตรวจสอบว่าเป็นตัวเลขหรือไม่
                if (isNumeric)
                {
                    // ตรวจสอบว่าค่าน้ำหนักไม่เกิน 150 
                    int weight = int.Parse(txtWeight.Text);
                    if (weight > 150)
                    {
                        MessageBox.Show("ห้ามใส่น้ำหนักเกิน 150", "น้ำหนัก !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // ลบตัวหลังสุดที่เพิ่มเข้ามา
                        txtWeight.Text = txtWeight.Text.Remove(txtWeight.Text.Length - 1);
                        // ตั้งตำแหน่งเคอร์เซอร์ที่ตำแหน่งสุดท้าย
                        txtWeight.Select(txtWeight.Text.Length, 0);
                    }
                 
                }
            }


        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่าช่องข้อมูลไม่ว่าง
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
                // ตรวจสอบว่าค่าที่ป้อนเข้ามาไม่เกิน 200
                if (int.Parse(txtHeight.Text) > 200)
                {
                    MessageBox.Show("กรุณาใส่ค่าสูงสุดไม่เกิน 200", "ส่วนสูง !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHeight.Text = "200"; // กำหนดค่าสูงสุดเป็น 200
                    txtHeight.Select(txtHeight.Text.Length, 0); // ตั้งเคอร์เซอร์ที่ตำแหน่งสุดท้าย
                    return;
                }




            }
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
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '์' && c != '่' && c != '้' && c != '๊' && c != '๋' && c !='ี' && c != 'ื' && c != 'ั' && c != 'ุ' && c != 'ู' && c != 'ิ' && c != '็')
                    { // (!char.IsLetter(c) ตรวจสอบว่า c เป็ยตัวอักษรไหม // char.IsWhiteSpace(c): ตรวจสอบว่า c เป็นช่องว่าง

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
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '์' && c != '่' && c != '้' && c != '๊' && c != '๋' && c != 'ี' && c != 'ื' && c != 'ั' && c != 'ุ' && c != 'ู' && c != 'ิ' && c != '็')
                    {
                        // แสดง MessageBox เตือนให้ใส่เฉพาะตัวอักษรหรือสระหรือวรรณยุกต์เท่านั้น
                        MessageBox.Show("กรุณาใส่ตัวอักษรเท่านั้น", "นามสกุลฟ!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan age = DateTime.Today - txtDOB.Value; // ดึงวันที่ปัจจุบัน ดึงวันที่เกิดจาก TextBox
            //คำนวณระยะเวลา(age) between DateTime.Today and txtDOB.Value
            int years = (int)(age.TotalDays / 365.25);

            //int years: แปลง age.TotalDays เป็นจำนวนเต็ม (years)

            // ตรวจสอบว่าอายุน้อยกว่า 17 ปีหรือไม่
            if (years < 17)
            {
                // แสดงข้อความแจ้งเตือน
                MessageBox.Show("อายุต้องไม่น้อยกว่า 17 ปี", "ขออภัย!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ตั้งค่าวันที่ให้เป็นวันที่ปัจจุบัน
                txtDOB.Value = DateTime.Today;
            }
            else
            {
                // แสดงอายุใน TextBox ที่กำหนดไว้
                txtage.Text = years.ToString();
            }

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

        private void txtage_TextChanged(object sender, EventArgs e)
        {
            // สร้างตัวแปรเพื่อเก็บค่าที่ผู้ใช้ป้อน
            string input = txtage.Text;

            // ตรวจสอบว่าข้อมูลที่ผู้ใช้ป้อนเป็นตัวเลขหรือไม่
            if (!int.TryParse(input, out int age))
            {
                // หากไม่ใช่ตัวเลข แสดงข้อความแจ้งเตือน
                MessageBox.Show("โปรดป้อนตัวเลขเท่านั้น", "มีข้อผิดพลาด !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // ล้างช่องข้อมูล
                txtage.Text = "";
            }
            else
            {
                if (age > 70)
                {
                    MessageBox.Show("อายุมากกว่า 70 ปี ไม่สามารถบริจาคเลือดได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtage.Text = "";
                }
            }

        }

        private void txtGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;

            if (txtGender.SelectedItem.ToString() == "อื่นๆ")
            {
                textBox1.Visible = true; // แสดง ComboBox1 เมื่อเลือก "อื่นๆ"
                textBox1.Focus(); // ให้ ComboBox1 รับ focus เพื่อให้ผู้ใช้สามารถเลือกได้ทันที
            }
            else
            {
                textBox1.Visible = false; // ซ่อน ComboBox1 หากไม่ได้เลือก "อื่นๆ"
                textBox1.Text = ""; // ล้างข้อความที่อาจจะมีอยู่ใน ComboBox1
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            Bitmap resizedImage = new Bitmap(width, height); //สร้างอ็อบเจ็กต์ Bitmap ใหม่โดยกำหนดขนาดความกว้างและความสูงตามที่ระบุในพารามิเตอร์ width และ height
            using (Graphics graphics = Graphics.FromImage(resizedImage)) // ใช้คำสั่ง using เพื่อให้ระบบปิดอ็อบเจ็กต์ Graphics อัตโนมัติเมื่อสิ้นสุดขอบเขตของการใช้งาน
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//กำหนดโหมดการปรับปรุงภาพ (Interpolation Mode) เป็น HighQualityBicubic เพื่อให้การปรับขนาดภาพมีคุณภาพสูง
                graphics.DrawImage(image, 0, 0, width, height); //  วาดภาพลงบนอ็อบเจ็กต์ Graphics โดยใช้เมท็อด DrawImage เพื่อวาดภาพที่รับเข้ามาในพารามิเตอร์ image ลงบนพื้นผิวของอ็อบเจ็กต์ Graphics ที่มีพิกัดเริ่มต้นที่ (0, 0) และกำหนดความกว้างและความสูงตามที่ระบุ
            }
            return resizedImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) //ตรวจสอบว่ามีรูปภาพอยู่ไหม
            {
                // แปลงรูปภาพให้เป็น Byte array
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }

                // เก็บรูปลงในฐานข้อมูล
                // ในที่นี้คุณต้องเขียนโค้ดเพื่อบันทึกข้อมูลรูปภาพลงในฐานข้อมูลของคุณ
                // โดยใช้ค่าของ imageBytes
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรูปภาพก่อนที่จะบันทึก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Healthinformation hf = new Healthinformation();
            hf.Show();
        }

        private void txtblood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
