using System;

namespace EqualsShowCase.BasicImplementation
{
  public class Address : IEquatable<Address>
  {
    public string Street;
    public string StreetNumber;
    public string City;
    public string ZipCode;
    public string Country;

    public override bool Equals (object obj)
    {
      return Equals (obj as Address);
    }

    public bool Equals (Address other)
    {
      if (other == null)
        return false;

      if (GetType () != other.GetType ())
        return false;

      return Street == other.Street && StreetNumber == other.StreetNumber &&
             City == other.City && ZipCode == other.ZipCode && Country == other.Country;
    }

    public override int GetHashCode ()
    {
      return Street.GetHashCode () ^ StreetNumber.GetHashCode () ^
             City.GetHashCode () ^ ZipCode.GetHashCode () ^ Country.GetHashCode ();
    }
  }
}