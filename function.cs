using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace BloodBank_Video
{
    internal class function
    {
       
        protected MySqlConnection getConnection()
        {
            string connectionString = "server=127.0.0.1;port=3306;user=root;password=;database=bloodbank;";
            MySqlConnection con = new MySqlConnection(connectionString);
            return con;
        }
        public DataSet getData(String query) // ใช้สำหรับการดึงข้อมูลจากฐานข้อมูล MySQL 
        {
            MySqlConnection con = getConnection(); //สร้างการเชื่อมต่อกับฐานข้อมูล จะสร้างการเชื่อมต่อกับฐานข้อมูล MySQL โดยใช้ข้อมูลการเชื่อมต่อที่กำหนดไว้
            MySqlCommand cmd = new MySqlCommand(); //MySqlCommand เป็นคลาสที่ใช้สำหรับส่งคำสั่ง SQL ไปยังฐานข้อมูล MySQL
            cmd.Connection = con;  // cmd.Connection กำหนดการเชื่อมต่อกับฐานข้อมูล 
            cmd.CommandText = query; // cmd.CommandText กำหนดคำสั่ง SQL ที่ต้องการใช้
            MySqlDataAdapter da = new MySqlDataAdapter(cmd); // MySqlDataAdapter เป็นคลาสที่ใช้สำหรับดึงข้อมูลจากฐานข้อมูล MySQL
            DataSet ds = new DataSet(); //ds เป็น DataSet ที่ใช้สำหรับเก็บข้อมูล
            da.Fill(ds); // ดึงข้อมูลจากฐานข้อมูล MySQL ไปยัง DataSet
            return ds;

        }
        public void setDate(String query) // ใช้สำหรับการดำเนินการเพิ่มหรืออัปเดตข้อมูลในฐานข้อมูล
        {
            MySqlConnection con = getConnection(); // ฟังก์ชั่น getConnection() จะสร้างการเชื่อมต่อกับฐานข้อมูล MySQL โดยใช้ข้อมูลการเชื่อมต่อที่กำหนดไว้
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con); //MySqlCommand เป็นคลาสที่ใช้สำหรับส่งคำสั่ง SQL ไปยังฐานข้อมูล MySQL
            cmd.Connection = con;
            cmd.ExecuteNonQuery(); // cmd.ExecuteNonQuery()  ใช้สำหรับส่งคำสั่ง SQL ไปยังฐานข้อมูล MySQL
            con.Close();
            
            MessageBox.Show("Data Processd Successfully.","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void setDate(String query, byte[] imageData) // ใช้สำหรับการดำเนินการเพิ่มหรืออัปเดตข้อมูลในฐานข้อมูล MySQL ซึ่งรวมถึงข้อมูลภาพที่เป็น binary data 
        {
            MySqlConnection con = getConnection();
            con.Open(); // เปิดการเชื่อมต่อกับฐานข้อมูล MySQL
            MySqlCommand cmd = new MySqlCommand(query, con);

            // เพิ่มพารามิเตอร์แบบ binary data สำหรับรูปภาพ
            cmd.Parameters.Add("@image", MySqlDbType.Blob).Value = imageData;

            cmd.ExecuteNonQuery(); // cmd.ExecuteNonQuery()  ใช้สำหรับส่งคำสั่ง SQL ไปยังฐานข้อมูล MySQL
            con.Close();

            MessageBox.Show("Data Processed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
