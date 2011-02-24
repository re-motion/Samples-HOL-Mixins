using System;
using System.Reflection;

namespace HOLApp.UtilityImplementation
{
  public class ComparyUtility
  {
    public static bool FieldEquals (object a, object b)
    {
      if ((a == null) != (b == null))
        return false;

      if (a.GetType () != b.GetType ())
        return false;

      FieldInfo[] s_targetFields = a.GetType().GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      
      for (int i = 0; i < s_targetFields.Length; i++)
      {
        object thisFieldValue = s_targetFields[i].GetValue (a);
        object otherFieldValue = s_targetFields[i].GetValue (b);

        if (!Equals (thisFieldValue, otherFieldValue))
          return false;
      }

      return true;
    }
  }

    public class Address : IEquatable<Address>
    {
      public string City;
      public string ZipCode;
      public string Country;

      public override bool Equals (object obj)
      {
        return Equals (obj as Address);
      }

      public bool Equals (Address other)
      {
        return HOLApp.UtilityImplementation.ComparyUtility.FieldEquals(this, other);
     }

      public override int GetHashCode ()
      {
        throw new NotImplementedException();
      }
    }

    public class StreetAddress : Address
    {
      public string Street;
      public string StreetNumber;
    }
  }

