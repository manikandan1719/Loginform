using System;
using System.Data;
using System.Data.SqlClient;

namespace EPMS1
{
    public class Utilise
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CBMELKSConn"].ConnectionString);
        SqlConnection MainCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CBMELKSConn"].ConnectionString);

        public DataSet getDataSet(string qry)
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            con.Close();
            return ds;
        }

        public DataSet getDataSet_SP(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = MainCon;
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(ds, "data");
            return ds;
        }

        //Encrypt
        public string Encrypt(string EncryptData)
        {
            byte[] encData_byte = new byte[EncryptData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(EncryptData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        //Decrypt
        public string Decrypt(string DecryptData)
        {
            //Decode
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(DecryptData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result1 = new String(decoded_char);

            return result1;
        }
    }
}