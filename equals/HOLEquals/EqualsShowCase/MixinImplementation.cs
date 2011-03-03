using System;
using System.Reflection;
using Remotion.Mixins;
using Remotion.Reflection;

namespace EqualsShowCase.MixinImplementation
{

// TODO: use this in print version: public class EquatableByValuesMixin<T> : Mixin<T>, IEquatable<T>
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

// TODO: remove in print version
public class EquatableByValues : UsesAttribute
{
  public EquatableByValues ()
    : base (typeof (EquatableByValuesMixin<>))
  {
  }
}

  
// TODO: use this in print version: [Uses(typeof (EquatableByValuesMixin<Address>))]
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

  // TODO: remove in print version
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

// TODO: remove in print version
public class City
{
  public string Name;
}

public class StreetAddress : Address
{
  // TODO: remove in print version
  public static StreetAddress NewObject (int zipCode, string city, string street, string streetNumber)
  {
    return ObjectFactory.Create<StreetAddress> (true, ParamList.Create (zipCode, city, street, streetNumber));
  }

  // TODO: public in print version
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
  // TODO: remove in print version
  public static POBoxAddress NewObject (int zipCode, string city, int poBox)
  {
    return ObjectFactory.Create<POBoxAddress> (true, ParamList.Create (zipCode, city, poBox));
  }

  // TODO: public in print version
  protected POBoxAddress (int zipCode, string city, int poBox)
    : base (zipCode, city)
  {
    POBox = poBox;
  }

  public int POBox;
}

// todo: remove in print version
[EquatableByValues]
public class PhoneNumber
{
  public int CountryCode;
  public int AreaCode;
  public int Number;
  public int? Extension;
}

}
