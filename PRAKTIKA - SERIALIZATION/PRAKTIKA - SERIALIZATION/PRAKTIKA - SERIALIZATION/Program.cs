using System;
using System.IO;

class Program
{
	static void Main()
	{
		const string jsonFilePath = "D:\\CodeAcademy\\PRAKTIKA - SERIALIZATION\\PRAKTIKA - SERIALIZATION\\Data\\customers.json";
		Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));

		var customer1 = new Customer { Id = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1234567890" };
		var customer2 = new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "0987654321" };

		Customer.Add(jsonFilePath, customer1);
		Customer.Add(jsonFilePath, customer2);

		var searchResult = Customer.Search(jsonFilePath, 1);
		Console.WriteLine(searchResult != null ? $"Found: {searchResult.FirstName} {searchResult.LastName}" : "Not Found");

		Customer.Update(jsonFilePath, 1, "John", "Doe", "1111111111");

		Customer.DeleteCustomer(jsonFilePath, 2);

		Customer.GetAll(jsonFilePath);
	}
}
