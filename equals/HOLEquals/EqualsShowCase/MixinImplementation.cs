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
using Remotion.Mixins;
using Remotion.Reflection;

namespace EqualsShowCase.MixinImplementation
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
    return Equals (other as T);
  }

  [OverrideTarget]
  public new int GetHashCode ()
  {
    int i = 0;
    foreach (FieldInfo f in s_targetFields)
      i ^= f.GetValue (Target).GetHashCode ();
    return i;
  }
}

public class EquatableByValues : UsesAttribute
{
  public EquatableByValues ()
    : base (typeof (EquatableByValuesMixin<>))
  {
  }
}


[EquatableByValues]
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

  // This alternative constructor shows the problem of runtime overloads. Assuming you create an Address using the statements
  //   string city;
  //   ObjectFactory.Create (1010, city)
  // and the Create method would accept a params object[], the implementation would pick the correct constructor overload only 
  // if city != null!
  // ParamList.Create is type-safe in this regard and will always pick the constructor overload using the static types of
  // the arguments (in this case, int and string)
  protected Address (int zipCode, City city)
  {
    ZipCode = zipCode;
    City = city != null ? city.Name : null;
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
  public static StreetAddress NewObject (int zipCode, string city, string street, string streetNumber)
  {
    return ObjectFactory.Create<StreetAddress> (true, ParamList.Create (zipCode, city, street, streetNumber));
  }

  protected StreetAddress (int zipCode, string city, string street, string streetNumber)
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
  public static POBoxAddress NewObject (int zipCode, string city, int poBox)
  {
    return ObjectFactory.Create<POBoxAddress> (true, ParamList.Create (zipCode, city, poBox));
  }

  protected POBoxAddress (int zipCode, string city, int poBox)
    : base (zipCode, city)
  {
    POBox = poBox;
  }

  public int POBox;
}

[EquatableByValues]
public class PhoneNumber
{
  public int CountryCode;
  public int AreaCode;
  public int Number;
  public int? Extension;
}

}
