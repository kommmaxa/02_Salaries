using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            Console.WriteLine("Employees before sorting:");
            employees.Print();
            Console.WriteLine("Saving data to XML file...");
            SaveEmployeesToXml(employees);
            employees.Sort();
            Console.WriteLine("Employees after sorting:");
            employees.Print();
            Console.WriteLine("Names of the first 5 employees:");
            employees.PrintOnePropertyOfSeveralObjects(5, "EmployeeName", true);
            Console.WriteLine("IDs of the last 3 employees:");
            employees.PrintOnePropertyOfSeveralObjects(3, "EmployeeID", false);
            employees.RemoveRange(0, employees.Count);
            employees = ReadEmployeesFromXml("employeeDocument.xml");
            Console.WriteLine("Original list of employees, read from file:");
            employees.Print();
            SaveEmployeesToXml(employees);

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

        public static void SaveEmployeesToXml (List<Employee> employeeList)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlNode root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            doc = WriteEmployeeIntoXmlFile(doc, employeeList);
            doc.Save("employeeDocument.xml");
        }

        public static XmlDocument WriteEmployeeIntoXmlFile(XmlDocument doc, List<Employee> employeeList)
        {
            XmlNode employees = doc.CreateElement("Employees");
            doc.AppendChild(employees);
            foreach (var employee in employeeList)
            {
                XmlNode employeeNode = GenerateEmployeeSubNodeWithAttribute(doc, "Employee", "", "SalaryBase", DefineEmployeeSalaryBase(employee));
                employees.AppendChild(employeeNode);
                employeeNode.AppendChild(GenerateEmployeeSubNode(doc, "ID", employee.EmployeeID.ToString()));
                employeeNode.AppendChild(GenerateEmployeeSubNode(doc, "Name", employee.EmployeeName));
                if (employee is EmployeeWithHourBasedSalary)
                {
                    employeeNode.AppendChild(GenerateEmployeeSubNodeWithAttribute(doc, "MonthSalary", employee.CalculateAverageMonthSalary.ToString(), "SalaryPerHour", ((EmployeeWithHourBasedSalary)employee).SalaryPerHour.ToString()));
                }
                else
                {
                    employeeNode.AppendChild(GenerateEmployeeSubNode(doc, "MonthSalary", employee.CalculateAverageMonthSalary.ToString()));
                }
            }
            return doc;
        }

        public static XmlNode GenerateEmployeeSubNode(XmlDocument doc, string nodeName, string nodeValue)
        {
            XmlNode result = doc.CreateElement(nodeName);
            result.InnerText = nodeValue;
            return result;
        }
        public static XmlNode GenerateEmployeeSubNodeWithAttribute(XmlDocument doc, string nodeName, string nodeValue, string attributeName, string attributeValue)
        {
            XmlNode node = GenerateEmployeeSubNode(doc, nodeName, nodeValue);
            return GenerateEmployeeSubNodeAttribute(node, doc, attributeName, attributeValue);
        }
        public static XmlNode GenerateEmployeeSubNodeAttribute(XmlNode node, XmlDocument doc, string attributeName, string attributeValue)
        {
            XmlAttribute attribute = doc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;
            node.Attributes.Append(attribute);
            return node;
        }

        public static List <Employee> ReadEmployeesFromXml(string xmlFileName)
        {
            List<Employee> result = new List<Employee>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            var nodes = xmlDoc.DocumentElement.SelectNodes("descendant::Employee");
            foreach (XmlNode node in nodes)
            {
                result.Add(ImportEmployeeFromXmlFile(node));
            }
            return result;
        }

        public static Employee ImportEmployeeFromXmlFile(XmlNode node)
        {
            if (node.Attributes["SalaryBase"].Value == Employee.hourBased)
            {
                return new EmployeeWithHourBasedSalary(node["Name"].InnerText.ToString(), Convert.ToInt16(node["ID"].InnerText), Convert.ToInt16(node["MonthSalary"].GetAttributeNode("SalaryPerHour").InnerText));
            }
            else
            {
                return new EmployeeWithMonthBasedSalary(node["Name"].InnerText.ToString(), Convert.ToInt16(node["ID"].InnerText), Convert.ToInt16(node["MonthSalary"].InnerText));
            }
        }
        public static string DefineEmployeeSalaryBase(Employee employee)
        {
            return (employee is EmployeeWithHourBasedSalary)?Employee.hourBased:Employee.monthBased;
        }
    }
}
