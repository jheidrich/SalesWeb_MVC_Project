﻿using System;
using System.Collections.Generic;
using System.Linq; 

namespace SalesProjectMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); 

        public Seller()
        {
        }

        public Seller(int id, string name, string eMail, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            EMail = eMail;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord saleRecord)
        {
            Sales.Add(saleRecord); 
        }

        public void RemoveSales(SalesRecord saleRecord)
        {
            Sales.Remove(saleRecord); 
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(saleR => saleR.Date >= initial && saleR.Date <= final).
                Sum(saleR => saleR.Amount); 
        }
    }
}