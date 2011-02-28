using System;
using System.Reflection;

namespace EqualsShowCase.UtilityImplementation
{
  public class EquateUtility
  {
    public static bool EquateByValues (object a, object b)
    {
      if ((a == null) && (b == null))
        return true;

      if ((a == null) || (b == null))
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
    public static int GetHashCode (object a)
    {
      int i = 0;
      foreach (FieldInfo f in a.GetType().GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        i = i ^ f.GetHashCode ();
      return i;
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
      return EqualsShowCase.UtilityImplementation.EquateUtility.EquateByValues (this, other);
    }

    public override int GetHashCode ()
    {
      return EqualsShowCase.UtilityImplementation.EquateUtility.GetHashCode (this);
    }
  }

  public class StreetAddress : Address
  {
    public string Street;
    public string StreetNumber;
  }
}