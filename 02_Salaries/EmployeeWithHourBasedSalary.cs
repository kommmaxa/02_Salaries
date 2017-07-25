using System;
using System.Linq;

namespace _02_Salaries
{
    class EmployeeWithHourBasedSalary : Employee
    {
        const int workingHoursPerDay = 8;
        const double workingDaysPerMonth = 20.8;
        int randomIntegerValue = RandomUtility.GetRandomInteger(1, 100);
        int randomIntegerValueOfEmployeeName = RandomUtility.GetRandomInteger(0, employeeNames.Count());
        public int SalaryPerHour { get; }
        public EmployeeWithHourBasedSalary(string employeeName, int employeeID, int salaryPerHour)
        {
            this.EmployeeName = employeeName;
            this.EmployeeID = employeeID;
            this.SalaryPerHour = salaryPerHour;
        }
        public EmployeeWithHourBasedSalary(int employeeID)
        {
            this.EmployeeName = employeeNames[randomIntegerValueOfEmployeeName];
            this.EmployeeID = employeeID;
            this.SalaryPerHour = randomIntegerValue;
        }
        public override int CalculateAverageMonthSalary
        {
            get
            {
                return Convert.ToInt16(workingDaysPerMonth * workingHoursPerDay * SalaryPerHour);
            }
        }
        public override int CompareTo(Employee secondEmployee)
        {
            if (this.CalculateAverageMonthSalary > secondEmployee.CalculateAverageMonthSalary)
                return -1;
            if (this.CalculateAverageMonthSalary < secondEmployee.CalculateAverageMonthSalary)
                return 1;
            else
                return 0;
        }
        public override string ToString()
        {
            return "Employee " + this.EmployeeName + " with ID = " + this.EmployeeID + " and salary " + this.CalculateAverageMonthSalary;
        }

    }
}
