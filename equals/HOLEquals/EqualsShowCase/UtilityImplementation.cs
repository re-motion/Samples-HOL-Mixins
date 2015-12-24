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