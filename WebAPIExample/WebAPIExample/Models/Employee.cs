using System;
using System.Collections.Generic;

namespace WebAPIExample.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public decimal Basic { get; set; }

    public short DeptNo { get; set; }

    public virtual Department DeptNoNavigation { get; set; } = null!;
}
