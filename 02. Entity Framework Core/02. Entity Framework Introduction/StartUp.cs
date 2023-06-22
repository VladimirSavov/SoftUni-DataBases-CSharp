using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Data.Models;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = RemoveTown(context);

            Console.WriteLine(result);
        }

        //Problem 3
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees;
            foreach (var employee in employees)
            {
                string firstName = employee.FirstName;
                string middleName = employee.MiddleName;
                string LastName = employee.LastName;
                string jobTitle = employee.JobTitle;
                var salary = employee.Salary;
                sb.AppendLine($"{firstName} {middleName} {LastName} {jobTitle} {salary:F2}");
            }
            return sb.ToString();
        }

        //Problem 4
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employees = context
                .Employees
                .Where(x => x.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }
            return stringBuilder.ToString();
        }
        //Problem 5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }
            return sb.ToString();
        }

        //Problem 6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee employeeNakov = context
                .Employees
                .First(e => e.LastName == "Nakov");

            employeeNakov.Address = newAddress;

            context.SaveChanges();

            List<string> addresses = context
                .Employees
                .OrderByDescending(e =>
                e.AddressId)
                .Take(10)
                .Select (e => e.Address.AddressText)
                .ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine(address);
            }

            return sb.ToString();
        }

        //Problem 7
        //public static string GetEmployeesInPeriod(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var employees = context
        //        .Employees
        //        .Where(e => e.)
        //        .Take(10)
        //}

        //Problem 9
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    ProjectName = e.Projects.First().Name
                })
                .OrderBy(e => e.FirstName)
                .ToList();
                
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                sb.AppendLine(employee.ProjectName);
            }
            return sb.ToString();
        }

        //Problem 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var departments = context
                .Departments
                .Where(e => e.Employees.Count > 5)
                .Select(d => new
                {
                    d.Name,
                    d.Manager.FirstName,
                    d.Manager.LastName,
                    DepEmployees = d.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e =>
                    e.LastName)
                    .ToList()
                })
            .OrderBy(e => e.DepEmployees.Count)
                .ThenBy(d => d.Name)
                .ToList();

            foreach(var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.FirstName} {d.LastName}");
                foreach (var e in d.DepEmployees)
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }
            return sb.ToString();
        }

        //Problem 11
        //public static string GetLatestProjects(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var latestProjects = context
        //        .Projects
        //        .OrderBy(p => p.StartDate)
                
                

        //}

        //Problem 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            IQueryable<Employee> employees = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services");
            foreach (Employee employee in employees) 
            {
                employee.Salary += employee.Salary * 0.12m;
            }
               context.SaveChanges();
                
            var employeesInfo =
                employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();
            foreach (var e in employeesInfo)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }
            return sb.ToString();   
        }

        //Problem 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
               .Employees
               .Where(e => e.FirstName.StartsWith("Sa"))

               .Select(e => new
               {
                   e.FirstName,
                   e.LastName,
                   e.JobTitle,
                   e.Salary
               })
               .OrderBy(e => e.FirstName)
               .ThenBy(e => e.LastName)
               .ToList();
            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
            }
            return sb.ToString();

            //var employees = context.Employees
            //.Where(e => EF.Functions.Like(e.FirstName, "Sa%")) // Case-insensitive comparison
            //.OrderBy(e => e.FirstName)
            //.ThenBy(e => e.LastName)
            //.Select(e => new
            //{
            //    FirstName = e.FirstName,
            //    LastName = e.LastName,
            //    JobTitle = e.JobTitle,
            //    Salary = Math.Round(e.Salary, 2)
            //})
            //.ToList();

            //foreach (var e in employees)
            //{
            //    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
            //}
            //return sb.ToString();
        }

        //Problem 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Projects.Find(2);
            context.Projects.Remove(employees);

            context.SaveChanges();

                var listEmployees = context
                .Projects
                .Select(e => new
                {
                    e.Name
                });
            foreach (var e in listEmployees)
            {
                sb.AppendLine($"{e.Name}");
            }
            return sb.ToString();
        }
        //Problem 15
        public static string RemoveTown(SoftUniContext context)
        {
            Town townToDel = context
                .Towns
                .First(t => t.Name == "Seattle");

            IQueryable<Address> addressesToDel = context
                .Addresses
                .Where(a => a.TownId == townToDel.TownId);
            int addressesCount = addressesToDel.Count();

            IQueryable<Employee> employeesOnDeletedAddresses = context
                .Employees
                .Where(e => addressesToDel
                .Any(a => a.AddressId == e.AddressId));

            foreach (Employee e in employeesOnDeletedAddresses)
            {
                e.AddressId = null;
            }

            foreach (Address address in addressesToDel)
            {
                context
                    .Addresses
                    .Remove(address);
            }
            context.Towns.Remove(townToDel);
            context.SaveChanges ();
            return $"{addressesCount} addresses in {townToDel} were deleted";
        }

    }
}