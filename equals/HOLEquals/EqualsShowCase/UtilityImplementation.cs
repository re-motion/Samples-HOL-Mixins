using System;
using System.Reflection;

namespace EqualsShowCase.UtilityImplementation
{
  public class EquateUtility
  {
    public static bool EqualsByValues (object a, object b)
    {
      #region //...
      if ((a == null) && (b == null))
        return true;

      if ((a == null) || (b == null))
        return false;

      if (a.GetType () != b.GetType ())
        return false;

      FieldInfo[] targetFields = a.GetType().GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (int i = 0; i < targetFields.Length; i++)
      {
        object thisFieldValue = targetFields[i].GetValue (a);
        object otherFieldValue = targetFields[i].GetValue (b);

        if (!Equals (thisFieldValue, otherFieldValue))
          return false;
      }

      return true;
      #endregion
    }

    public static int GetHashCodeByValues (object obj)
    {
      #region //...
      FieldInfo[] targetFields = obj.GetType().GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      int j = 0;
      foreach (var f in targetFields)
      {
        j ^= f.GetValue(obj).GetHashCode();
      }
      return j;
      #endregion
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
      return EquateUtility.EqualsByValues (this, other);
    }

    public override int GetHashCode ()
    {
      return EquateUtility.GetHashCodeByValues (this);
    }
  }

  public class StreetAddress : Address
  {
    public string Street;
    public string StreetNumber;
  }
}