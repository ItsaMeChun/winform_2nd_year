using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace _431_396_CDTH19E
{
    class clsBanHang
    {
        SqlConnection con = new SqlConnection();
        void ketNoi()
        {
            con.ConnectionString= @"Data Source=.\SQLEXPRESS;Initial Catalog=banhang;Integrated Security=True";
            if(con.State==ConnectionState.Closed)
                con.Open();
        }
        public clsBanHang()
        {
             ketNoi();
        }

        public DataSet layDuLieu(string sql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            return ds;
        }

        public int capNhatDuLieu(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }

        public DataTable layDuLieuTheoThuTuc(string tenthutuc, string tenthamso, string giatrithamso)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = tenthutuc;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter pa = new SqlParameter(tenthamso, giatrithamso);
            cmd.Parameters.Add(pa);
            
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable layDuLieuTheoThuTuc2(string tenthutuc, string gt1, string gt2)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = tenthutuc;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter pa = new SqlParameter("@sdt", gt1);
            cmd.Parameters.Add(pa);
            SqlParameter pa1 = new SqlParameter("@trangthai", gt2);
            cmd.Parameters.Add(pa1);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }

 
}
