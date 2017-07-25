using System;

namespace _02_Salaries
{
    public abstract class Employee : IComparable<Employee>
    {
        public abstract int CalculateAverageMonthSalary { get; }
        public abstract int CompareTo(Employee other);
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }
        protected static string[] employeeNames = { "Vasya", "Petya", "Kyryl", "Masha", "Arsen", "Olya", "Levon", "Oleksii", "Andriy" };
        public const string hourBased = "HourBased";
        public const string monthBased = "MonthBased";
    }
}
