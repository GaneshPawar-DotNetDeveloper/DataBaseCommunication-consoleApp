using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cofeeshopeApplication
{
  public class HelperClass
    {
        string connectionString = "data source=DESKTOP-KVGH71D;database= CoffeeShop;integrated security=true";

       public void DisplayMenu()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Product Menu:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]}: {reader["Name"]} - ${reader["Price"]}");
                }

                reader.Close();
            }
            Console.WriteLine();
        }
        
       public void HandleCustomerVisit()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter your mobile number: ");
            string mobile = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter your location: ");
            string location = Console.ReadLine();
            Console.WriteLine();

            int customerId = GetOrCreateCustomer(name, mobile, location);
            int visitCount = GetVisitCount(customerId);

            if (visitCount == 0)
            {
                Console.WriteLine(" you are First time visiting! Enjoy your coffee!");
            }
             if (visitCount == 1)
            {
                Console.WriteLine(" you are second time visiting! Enjoy your coffee!");
            }
             if (visitCount == 2)
            {
                Console.WriteLine(" you are third time visiting! Enjoy your coffee!");
            }
             
            LogVisit(customerId);

            if (visitCount > 0 && (visitCount + 1) % 3 == 0)
            {
                Console.WriteLine("Congratulations! You've earned a free coffee!");
                Console.WriteLine("Please wait for 15 second...");
                Thread.Sleep(15000);
                Console.WriteLine("Order successful! Your total is $0.00");
            }
            else
            {
                TakeOrder(customerId);
            }
        }

       public int GetOrCreateCustomer(string name, string mobile, string location)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand checkCustomer = new SqlCommand(
                    "SELECT CustomerID FROM Customers WHERE MobileNumber = @Mobile", connection);
                checkCustomer.Parameters.AddWithValue("@Mobile", mobile);
                object result = checkCustomer.ExecuteScalar();

                if (result != null)
                {
                    return (int)result;
                }

                SqlCommand insertCustomer = new SqlCommand(
                    "INSERT INTO Customers (Name, MobileNumber, Location) OUTPUT INSERTED.CustomerID VALUES (@Name, @Mobile, @Location)", connection);
                insertCustomer.Parameters.AddWithValue("@Name", name);
                insertCustomer.Parameters.AddWithValue("@Mobile", mobile);
                insertCustomer.Parameters.AddWithValue("@Location", location);

                return (int)insertCustomer.ExecuteScalar();
            }
        }

       public void LogVisit(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertVisit = new SqlCommand(
                    "INSERT INTO Visits (CustomerID, VisitDate) VALUES (@CustomerID, GETDATE())", connection);
                insertVisit.Parameters.AddWithValue("@CustomerID", customerId);

                insertVisit.ExecuteNonQuery();
            }
        }

    public int GetVisitCount(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand countVisits = new SqlCommand(
                    "SELECT COUNT(*) FROM Visits WHERE CustomerID = @CustomerID", connection);
                countVisits.Parameters.AddWithValue("@CustomerID", customerId);

                return (int)countVisits.ExecuteScalar();
            }
        }

       public void TakeOrder(int customerId)
        {
            Console.WriteLine("Please enter the product IDs you want to order, separated by commas:");
            string orderInput = Console.ReadLine();
            string[] productIds = orderInput.Split(',');

            decimal total = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                 
                connection.Open();
                    foreach (string productId in productIds)
                    {
                        SqlCommand getPrice = new SqlCommand(
                            "SELECT Price FROM Products WHERE ProductID = @ProductID", connection);
                        getPrice.Parameters.AddWithValue("@ProductID", productId.Trim());

                        total += (decimal)getPrice.ExecuteScalar(); 
                    
                }

                decimal discount = 0.05m * total;
                decimal gst = 0.09m * total;
                decimal cgst = 0.09m * total;
                decimal finalTotal = total + gst + cgst - discount;

                SqlCommand insertOrder = new SqlCommand(
                    "INSERT INTO Orders (CustomerID, Total, OrderDate) VALUES (@CustomerID, @Total, GETDATE())", connection);
                insertOrder.Parameters.AddWithValue("@CustomerID", customerId);
                insertOrder.Parameters.AddWithValue("@Total", finalTotal);

                insertOrder.ExecuteNonQuery();

                Console.WriteLine("Thank you for your order! Please wait for 10 Second...");
                Thread.Sleep(10000);
                Console.WriteLine();
                Console.WriteLine($"Order successful! Your total is Rs: {finalTotal}");
            }
        }
    }

}

