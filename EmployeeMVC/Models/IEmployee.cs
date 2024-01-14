namespace EmployeeMVC.Models
{
    public interface IEmployee
    {
        static abstract void AddEmployee(Employee employee);
        static abstract List<Employee> getAllEmployees();

        static abstract String UpdateEmployee(Employee employee);
        static abstract String DeleteEmployee(String name);


    }
}
