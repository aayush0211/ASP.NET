namespace Employee_Management_System.Models
{
    public interface Interface
    {
       static abstract void AddEmployee(Employee employee);
        static abstract List<Employee> GetAll();

        static abstract String UpdateEmployee(Employee employee);

        static abstract String DeleteEmployee(int id);
    }
}
