﻿using System;
using System.Linq;
using System.Reflection;
using Remotion.Mixins;

namespace HOLApp.MixinImplementation
{
  public class EquatableByValuesMixin<[BindToTargetType]T> : Mixin<T>, IEquatable<T>
     where T : class
  {
    private static readonly FieldInfo[] s_targetFields = typeof (T).GetFields (
                  BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    public bool Equals (T other)
    {
      if (other == null)
        return false;

      if (Target.GetType () != other.GetType ())
        return false;

      for (int i = 0; i < s_targetFields.Length; i++)
      {
        object thisFieldValue = s_targetFields[i].GetValue (Target);
        object otherFieldValue = s_targetFields[i].GetValue (other);

        if (!Equals (thisFieldValue, otherFieldValue))
          return false;
      }

      return true;
    }

    [OverrideTarget]
    public new bool Equals (object other)
    {
      return ((IEquatable<T>)this).Equals (other as T);
    }

    [OverrideTarget]
    public new int GetHashCode ()
    {
      return s_targetFields.Aggregate (0, (current, t) => current ^ t.GetValue (Target).GetHashCode ());
    }
  }

  [EquatableByValuesAttribute]
  public abstract class Address
  {
    protected Address ()
    {
    }

    protected Address (int zipCode, string city)
    {
      ZipCode = zipCode;
      City = city;
    }

    protected Address (int zipCode, City city)
    {
      ZipCode = zipCode;
      City = city.Name; // null-check missing
    }

    public int ZipCode;
    public string City;
  }

  public class City
  {
    public string Name;
  }

  public class StreetAddress : Address
  {
    public StreetAddress (int zipCode, string city, string street, string streetNumber)
      : base (zipCode, city)
    {
      Street = street;
      StreetNumber = streetNumber;
    }

    public string Street;
    public string StreetNumber;
  }

  public class POBoxAddress : Address
  {
    public POBoxAddress (int zipCode, string city, int poBox)
      : base (zipCode, city)
    {
      POBox = poBox;
    }

    public int POBox;
  }

  public class EquatableByValuesAttribute : UsesAttribute
  {
    public EquatableByValuesAttribute ()
      : base (typeof (EquatableByValuesMixin<>))
    {
    }
  }
}