using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class EmployeeBusLayer
    {
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public IEnumerable<Employee> Employees
        {
            get
            {

                List<Employee> employees = new List<Employee>();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand("getAllEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.Name = reader["Name"].ToString();
                    emp.Gender = reader["Gender"].ToString();
                    emp.City = reader["City"].ToString();
                    emp.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());

                    employees.Add(emp);


                }
                return employees;
            }

        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("addEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter name = new SqlParameter();
                name.ParameterName = "@Name";
                name.Value = employee.Name;
                cmd.Parameters.Add(name);

                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@gender";
                gender.Value = employee.Gender;
                cmd.Parameters.Add(gender);

                SqlParameter city = new SqlParameter();
                city.ParameterName = "@city";
                city.Value = employee.City;
                cmd.Parameters.Add(city);

                SqlParameter dptid = new SqlParameter();
                dptid.ParameterName = "@dptId";
                dptid.Value = employee.DepartmentId;
                cmd.Parameters.Add(dptid);

                con.Open();
                cmd.ExecuteNonQuery();


            }
        }

        public void saveEmployee(Employee employee)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("updateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name",employee.Name);
                cmd.Parameters.Add(name);

                SqlParameter gender = new SqlParameter("@gender", employee.Gender);
                cmd.Parameters.Add(gender);

                SqlParameter city = new SqlParameter("@city", employee.City);
                cmd.Parameters.Add(city);

                SqlParameter dpt = new SqlParameter("@dptid", employee.DepartmentId);
                cmd.Parameters.Add(dpt);

                SqlParameter id = new SqlParameter("@id", employee.EmployeeId);
                cmd.Parameters.Add(id);

                cmd.ExecuteNonQuery();
                con.Close();


            }
        }

    }
}
