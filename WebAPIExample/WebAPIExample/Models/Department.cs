using System;
using System.Collections.Generic;

namespace WebAPIExample.Models;

public partial class Department
{
    public short DeptNo { get; set; }

    public string DeptName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
