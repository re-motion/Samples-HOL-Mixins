// Copyright (c) 2011 rubicon informationstechnologie gmbh
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Reflection;

namespace EqualsShowCase.StrategyImplementation
{

public interface IEqualsStrategy
{
  bool CustomEquals(object first, object second);
  int CustomGetHashCode (object obj);
}

public class EquatableByValueStrategy<T> : IEqualsStrategy
{
  static FieldInfo[] s_targetFields = typeof(T).GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

  public bool CustomEquals (object first, object second)
  {
    if ((first == null) && (second == null))
      return true;

    if ((first == null) || (second == null))
      return false;

    if (first.GetType () != second.GetType ())
      return false;

    for (int i = 0; i < s_targetFields.Length; i++)
    {
      object thisFieldValue = s_targetFields[i].GetValue (first);
      object otherFieldValue = s_targetFields[i].GetValue (second);

      if (!Equals (thisFieldValue, otherFieldValue))
        return false;
    }

    return true;
  }

  public int CustomGetHashCode (object obj)
  {
    int i = 0;
    foreach (FieldInfo f in s_targetFields)
      i ^= f.GetValue (obj).GetHashCode ();
    return i;
  }
}

public class Address : IEquatable<Address>
{
  private IEqualsStrategy _equalsStrategy;

  public string City;
  public string ZipCode;
  public string Country;

  public Address () : this (new EquatableByValueStrategy<Address>())
  {}

  public Address (IEqualsStrategy equalsStrategy)
  {
    _equalsStrategy = equalsStrategy;
  }

  public override bool Equals (object obj)
  {
    return Equals (obj as Address);
  }

  public bool Equals (Address other)
  {
    return _equalsStrategy.CustomEquals(this, other);
  }

  public override int GetHashCode ()
  {
    return _equalsStrategy.CustomGetHashCode (this);
  }
}

public class StreetAddress : Address
{
  public StreetAddress () : this (new EquatableByValueStrategy<StreetAddress>()) 
  {}

  public StreetAddress (IEqualsStrategy equalsStrategy ) : base (equalsStrategy)
  {}

  public string Street;
  public string StreetNumber;
}

}
