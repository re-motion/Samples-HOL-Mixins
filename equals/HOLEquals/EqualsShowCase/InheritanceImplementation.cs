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

namespace EqualsShowCase.InheritanceImplementation
{

public class EquatableByValues<T> : IEquatable<T>
    where T : class
{
    private static readonly FieldInfo[] s_targetFields = typeof (T).GetFields (
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

  public bool Equals (T other)
  {
    if (other == null)
      return false;

    if (GetType () != other.GetType ())
      return false;

    for (int i = 0; i < s_targetFields.Length; i++)
    {
      object thisFieldValue = s_targetFields[i].GetValue (this);
      object otherFieldValue = s_targetFields[i].GetValue (other);

      if (!Equals (thisFieldValue, otherFieldValue))
        return false;
    }

    return true;
  }

  public override bool Equals (object obj)
  {
    return Equals (obj as T);
  }

  public override int GetHashCode ()
  {
    int i = 0;
    foreach (FieldInfo f in s_targetFields)
      i ^= f.GetValue (this).GetHashCode ();
    return i;
  }
}

//// Problem 1: can't have more than one base class like this, e.g. 
//// public class Address : EquatableByValues<Address>, DisposableBase
public class Address : EquatableByValues<Address>
{
  public string City;
  public string ZipCode;
  public string Country;
}

//// Problem 2: can't derive class hierarchies from base class
//// public class StreetAddress : Address // implements IEquatable<Address> instead of IEquatable<StreetAddress>!
public class StreetAddress : Address
{
  public string Street;
  public string StreetNumber;
}

}