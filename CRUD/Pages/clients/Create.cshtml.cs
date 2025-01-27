using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages.clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo ClientInfo =new ClientInfo();
        public string ErrorMessage = string.Empty;
        public string SuccessMessage = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            ClientInfo.name = Request.Form["name"];
            ClientInfo.email = Request.Form["email"];
            ClientInfo.phone = Request.Form["phone"];
            ClientInfo.address = Request.Form["address"];

            if (ClientInfo.name.Length == 0 || ClientInfo.email.Length ==0 || ClientInfo.phone.Length == 0 || ClientInfo.address.Length == 0)
            {
                ErrorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD_BD;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    String sql = "INSERT INTO clients " + 
                        " (name , email , phone, address ) VALUES " + 
                        " (@name , @email , @phone, @address ); ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@name", ClientInfo.name);
                        command.Parameters.AddWithValue("@email", ClientInfo.email);
                        command.Parameters.AddWithValue("@phone", ClientInfo.phone);
                        command.Parameters.AddWithValue("@address", ClientInfo.address);
                        
                        
                        command.ExecuteNonQuery();
                    }

                }

            }catch (Exception ex) 
            { 
                ErrorMessage = ex.Message;
                return;
            }


            //save the new client into the database 

            ClientInfo.name = " ";ClientInfo.email = " ";ClientInfo.phone = " "; ClientInfo.address = " ";
            SuccessMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");

        }
    }
}
