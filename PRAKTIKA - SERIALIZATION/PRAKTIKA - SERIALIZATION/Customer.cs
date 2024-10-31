using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Customer
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }

	public static void Add(string path, Customer customer)
	{
		var customers = DeserializeCustomers(path);
		if (customers.Exists(c => c.Id == customer.Id))
		{
			Console.WriteLine("Customer with the same ID already exists.");
			return;
		}
		customers.Add(customer);
		SerializeCustomers(path, customers);
	}

	public static Customer Search(string path, int id)
	{
		var customers = DeserializeCustomers(path);
		return customers.Find(c => c.Id == id);
	}

	public static void Update(string path, int id, string newFirstName, string newLastName, string newPhoneNumber)
	{
		var customers = DeserializeCustomers(path);
		var customer = customers.Find(c => c.Id == id);
		if (customer != null)
		{
			customer.FirstName = newFirstName;
			customer.LastName = newLastName;
			customer.PhoneNumber = newPhoneNumber;
			SerializeCustomers(path, customers);
		}
		else
		{
			Console.WriteLine("Customer not found.");
		}
	}

	public static void DeleteCustomer(string path, int id)
	{
		var customers = DeserializeCustomers(path);
		var customer = customers.Find(c => c.Id == id);
		if (customer != null)
		{
			customers.Remove(customer);
			SerializeCustomers(path, customers);
		}
		else
		{
			Console.WriteLine("Customer not found.");
		}
	}

	public static void GetAll(string path)
	{
		var customers = DeserializeCustomers(path);
		foreach (var customer in customers)
		{
			Console.WriteLine($"ID: {customer.Id}, Name: {customer.FirstName} {customer.LastName}, Phone: {customer.PhoneNumber}");
		}
	}

	private static List<Customer> DeserializeCustomers(string path)
	{
		if (!File.Exists(path))
		{
			return new List<Customer>();
		}

		using (StreamReader sr = new StreamReader(path))
		{
			var jsonData = sr.ReadToEnd();
			return string.IsNullOrWhiteSpace(jsonData)
				? new List<Customer>()
				: JsonConvert.DeserializeObject<List<Customer>>(jsonData) ?? new List<Customer>();
		}
	}

	private static void SerializeCustomers(string path, List<Customer> customers)
	{
		string jsonData = JsonConvert.SerializeObject(customers, Formatting.Indented);
		using (StreamWriter sw = new StreamWriter(path))
		{
			sw.Write(jsonData);
		}
	}
}
