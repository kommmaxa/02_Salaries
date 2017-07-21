using System.Linq;

namespace _02_Salaries
{
    class EmployeeWithMonthBasedSalary: Employee
    {
        int _fixedMonthSalary;
        int randomIntegerValue = RandomUtility.GetRandomInteger(100, 10000);
        int randomIntegerValueOfEmployeeName = RandomUtility.GetRandomInteger(0, employeeNames.Count());
        
        public EmployeeWithMonthBasedSalary(string employeeName, int employeeID, int fixedMonthSalary)
        {
            this.EmployeeName = employeeName;
            this.EmployeeID = employeeID;
            this._fixedMonthSalary = fixedMonthSalary;
        }

        public EmployeeWithMonthBasedSalary(int employeeID)
        {
            this.EmployeeName = employeeNames[randomIntegerValueOfEmployeeName];
            this.EmployeeID = employeeID;
            this._fixedMonthSalary = randomIntegerValue;
        }
        public override int CalculateAverageMonthSalary
        {
            get
            {
                return _fixedMonthSalary;
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
