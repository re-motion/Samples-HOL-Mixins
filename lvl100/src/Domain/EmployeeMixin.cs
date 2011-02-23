using System;
using Remotion.Mixins;

namespace Domain
{
  public interface IEmployee
  {
    int Salary { get; set; }
    DateTime? HireDate { get; set; }
  }

  [Extends(typeof(Person))]
  public class EmployeeMixin : IEmployee
  {
    public int Salary { get; set;  }
    public DateTime? HireDate { get; set;}
    public EmployeeMixin()
    {
      // set default values
      Salary = 1000;
      HireDate = DateTime.Now;
    }
  }
}
