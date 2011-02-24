using System;
using System.Reflection;

namespace EqualsShowCase.UtilityImplementation
{
  public class CompareUtility
  {
    public static bool FieldEquals (object a, object b)
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
  }

  public class Address : IEquatable<Address>
  {
    protected delegate bool EqualCheck (object a, object b);
    public string City;
    public string ZipCode;
    public string Country;

    protected virtual EqualCheck GetComparison ()
    {
      return EqualsShowCase.UtilityImplementation.CompareUtility.FieldEquals;
    }

    public override bool Equals (object obj)
    {
      return Equals (obj as Address);
    }

    public bool Equals (Address other)
    {
      return GetComparison () (this, other);
    }

    public override int GetHashCode ()
    {
      return City.GetHashCode () ^ ZipCode.GetHashCode () ^ Country.GetHashCode ();
    }
  }

  public class StreetAddress : Address
  {
    public string Street;
    public string StreetNumber;
  }
}

