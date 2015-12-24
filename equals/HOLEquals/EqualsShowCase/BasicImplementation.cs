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