using System.Collections.Generic;

namespace _02_Salaries
{
    class Program
    {
        static int CountOfMonthSalaryEmployees = 4;
        static int CountOfHourSalaryEmployees = 3;
        static List <Employee> employees = new List <Employee> (CountOfMonthSalaryEmployees + CountOfHourSalaryEmployees);

        static void Main(string[] args)
        {
            GenerateEmployees();
            employees.Sort();
            employees.Print();
            employees.PrintOnePropertyOfSeveralObjects(5, "EmployeeName", true);
            employees.PrintOnePropertyOfSeveralObjects(3, "EmployeeID", false);

        }
        static void GenerateEmployees()
        {
            for (int i = 0; i < CountOfMonthSalaryEmployees; i++)
            {
                employees.Add(new EmployeeWithMonthBasedSalary(i));
            }
            for (int i = CountOfMonthSalaryEmployees; i < CountOfHourSalaryEmployees + CountOfMonthSalaryEmployees; i++)
            {
                employees.Add(new EmployeeWithHourBasedSalary(i));
            }
        }
    }
}
