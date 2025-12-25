using myFirstApi.Models;
using myFirstApi.Repository.Defination;
using System.Data;
using System.Data.SqlClient;

namespace myFirstApi.Repository.Implentation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration config;

        public EmployeeRepository(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<bool> AddEmployeeAsync(Employee emp)
        {
            bool flag = false;
            string? constring=config.GetConnectionString("DefaultConnection");
            string query = "INSERT INTO Employees (Name, Email, Password) VALUES (@Name, @Email, @Password)";
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = emp.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = emp.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = emp.Password;
                await  con.OpenAsync();
                flag= await cmd.ExecuteNonQueryAsync()>0;
                await con.CloseAsync();
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return flag;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            bool flag = false;
            string? constring = config.GetConnectionString("DefaultConnection");
            string query = "delete from Employees where id =@id";
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                
                await con.OpenAsync();
                flag = await cmd.ExecuteNonQueryAsync() > 0;
                await con.CloseAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return flag;

        }

        public async Task<IList<Employee>> GetAllEmployeeAsync()
        {
            IList<Employee> list = null;
            string? constring = config.GetConnectionString("DefaultConnection");
            try {

                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("Select [Id],[Name],[Email] from Employees", con);
               await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                list=new List<Employee>();
                while (reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Id=reader.GetInt32("Id"),
                        Name=reader.GetString("Name"),
                        Email=reader.GetString("Email")
                    };
                    list.Add(employee);
                }
                await con.CloseAsync();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return list;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
           IList<Employee> list= await this.GetAllEmployeeAsync();
           return  list.FirstOrDefault(ob => ob.Email.Equals(email));
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            IList<Employee> list = await this.GetAllEmployeeAsync();
            return list.FirstOrDefault(e => e.Id == id);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee emp)
        {
            bool flag = false;
            string? constring = config.GetConnectionString("DefaultConnection");
            string query = "Update Employees set Name=@Name, Email= @Email, Password= @Password where id =@id";
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = emp.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = emp.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = emp.Password;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = emp.Id;
                await con.OpenAsync();
                flag = await cmd.ExecuteNonQueryAsync() > 0;
                await con.CloseAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return flag;
        }
    }
}
