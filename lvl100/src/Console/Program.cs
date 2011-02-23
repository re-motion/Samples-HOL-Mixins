using Domain;
using Remotion.Reflection;
using Remotion.Mixins;

namespace Console
{
  class Program
  {
    static void Main(string[] args)
    {
      var employee = (IEmployee)ObjectFactory.Create<Person>(ParamList.Create("Max", "Mustermann"));
      System.Console.WriteLine("Fullname: {0}", ((Person)employee).FirstName + " " + ((Person)employee).LastName);
      System.Console.WriteLine("Salary: {0}", employee.Salary);
      
      System.Console.ReadKey();
    }
  }
}
