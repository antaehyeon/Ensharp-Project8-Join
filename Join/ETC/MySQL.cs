using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Join
{
    class MySQL
    {
        // MySQL Connect Info
        string strConn = "Server=localhost; Database=memberdb; Uid=root; Pwd=xogus1696";
        MySqlConnection conn;
        MySqlCommand cmd;

        SharingData sd;

        string id = "";
        string pw = "";
        string name = "";
        string postalCode = "";
        string address = "";
        string email = "";
        string phoneNumber = "";
        string sex = "";
        string birthDay = "";

        public MySQL()
        {
            conn = new MySqlConnection(strConn);

            sd = SharingData.GetInstance();
        }

        // DB의 데이터를 현재 List에 전송
        public void DBDataToList()
        {
            int row = 0;

            DataSet ds = new DataSet();

            string sql = "SELECT * FROM member";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds);

            foreach(DataRow r in ds.Tables[0].Rows)
            {
                id = Convert.ToString(r["ID"]);
                pw = Convert.ToString(r["PW"]);
                name = Convert.ToString(r["Name"]);
                postalCode = Convert.ToString(r["postalCode"]);
                address = Convert.ToString(r["Address"]);
                email = Convert.ToString(r["Email"]);
                phoneNumber = Convert.ToString(r["PhoneNumber"]);
                sex = Convert.ToString(r["Sex"]);
                birthDay = Convert.ToString(r["BirthDay"]);
            }

            if (id.Equals("")) { return; }

            MemberVO memberData = new MemberVO(id, pw, name, phoneNumber, postalCode, address, email, sex, birthDay);

            sd.MemberList.Add(memberData);
        }

        public void insertMemberData(MemberVO member)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO member(ID, PW, Name, Email, PhoneNumber, Sex, BirthDay, PostalCode, Address) VALUES (@ID, @PW, @Name, @Email, @PhoneNumber, @Sex, @BirthDay, @PostalCode, @Address);";

            //@ID, @PW, @Name, @Email, @PhoneNumber, @Sex, @BirthDay, @PostalCode, @Address
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = member.Id;
            cmd.Parameters.Add("@PW", MySqlDbType.VarChar).Value = member.Pw;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = member.Name;
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = member.Email;
            cmd.Parameters.Add("@PhoneNumber", MySqlDbType.VarChar).Value = member.PhoneNumber;
            cmd.Parameters.Add("@Sex", MySqlDbType.VarChar).Value = member.Sex;
            cmd.Parameters.Add("@BirthDay", MySqlDbType.VarChar).Value = member.BrithDay;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.VarChar).Value = member.PostalCode;
            cmd.Parameters.Add("@Address", MySqlDbType.VarChar).Value = member.Address;
            cmd.ExecuteNonQuery();
            conn.Close();

            sd.MemberList.Add(member);
        }

        public void deleteTuple()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM member WHERE Id = '" + sd.CurrentId + "';";
            cmd.ExecuteNonQuery();
            cmd.Clone();
        }


    }


}
