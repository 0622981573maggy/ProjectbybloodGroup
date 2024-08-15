using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank_Video
{
    public partial class Againhealtdata : Form
    {
        function fn = new function();
        public Againhealtdata()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Againhealtdata_Load(object sender, EventArgs e)
        {
           
        }

        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {
            // ดึงวันที่ปัจจุบัน
            DateTime currentDate = DateTime.Today;

            // ตรวจสอบว่าวันที่ที่ถูกเลือกเป็นวันปัจจุบันหรือไม่
            if (txtDOB.Value.Date != currentDate)
            {
                // ถ้าไม่ใช่ ให้กำหนดค่าวันที่ให้เป็นวันปัจจุบัน
                txtDOB.Value = currentDate;
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
        }

        private void t1_CheckedChanged(object sender, EventArgs e)
        {
            // ถ้า t1 ถูกเลือก ให้ตรวจสอบว่า f1 ถูกเลือกหรือไม่
            if (t1.Checked && f1.Checked)
            {
                // ถ้า f1 ก็ถูกเลือกให้ปิด f1 และแสดงข้อความแจ้งเตือน
                f1.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void f1_CheckedChanged(object sender, EventArgs e)
        {
            // ถ้า f1 ถูกเลือก ให้ตรวจสอบว่า t1 ถูกเลือกหรือไม่
            if (f1.Checked && t1.Checked)
            {
                // ถ้า t1 ก็ถูกเลือกให้ปิด t1 และแสดงข้อความแจ้งเตือน
                t1.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (f1.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultf1 = MessageBox.Show("ท่านแน่ใจว่า ท่านรู้สึกไม่ค่อยสบาย สุขภาพแข็งแรงไม่แข็งแรง ไม่พร้อมที่จะบริจาคโลหิต", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultf1 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของท่านไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก f1
                    f1.CheckedChanged -= f1_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    f1.Checked = false;
                  
                }
            }
        }

        private void t2_CheckedChanged(object sender, EventArgs e)
        {
            if (t2.Checked && f2.Checked)
            {
                f2.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void f2_CheckedChanged(object sender, EventArgs e)
        {
            if (f2.Checked && t2.Checked)
            {
                t2.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (f2.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultf2 = MessageBox.Show("ท่านแน่ใจว่า ท่านนอนหลับพักผ่อน (น้อยกว่า 5 ชั่วโมง)", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultf2 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก f2
                    f2.CheckedChanged -= f2_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    f2.Checked = false;
                   
                }
            }
        }

        private void t3_CheckedChanged(object sender, EventArgs e)
        {
            if (t3.Checked && f3.Checked)
            {
                f3.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t3.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultf3 = MessageBox.Show("ท่านแน่ใจว่า ท่านรับประทานอาหารที่มีไขมันสูง ภายใน 6 ชั่วโมงที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultf3 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t3
                    t3.CheckedChanged -= t3_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t3.Checked = false;

                }
            }
        }

        private void f3_CheckedChanged(object sender, EventArgs e)
        {
            if (f3.Checked && t3.Checked)
            {
                t3.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void t4_CheckedChanged(object sender, EventArgs e)
        {
            if (t4.Checked && f4.Checked)
            {
                f4.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t4.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt4 = MessageBox.Show("ท่านแน่ใจว่า ท่านมีโรคประจำตัว หรือเคยเป็นโรคมาก่อน", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt4 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t4
                    t4.CheckedChanged -= t4_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t4.Checked = false;
                    
                }
            }
            


        }

        private void f4_CheckedChanged(object sender, EventArgs e)
        {
            if (f4.Checked && t4.Checked)
            {
                t4.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t5_CheckedChanged(object sender, EventArgs e)
        {
            if (t5.Checked && f5.Checked)
            {
                f5.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t5.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt5 = MessageBox.Show("ท่านแน่ใจว่าท่านรับประทานยาปฏิชีวนะ (ยาฆ่าเชื้อ) ภายใน 7 วันที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt5 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t5
                    t5.CheckedChanged -= t5_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t5.Checked = false;
                    
                }
            }
        }

        private void f5_CheckedChanged(object sender, EventArgs e)
        {
            if (f5.Checked && t5.Checked)
            {
                t5.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t6_CheckedChanged(object sender, EventArgs e)
        {
            if (t6.Checked && f6.Checked)
            {
                f6.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t6.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt6 = MessageBox.Show("ท่านแน่ใจว่าท่าน รับประทานยายาแอสไพริน ยาคลายกล้ามเนื้อ ยาแก้ปวดข้อ หรือยาอื่นๆ ในกลุ่ม เดียวกันภายใน 2 วันที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt6 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t6
                    t6.CheckedChanged -= t6_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t6.Checked = false;
                   
                }
            }
        }

        private void f6_CheckedChanged(object sender, EventArgs e)
        {
            if (f6.Checked && t6.Checked)
            {
                t6.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t7_CheckedChanged(object sender, EventArgs e)
        {
            if (t7.Checked && f7.Checked)
            {
                f7.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t7.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt7 = MessageBox.Show("ท่านแน่ใจว่าท่าน มีการใช้ ยา / สมุนไพร / อาหารเสริมที่มีไบโอตินเป็นส่วนประกอบ เป็นประจำ", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt7 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t6
                    t7.CheckedChanged -= t7_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t7.Checked = false;

                }
            }
        }

        private void f7_CheckedChanged(object sender, EventArgs e)
        {
            if (f7.Checked && t7.Checked)
            {
                f7.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t8_CheckedChanged(object sender, EventArgs e)
        {
            if (t8.Checked && f8.Checked)
            {
                f8.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t8.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt8 = MessageBox.Show("ท่านแน่ใจว่าท่าน ดื่มแอลกอฮอล์ภายใน 24 ชั่วโมงที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt8 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t8
                    t8.CheckedChanged -= t8_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t8.Checked = false;

                }
            }
        }

        private void f8_CheckedChanged(object sender, EventArgs e)
        {
            if (f8.Checked && t8.Checked)
            {
                f8.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void t9_CheckedChanged(object sender, EventArgs e)
        {
            if (t9.Checked && f9.Checked)
            {
                f9.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t9.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt9 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยบริจาคเซลล์ต้นกำเนิดเม็ดโลหิตในระยะ 6 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt9 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                { 
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t9
                    t9.CheckedChanged -= t9_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t9.Checked = false;

                }
            }
        }

        private void f9_CheckedChanged(object sender, EventArgs e)
        {
            if (f9.Checked && t9.Checked)
            {
                f9.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t10_CheckedChanged(object sender, EventArgs e)
        {
            if (t10.Checked && f10.Checked)
            {
                f10.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t10.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt10 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยตั้งครรภ์ หรือแท้งบุตร มาก่อน", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt10 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t10
                    t10.CheckedChanged -= t10_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t10.Checked = false;

                }
            }
        }

        private void f10_CheckedChanged(object sender, EventArgs e)
        {
            if (f10.Checked && t10.Checked)
            {
                f10.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t11_CheckedChanged(object sender, EventArgs e)
        {
            if (t11.Checked && f11.Checked)
            {
                f11.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t11.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt11 = MessageBox.Show("ท่านแน่ใจว่าท่าน อยู่ในช่วงตั้งครรภ์ หรือให้นมบุตร", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt11 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t11
                    t11.CheckedChanged -= t11_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t11.Checked = false;

                }
            }
        }

        private void f11_CheckedChanged(object sender, EventArgs e)
        {
            if (f11.Checked && t11.Checked)
            {
                t11.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t12_CheckedChanged(object sender, EventArgs e)
        {
            if (t12.Checked && f12.Checked)
            {
                f12.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t12.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt12 = MessageBox.Show("ท่านแน่ใจว่าท่าน คลอดบุตร หรือแท้งบุตร ภายใน 6 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt12 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t12
                    t12.CheckedChanged -= t12_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t12.Checked = false;
                }
            }
        }

        private void f12_CheckedChanged(object sender, EventArgs e)
        {
            if (f12.Checked && t12.Checked)
            {
                t12.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t13_CheckedChanged(object sender, EventArgs e)
        {
            if (t13.Checked && f13.Checked)
            {
                f13.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t13.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt13 = MessageBox.Show("ท่านแน่ใจว่าท่าน หรือคู่ของท่านเคยมีเพศสัมพันธ์กับ : ผู้ที่ไม่ใช่คู่ของตนเอง / ผู้ที่ทำงานบริการทางเพศ ผู้เสพยาเสพติด ผู้ที่อาจติดเชื้อเอชไอวีหรือโรคติดต่อทางเพศสัมพันธ์อื่น", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt13 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t13
                    t13.CheckedChanged -= t13_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t13.Checked = false;
                }
            }
        }

        private void f13_CheckedChanged(object sender, EventArgs e)
        {
            if (f13.Checked && t13.Checked)
            {
                t13.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t14_CheckedChanged(object sender, EventArgs e)
        {
            if (t14.Checked && f14.Checked)
            {
                f14.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t14.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt14 = MessageBox.Show("ท่านแน่ใจว่าท่าน หรือคู่ของท่านเคยมีเพศสัมพันธ์กับ : ผู้ที่ไม่ใช่คู่ของตนเอง / ผู้ที่ทำงานบริการทางเพศ ผู้เสพยาเสพติด ผู้ที่อาจติดเชื้อเอชไอวีหรือโรคติดต่อทางเพศสัมพันธ์อื่น", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt14 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t14
                    t14.CheckedChanged -= t14_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t14.Checked = false;
                }
            }
        }

        private void f14_CheckedChanged(object sender, EventArgs e)
        {
            if (f14.Checked && t14.Checked)
            {
                t14.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t15_CheckedChanged(object sender, EventArgs e)
        {
            if (t15.Checked && f15.Checked)
            {
                f15.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t15.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt15 = MessageBox.Show("ท่านแน่ใจว่าท่าน มีพฤติกรรมเพศสัมพันธ์แบบชายกับชาย", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt15 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t15
                    t15.CheckedChanged -= t15_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t15.Checked = false;
                }
            }
        }

        private void f15_CheckedChanged(object sender, EventArgs e)
        {
            if (f15.Checked && t15.Checked)
            {
                t15.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t16_CheckedChanged(object sender, EventArgs e)
        {
            if (t16.Checked && f16.Checked)
            {
                f16.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t16.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt16 = MessageBox.Show("ท่านแน่ใจว่าท่าน มีพฤติกรรมเพศสัมพันธ์แบบชายกับชาย", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt16 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t16
                    t16.CheckedChanged -= t16_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t16.Checked = false;
                }
            }
        }

        private void f16_CheckedChanged(object sender, EventArgs e)
        {
            if (f16.Checked && t16.Checked)
            {
                t16.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t17_CheckedChanged(object sender, EventArgs e)
        {
            if (t17.Checked && f17.Checked)
            {
                f17.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t17.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt17 = MessageBox.Show("ท่านแน่ใจว่าท่าน ท้องเสีย ท้องร่วง ภายใน 7 วันที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt17 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t17
                    t17.CheckedChanged -= t17_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t17.Checked = false;
                }
            }
        }

        private void f17_CheckedChanged(object sender, EventArgs e)
        {
            if (f17.Checked && t17.Checked)
            {
                t17.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t18_CheckedChanged(object sender, EventArgs e)
        {
            if (t18.Checked && f18.Checked)
            {
                f18.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t18.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt18 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยเจาะหู เจาะผิวหนัง สัก ลบรอยสัก ฝังเข็ม ภายใน 4 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt18 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t18
                    t18.CheckedChanged -= t18_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t18.Checked = false;
                }
            }
        }

        private void f18_CheckedChanged(object sender, EventArgs e)
        {
            if (f18.Checked && t18.Checked)
            {
                t18.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t19_CheckedChanged(object sender, EventArgs e)
        {
            if (t19.Checked && f19.Checked)
            {
                f19.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t19.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultt19 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยได้รับการผ่าตัดเล็ก ภายใน 7 วันที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultt19 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t19
                    t19.CheckedChanged -= t19_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t19.Checked = false;
                }
            }
        }

        private void f19_CheckedChanged(object sender, EventArgs e)
        {
            if (f19.Checked && t19.Checked)
            {
                t19.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t20_CheckedChanged(object sender, EventArgs e)
        {
            if (t20.Checked && f20.Checked)
            {
                f20.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t20.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result20 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยผ่าตัดใหญ่ ภายใน 6 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result20 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                { 
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t20
                    t20.CheckedChanged -= t20_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t20.Checked = false;
                }
            }
        }

        private void f20_CheckedChanged(object sender, EventArgs e)
        {
            if (f20.Checked && t20.Checked)
            {
                t20.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t21_CheckedChanged(object sender, EventArgs e)
        {
            if (t21.Checked && f21.Checked)
            {
                f21.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t21.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result21 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยป่วยและได้รับโลหิต / ส่วนประกอบโลหิต ภายใน 1 ปีที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result21 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t21
                    t21.CheckedChanged -= t21_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t21.Checked = false;
                }
            }
        }

        private void f21_CheckedChanged(object sender, EventArgs e)
        {
            if (f21.Checked && t21.Checked)
            {
                t21.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t22_CheckedChanged(object sender, EventArgs e)
        {
            if (t22.Checked && f22.Checked)
            {
                f22.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t22.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result22 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยได้รับการปลูกถ่ายอวัยวะ หรือเซลล์ต้นกำเนิดเม็ดโลหิต", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result22 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t22
                    t22.CheckedChanged -= t22_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t22.Checked = false;
                }
            }
        }

        private void f22_CheckedChanged(object sender, EventArgs e)
        {
            if (f22.Checked && t22.Checked)
            {
                t22.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t23_CheckedChanged(object sender, EventArgs e)
        {
            if (t23.Checked && f23.Checked)
            {
                f23.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t23.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result23 = MessageBox.Show("ท่านแน่ใจว่าท่าน ถูกเข็มที่เปื้อนเลือดตา ในระยะ 1 ปีที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result23 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t23
                    t23.CheckedChanged -= t23_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t23.Checked = false;
                }
            }
        }

        private void f23_CheckedChanged(object sender, EventArgs e)
        {
            if (f23.Checked && t23.Checked)
            {
                t23.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t24_CheckedChanged(object sender, EventArgs e)
        {
            if (t24.Checked && f24.Checked)
            {
                f24.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t24.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result24 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยป่วยเป็นโรคตับอักเสบ หลังอายุ 11 ปี", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result24 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t24
                    t24.CheckedChanged -= t24_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t24.Checked = false;
                }
            }
        }

        private void f24_CheckedChanged(object sender, EventArgs e)
        {
            if (f24.Checked && t24.Checked)
            {
                t24.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t25_CheckedChanged(object sender, EventArgs e)
        {
            if (t25.Checked && f25.Checked)
            {
                f25.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t25.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result25 = MessageBox.Show("ท่านแน่ใจว่า คู่ของท่านหรือบุคคลในครอบครัวของท่าน เป็นโรคตับอักเสบ ในระยะเวลา 1 ปี ที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result25 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t25
                    t25.CheckedChanged -= t25_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t25.Checked = false;
                }
            }
        }

        private void f25_CheckedChanged(object sender, EventArgs e)
        {
            if (f25.Checked && t25.Checked)
            {
                t25.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t26_CheckedChanged(object sender, EventArgs e)
        {
            if (t26.Checked && f26.Checked)
            {
                f26.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t26.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result26 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยตรวจพบว่าเป็นพาหะของโรคตับอักเสบ", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result26 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t26
                    t26.CheckedChanged -= t26_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t26.Checked = false;
                }
            }
        }

        private void f26_CheckedChanged(object sender, EventArgs e)
        {
            if (f26.Checked && t26.Checked)
            {
                t26.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t27_CheckedChanged(object sender, EventArgs e)
        {
            if (t27.Checked && f27.Checked)
            {
                f27.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t27.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result27 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยป่วยเป็นโรคมาลาเรีย ในระยะ 3 ปีที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result27 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t27
                    t27.CheckedChanged -= t27_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t27.Checked = false;
                }
            }
        }

        private void f27_CheckedChanged(object sender, EventArgs e)
        {
            if (f27.Checked && t27.Checked)
            {
                t27.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t28_CheckedChanged(object sender, EventArgs e)
        {
            if (t28.Checked && f28.Checked)
            {
                f28.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t28.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result28 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยเข้าไปในพื้นที่มีเชื้อมาลาเรียชุกชุม ในระยะ 1 ปีที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result28 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t28
                    t28.CheckedChanged -= t28_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t28.Checked = false;
                }
            }
        }

        private void f28_CheckedChanged(object sender, EventArgs e)
        {
            if (f28.Checked && t28.Checked)
            {
                t28.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t29_CheckedChanged(object sender, EventArgs e)
        {
            if (t29.Checked && f29.Checked)
            {
                f29.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t29.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result29 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยป่วยเป็นโรคไข้หวัดใหญ่ / โรคไข้เลือดออก / โรคชิคุนกุนยา โรคไข้ซิกา หรือ โรคโควิด-19 ในระยะ 1 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result29 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t29
                    t29.CheckedChanged -= t29_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t29.Checked = false;
                }
            }
        }

        private void f29_CheckedChanged(object sender, EventArgs e)
        {
            if (f29.Checked && t29.Checked)
            {
                t29.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t30_CheckedChanged(object sender, EventArgs e)
        {
            if (t30.Checked && f30.Checked)
            {
                f30.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t30.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result30 = MessageBox.Show("ท่านแน่ใจว่าท่าน ได้รับวัคซีนเพื่อป้องกันโรคในระยะ 2 เดือนที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result30 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t30
                    t30.CheckedChanged -= t30_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t30.Checked = false;
                }
            }
        }

        private void f30_CheckedChanged(object sender, EventArgs e)
        {
            if (f30.Checked && t30.Checked)
            {
                t30.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t31_CheckedChanged(object sender, EventArgs e)
        {
            if (t31.Checked && f31.Checked)
            {
                f31.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t31.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result31 = MessageBox.Show("ท่านแน่ใจว่าท่าน ได้รับเซรุ่ม ภายใน 1 ปี ที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result31 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t31
                    t31.CheckedChanged -= t31_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t31.Checked = false;
                }
            }
        }

        private void f31_CheckedChanged(object sender, EventArgs e)
        {
            if (f31.Checked && t31.Checked)
            {
                t31.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t32_CheckedChanged(object sender, EventArgs e)
        {
            if (t32.Checked && f32.Checked)
            {
                f33.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t32.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result32 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยมีประวัติเสพยาเสพติด", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result32 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t32
                    t32.CheckedChanged -= t32_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t32.Checked = false;
                }
            }
        }

        private void f32_CheckedChanged(object sender, EventArgs e)
        {
            if (f32.Checked && t32.Checked)
            {
                t32.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t33_CheckedChanged(object sender, EventArgs e)
        {
            if (t33.Checked && f33.Checked)
            {
                f33.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t33.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result33 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยถูกควบคุมตัวหรือจองจำในเรือนจ าติดต่อกันเกิน 72 ชั่วโมง ในช่วง 1 ปีที่ผ่านมา", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result33 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t33
                    t33.CheckedChanged -= t33_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t33.Checked = false;
                }
            }
        }

        private void f33_CheckedChanged(object sender, EventArgs e)
        {
            if (f33.Checked && t33.Checked)
            {
                t33.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t34_CheckedChanged(object sender, EventArgs e)
        {
            if (t34.Checked && f34.Checked)
            {
                f34.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t34.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result34 = MessageBox.Show("ท่านแน่ใจว่าท่าน เคยมีน้ำหนักลด มีไข้ มีต่อมน้ าเหลืองโตโดยไม่ทราบสาเหตุ ในระยะ 3 เดือนที่ผ่านมา หรือเคยตรวจพบว่าติดเชื้อเอชไอวี", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result34 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t34
                    t34.CheckedChanged -= t34_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t34.Checked = false;
                }
            }
        }

        private void f34_CheckedChanged(object sender, EventArgs e)
        {
            if (f34.Checked && t34.Checked)
            {
                t34.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t35_CheckedChanged(object sender, EventArgs e)
        {
            if (t35.Checked && f35.Checked)
            {
                f35.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t35.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result35 = MessageBox.Show("ท่านแน่ใจว่าช่วง พ.ศ. 2523 - 2539 ท่านเคยพำนักอาศัยอยู่ในประเทศเหล่านี้ เป็นเวลาสะสมมากกว่า 3 เดือน อังกฤษ ไอร์แลนด์เหนือ สก๊อตแลนด์ เวลส์", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result35 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t35
                    t35.CheckedChanged -= t35_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t35.Checked = false;
                }
            }
        }

        private void f35_CheckedChanged(object sender, EventArgs e)
        {
            if (f35.Checked && t35.Checked)
            {
                t35.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t36_CheckedChanged(object sender, EventArgs e)
        {
            if (t36.Checked && f36.Checked)
            {
                f36.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (t36.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult result36 = MessageBox.Show("ท่านแน่ใจว่าช่วง ช่วง พ.ศ. 2523 – 2544 ท่านเคยพำนักอาศัยอยู่ในประเทศฝรั่งเศส และไอร์แลนด์ เป็น ระยะเวลาสะสมมากกว่า 5 ", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (result36 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของคุณไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก t36
                    t36.CheckedChanged -= t36_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    t36.Checked = false;
                }
            }
        }

        private void f36_CheckedChanged(object sender, EventArgs e)
        {
            if (f36.Checked && t36.Checked)
            {
                t37.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void t37_CheckedChanged(object sender, EventArgs e)
        {
            if (t37.Checked && f37.Checked)
            {
                f37.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void f37_CheckedChanged(object sender, EventArgs e)
        {
            if (f37.Checked && t37.Checked)
            {
                t37.Checked = false;
                MessageBox.Show("สามรถเลือกได้เพียงแค่ 1 ตัวเลือก", "เตือนน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (f37.Checked)
            {
                // แสดงกล่องข้อความยืนยัน
                DialogResult resultf37 = MessageBox.Show("ท่านไม่มั่นใจว่าโลหิตของท่านมีความปลอดภัยต่อผู้ป่วย", "กรุณาตอบตามความจริงด้วยนะครับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ถ้าผู้ใช้กด OK
                if (resultf37 == DialogResult.Yes)
                {
                    MessageBox.Show("ท่านไม่สามารถบริจาคเลือดได้เพราะ เนื่องจากเงื่อนไขสุภาพของท่านไม่เหมาะสมที่จะบริจาคเลือด ขอความกรุณากลับไปพักผ่อนก่อนนะครับ แล้วกลับมาบริจาคเลือดได้ภายหลังนะครับ", "ทางศูนย์รับบริจาคเลือดต้องขออภัยเป็นอย่างสูง");
                    // ปิดหน้าจอ
                    this.Close();
                }
                else
                {
                    // ถ้าผู้ใช้กด Cancel ให้ยกเลิกการเลือก f37
                    f37.CheckedChanged -= f37_CheckedChanged; // ป้องกันการเรียก event handler ซ้ำ
                    f37.Checked = false;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string one = label3.Text;
            string dob = txtDOB.Text;
            string idcardd = txtID.Text;
            bool n1 = t1.Checked;
            bool n2 = t2.Checked;
            bool n3 = t3.Checked;
            bool n4 = t4.Checked;
            bool n5 = t5.Checked;
            bool n6 = t6.Checked;
            bool n7 = t7.Checked;
            bool n8 = t8.Checked;
            bool n9 = t9.Checked;
            bool n10 = t10.Checked;
            bool n11 = t11.Checked;
            bool n12 = t12.Checked;
            bool n13 = t13.Checked;
            bool n14 = t14.Checked;
            bool n15 = t15.Checked;
            bool n16 = t16.Checked;
            bool n17 = t17.Checked;
            bool n18 = t18.Checked;
            bool n19 = t19.Checked;
            bool n20 = t20.Checked;
            bool n21 = t21.Checked;
            bool n22 = t22.Checked;
            bool n23 = t23.Checked;
            bool n24 = t24.Checked;
            bool n25 = t25.Checked;
            bool n26 = t26.Checked;
            bool n27 = t27.Checked;
            bool n28 = t28.Checked;
            bool n29 = t29.Checked;
            bool n30 = t30.Checked;
            bool n31 = t31.Checked;
            bool n32 = t32.Checked;
            bool n33 = t33.Checked;
            bool n34 = t34.Checked;
            bool n35 = t35.Checked;
            bool n36 = t36.Checked;
            bool n37 = t37.Checked;
            bool z1 = f1.Checked;
            bool z2 = f2.Checked;
            bool z3 = f3.Checked;
            bool z4 = f4.Checked;
            bool z5 = f5.Checked;
            bool z6 = f6.Checked;
            bool z7 = f7.Checked;
            bool z8 = f8.Checked;
            bool z9 = f9.Checked;
            bool z10 = f10.Checked;
            bool z11 = f11.Checked;
            bool z12 = f12.Checked;
            bool z13 = f13.Checked;
            bool z14 = f14.Checked;
            bool z15 = f15.Checked;
            bool z16 = f16.Checked;
            bool z17 = f17.Checked;
            bool z18 = f18.Checked;
            bool z19 = f19.Checked;
            bool z20 = f20.Checked;
            bool z21 = f21.Checked;
            bool z22 = f22.Checked;
            bool z23 = f23.Checked;
            bool z24 = f24.Checked;
            bool z25 = f25.Checked;
            bool z26 = f26.Checked;
            bool z27 = f27.Checked;
            bool z28 = f28.Checked;
            bool z29 = f29.Checked;
            bool z30 = f30.Checked;
            bool z31 = f31.Checked;
            bool z32 = f32.Checked;
            bool z33 = f33.Checked;
            bool z34 = f34.Checked;
            bool z35 = f35.Checked;
            bool z36 = f36.Checked;
            bool z37 = f37.Checked;




            // แปลงค่า boolean เป็น string ที่แสดงถึงค่า "ใช่" หรือ "ไม่ใช่"
            string n1S = n1 ? "ใช่" : "ไม่ใช่";
            string n2S = n2 ? "ใช่" : "ไม่ใช่";
            string n3S = n3 ? "ใช่" : "ไม่ใช่";
            string n4S = n4 ? "ใช่" : "ไม่ใช่";
            string n5S = n5 ? "ใช่" : "ไม่ใช่";
            string n6S = n6 ? "ใช่" : "ไม่ใช่";
            string n7S = n7 ? "ใช่" : "ไม่ใช่";
            string n8S = n8 ? "ใช่" : "ไม่ใช่";
            string n9S = n9 ? "ใช่" : "ไม่ใช่";
            string n10S = n10 ? "ใช่" : "ไม่ใช่";
            string n11S = n11 ? "ใช่" : "ไม่ใช่";
            string n12S = n12 ? "ใช่" : "ไม่ใช่";
            string n13S = n13 ? "ใช่" : "ไม่ใช่";
            string n14S = n14 ? "ใช่" : "ไม่ใช่";
            string n15S = n15 ? "ใช่" : "ไม่ใช่";
            string n16S = n16 ? "ใช่" : "ไม่ใช่";
            string n17S = n17 ? "ใช่" : "ไม่ใช่";
            string n18S = n18 ? "ใช่" : "ไม่ใช่";
            string n19S = n19 ? "ใช่" : "ไม่ใช่";
            string n20S = n20 ? "ใช่" : "ไม่ใช่";
            string n21S = n21 ? "ใช่" : "ไม่ใช่";
            string n22S = n22 ? "ใช่" : "ไม่ใช่";
            string n23S = n23 ? "ใช่" : "ไม่ใช่";
            string n24S = n24 ? "ใช่" : "ไม่ใช่";
            string n25S = n25 ? "ใช่" : "ไม่ใช่";
            string n26S = n26 ? "ใช่" : "ไม่ใช่";
            string n27S = n27 ? "ใช่" : "ไม่ใช่";
            string n28S = n28 ? "ใช่" : "ไม่ใช่";
            string n29S = n29 ? "ใช่" : "ไม่ใช่";
            string n30S = n30 ? "ใช่" : "ไม่ใช่";
            string n31S = n31 ? "ใช่" : "ไม่ใช่";
            string n32S = n32 ? "ใช่" : "ไม่ใช่";
            string n33S = n33 ? "ใช่" : "ไม่ใช่";
            string n34S = n34 ? "ใช่" : "ไม่ใช่";
            string n35S = n35 ? "ใช่" : "ไม่ใช่";
            string n36S = n36 ? "ใช่" : "ไม่ใช่";
            string n37S = n37 ? "ใช่" : "ไม่ใช่";



            // สร้าง query
            string query = "INSERT INTO healthdata (idcard,id, daymonthyear, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20, n21, n22, n23, n24, n25, n26, n27, n28, n29, n30, n31, n32, n33, n34, n35, n36, n37) VALUES ('" + idcardd + "','" + one + "','" + dob + "','" + n1S + "','" + n2S + "','" + n3S + "','" + n4S + "','" + n5S + "','" + n6S + "','" + n7S + "','" + n8S + "','" + n9S + "','" + n10S + "','" + n11S + "','" + n12S + "','" + n13S + "','" + n14S + "','" + n15S + "','" + n16S + "','" + n17S + "','" + n18S + "','" + n19S + "','" + n20S + "','" + n21S + "','" + n22S + "','" + n23S + "','" + n24S + "','" + n25S + "','" + n26S + "','" + n27S + "','" + n28S + "','" + n29S + "','" + n30S + "','" + n31S + "','" + n32S + "','" + n33S + "','" + n34S + "','" + n35S + "','" + n36S + "','" + n37S + "')";

            // เรียกใช้เมธอดที่มีการส่งพารามิเตอร์
            fn.setDate(query);

            // สร้าง query
            query = "INSERT INTO healthdata (idcard,id, daymonthyear, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20, n21, n22, n23, n24, n25, n26, n27, n28, n29, n30, n31, n32, n33, n34, n35, n36, n37) VALUES ('" + idcardd + "','" + one + "','" + dob + "','" + n1S + "','" + n2S + "','" + n3S + "','" + n4S + "','" + n5S + "','" + n6S + "','" + n7S + "','" + n8S + "','" + n9S + "','" + n10S + "','" + n11S + "','" + n12S + "','" + n13S + "','" + n14S + "','" + n15S + "','" + n16S + "','" + n17S + "','" + n18S + "','" + n19S + "','" + n20S + "','" + n21S + "','" + n22S + "','" + n23S + "','" + n24S + "','" + n25S + "','" + n26S + "','" + n27S + "','" + n28S + "','" + n29S + "','" + n30S + "','" + n31S + "','" + n32S + "','" + n33S + "','" + n34S + "','" + n35S + "','" + n36S + "','" + n37S + "')";

            // เรียกใช้เมธอดที่มีการส่งพารามิเตอร์
            fn.setDate(query);

            // Assuming `fn` is an instance of the class that contains the getData method.
            string idCard = txtID.Text;

            // สร้าง query เพื่อดึงข้อมูลจากฐานข้อมูล tb_newdonor
            query = $"SELECT name, surname, bloodgroup, email FROM tb_newdonor WHERE idcard = '{idCard}'";
            DataSet donorData = fn.getData(query);

            if (donorData.Tables[0].Rows.Count > 0)
            {
                DataRow donorRow = donorData.Tables[0].Rows[0];
                string dname = donorRow["name"].ToString();
                string sname = donorRow["surname"].ToString();
                string bloodgroup = donorRow["bloodgroup"].ToString();
                string receiverEmail = donorRow["email"].ToString();

                // สร้าง query เพื่อดึงข้อมูลจากฐานข้อมูล healthdata โดยใช้ idcard
                string queryHealthData = $"SELECT MAX(id) AS maxId FROM healthdata WHERE idcard = '{idCard}'";
                DataSet healthData = fn.getData(queryHealthData);

                if (healthData.Tables[0].Rows.Count > 0)
                {
                    DataRow healthRow = healthData.Tables[0].Rows[0];
                    int maxId = healthRow["maxId"] != DBNull.Value ? Convert.ToInt32(healthRow["maxId"]) : 0;

                    // ข้อมูลอีเมล
                    string senderEmail = "maggy.gygy@gmail.com"; // ใส่ email ของคุณ
                    string appPassword = "zziu hkfe udrz ubxr"; // ใส่ App Password ของคุณ
                    string subject = $"ขอบคุณจากใจจริงครับ ที่มาบริจาคโลหิตครั้งที่ {maxId} การให้ครั้งนี้ช่วยต่อชีวิตเพื่อนมนุษย์ได้มากมาย";

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
                else
                {
                    Console.WriteLine("No health data found with the provided ID card number.");
                }
            }
            else
            {
                Console.WriteLine("No donor found with the provided ID card number.");
            }



        }

        private void txtonce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // รับค่าจาก txtID ที่ผู้ใช้กรอก
            string inputIdCard = txtID.Text;

            // สร้างสตริง query เพื่อดึงค่า max(id) จากตาราง healthdata
            string queryMaxId = $"SELECT max(id) FROM `healthdata` WHERE idcard = '{inputIdCard}'";

            // สร้างสตริง query เพื่อดึงค่า gender จากตาราง tb_newdonor
            string queryGender = $"SELECT gender FROM `tb_newdonor` WHERE idcard = '{inputIdCard}'";

            string querybloodgroup = $"SELECT bloodgroup FROM `tb_newdonor` WHERE idcard = '{inputIdCard}'";

            // ดึงข้อมูลจากฐานข้อมูลโดยใช้ฟังก์ชัน getData
            DataSet dsMaxId = fn.getData(queryMaxId);
            DataSet dsGender = fn.getData(queryGender);
            DataSet dsbloodgroup = fn.getData(querybloodgroup);

            // ตรวจสอบว่าผลลัพธ์ที่ได้จาก queryMaxId ไม่ว่างเปล่า
            if (dsMaxId.Tables[0].Rows.Count > 0 && dsMaxId.Tables[0].Rows[0][0] != DBNull.Value)
            {
                // แปลงค่าที่ได้จาก DataSet เป็นจำนวนเต็ม และเพิ่มค่า 1
                int maxId = int.Parse(dsMaxId.Tables[0].Rows[0][0].ToString()) + 1;

                // ตั้งค่าให้ label3 แสดงผลค่าที่ได้
                label3.Text = maxId.ToString();
            }
            else
            {
                // จัดการกรณีที่ไม่มีข้อมูลในตาราง
                label3.Text = "ไม่พบผู้ใช้";
            }

            // ตรวจสอบว่าผลลัพธ์ที่ได้จาก queryGender ไม่ว่างเปล่า
            if (dsGender.Tables[0].Rows.Count > 0 && dsGender.Tables[0].Rows[0][0] != DBNull.Value)
            {
                // ตรวจสอบว่าเป็นเพศชายหรือเพศหญิง
                string gender = dsGender.Tables[0].Rows[0]["gender"].ToString();
                if (gender == "ชาย")
                {
                    // ติ๊ก checkbox สำหรับ F10, F11, F12 ถ้าเป็นเพศชาย
                    f10.Checked = true;
                    f11.Checked = true;
                    f12.Checked = true;
                    // ยกเลิกการติ๊ก checkbox สำหรับ F15 ถ้าไม่ใช่เพศหญิง
                    f15.Checked = false;
                }
                else if (gender == "หญิง")
                {
                    // ติ๊ก checkbox สำหรับ F15 ถ้าเป็นเพศหญิง
                    f15.Checked = true;
                    // ยกเลิกการติ๊ก checkbox สำหรับ F10, F11, F12 ถ้าไม่ใช่เพศชาย
                    f10.Checked = false;
                    f11.Checked = false;
                    f12.Checked = false;
                }
                else
                {
                    // ยกเลิกการติ๊ก checkbox สำหรับทุกกรณีถ้าไม่ใช่เพศชายหรือหญิง
                    f10.Checked = false;
                    f11.Checked = false;
                    f12.Checked = false;
                    f15.Checked = false;
                }
            }
            else
            {
                // จัดการกรณีที่ไม่มีข้อมูลในตาราง
                label3.Text = "ไม่พบผู้ใช้";
                // ยกเลิกการติ๊ก checkbox สำหรับทุกกรณีที่ไม่พบผู้ใช้
                f10.Checked = false;
                f11.Checked = false;
                f12.Checked = false;
                f15.Checked = false;
            }
            // ตรวจสอบว่าผลลัพธ์ที่ได้จาก queryBloodGroup ไม่ว่างเปล่า
            if (dsbloodgroup.Tables[0].Rows.Count > 0 && dsbloodgroup.Tables[0].Rows[0][0] != DBNull.Value)
            {
                // ดึงข้อมูล bloodgroup ออกมา
                string bloodGroup = dsbloodgroup.Tables[0].Rows[0]["bloodgroup"].ToString();

                // แสดง bloodgroup ใน label4
                label4.Text = bloodGroup;
            }
            else
            {
                // จัดการกรณีที่ไม่มีข้อมูลในตาราง bloodgroup
                label4.Text = "ไม่พบข้อมูลกลุ่มเลือด";
            }

            // ตรวจสอบว่าผู้ใช้กรอกข้อมูลเลขบัตรประชาชนและได้รับข้อมูล blood group
            if (!string.IsNullOrEmpty(inputIdCard) && !string.IsNullOrEmpty(label4.Text))
            {
                // รับค่า blood group จากผู้ใช้
                string bloodGroup = label4.Text;
                
                // เพิ่มข้อมูลลงในตาราง `stock`
                string updateQuery = "";
                if (bloodGroup == "O+" || bloodGroup == "A+" || bloodGroup == "B+" || bloodGroup == "AB+" || bloodGroup == "A-" || bloodGroup == "O-" || bloodGroup == "B-" || bloodGroup == "AB-")
                {
                    // เพิ่มจำนวน quantity ในกรณีที่ blood group เป็น A+, B+, AB+, A-, O-, B-, AB- เพิ่มเพียงแค่ +1 เท่านั้น
                    updateQuery = $"UPDATE `stock` SET quantity = quantity + 1 WHERE bloodgroup = '{bloodGroup}'";
                }

                // ทำการเพิ่มข้อมูลด้วยฟังก์ชันที่กำหนด
                fn.getData(updateQuery);
            }

            


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
