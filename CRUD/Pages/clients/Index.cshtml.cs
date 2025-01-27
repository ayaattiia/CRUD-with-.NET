using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages.clients
{
    public class IndexModel : PageModel
    {

        public List<ClientInfo> ListClients = new List<ClientInfo>();

        public void OnGet()
        {
            //to file this list "ListClients" 
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD_BD;Integrated Security=True;Encrypt=False";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String Sql = "select * from clients";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();

                                clientInfo.id = " " + reader.GetInt32(0);// tzid " " bach tbadall l type id l string 5aterha int fil data base  
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);

                                //clientInfo.created_at = reader.GetString(5).ToString();
                                clientInfo.created_at = reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"); // Format date/heure

                                ListClients.Add(clientInfo);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception : " +ex.ToString());
            }
        }
    }
    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
        
    }

}