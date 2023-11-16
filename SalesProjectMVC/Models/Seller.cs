using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; 

namespace SalesProjectMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        [Display(Name = "Department Name")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
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

            if (department != null)
                DepartmentId = department.Id;
            else
                DepartmentId = 0;
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
