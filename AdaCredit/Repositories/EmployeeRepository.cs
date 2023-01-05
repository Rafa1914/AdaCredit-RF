using AdaCredit.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace AdaCredit.Repositories
{
    public class EmployeeRepository
    {
        private List<Employee> _employees = new List<Employee>();
        private string employeePath = Path.Combine(Environment.CurrentDirectory, @"Employees.txt");
        private bool isEmployeeEmpty = false;

        public EmployeeRepository()
        {
            //Verificação da Existência do Arquivo:
            if (File.Exists(employeePath))
            {
                //Configuração do CsvHelper:
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };

                //Leitura do Arquivo
                using var reader = new StreamReader(employeePath);
                using var csv = new CsvParser(reader, config);

                if (!csv.Read())
                    return;

                while (csv.Read())
                {
                    _employees.Add(new Employee(csv.Record[0], csv.Record[1], csv.Record[2], csv.Record[3], bool.Parse(csv.Record[4]), Convert.ToDateTime(csv.Record[5])));
                }
                if(_employees.Count == 0)
                {
                    isEmployeeEmpty = true;
                    var employeeStandard = new Employee("user", "pass", true);
                    _employees.Add(employeeStandard);
                }

            }
            else
            {
                using var writer = new StreamWriter(employeePath);
                WriteHeader(writer);
                isEmployeeEmpty= true;
                var employeeStandard = new Employee("user", "pass", true);
                _employees.Add(employeeStandard);
            }
        }

        private void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("Username,Password,Salt,Hashed_Password,Activate,Last_Login");
        }

        public bool AddEmployee(Employee employee)
        {
            
            if (_employees.Any(x => x.Username.Equals(employee.Username)))
            {
                Console.WriteLine("Usuário já cadastrado!");
                Console.ReadKey();

                return false;
            }

            var entity = new Employee(employee.Username, employee.Password, employee.Activate);

            _employees.Add(entity);

            return true;
        }

        public void Save()
        {
            if(isEmployeeEmpty)
            {
                _employees.RemoveAt(0);
            }
            using var write = new StreamWriter(employeePath);
            WriteHeader(write);
            foreach (var employee in _employees)
            {
                write.WriteLine(employee.ToString());
            }
        }

        public Employee FindEmployee(string? user)
        {
            return _employees.FirstOrDefault(x=>x.Username.Equals(user));
        }

        public void changeEmployee(Employee employee, string user)
        {        
            var index = _employees.FindIndex(x => x.Username.Equals(user));
            _employees[index] = employee;            
            Save();        
        }

        public void GenerateFakeEmployees(int numberOfFakes)
        {

            var employeeFaker = new Faker<Employee>("pt_BR")
                .RuleFor(e => e.Username, f => f.Name.LastName()+f.Name.FirstName())
                .RuleFor(e => e.Password, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(e => e.Activate, f => f.PickRandomParam(new bool[] { true, false }));

            foreach (var employee in employeeFaker.Generate(numberOfFakes))
            {
                AddEmployee(employee);
            }

            Save();
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }
    }
}
