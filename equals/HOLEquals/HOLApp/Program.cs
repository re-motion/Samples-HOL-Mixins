using System;
using Remotion.Mixins;
using Remotion.Reflection;

namespace HOLApp
{
  class Program
  {
    static void Main (string[] args)
    {
      HOLApp.BasicImplementation.Address address1 = new HOLApp.BasicImplementation.Address ();
      HOLApp.BasicImplementation.Address address2 = new HOLApp.BasicImplementation.Address ();
      Console.WriteLine ("Basic Implementation: Both instances have the same values: {0}", Equals (address1, address2));

      HolApp.BaseClassEquals.Address address3 = new HolApp.BaseClassEquals.Address ();
      HolApp.BaseClassEquals.Address address4 = new HolApp.BaseClassEquals.Address ();
      Console.WriteLine ("Inheritance Implementation: Both instances have the same values: {0}", Equals (address3, address4));

      HolApp.BaseClassEquals.StreetAddress streetaddress1 = new HolApp.BaseClassEquals.StreetAddress ();
      HolApp.BaseClassEquals.StreetAddress streetaddress2 = new HolApp.BaseClassEquals.StreetAddress ();
      streetaddress1.Street = "Test";
      streetaddress2.Street = "Test2";
      Console.WriteLine ("(Value not as expected)Inheritance Implementation StreetAddress: Both instances have the same values: {0}", Equals (streetaddress1, streetaddress2));


      HOLApp.UtilityImplementation.StreetAddress streetaddress3 = new HOLApp.UtilityImplementation.StreetAddress ();
      HOLApp.UtilityImplementation.StreetAddress streetaddress4 = new HOLApp.UtilityImplementation.StreetAddress ();
      streetaddress3.Street = "Test";
      streetaddress4.Street = "Test2";
      Console.WriteLine ("Utility Implementation StreetAddress: Both instances have the same values: {0}", Equals (streetaddress3, streetaddress4));


      HOLApp.MixinImplementation.StreetAddress mixedAddress1 = ObjectFactory.Create<HOLApp.MixinImplementation.StreetAddress> (ParamList.Create (1010, "Wien", "Stephansplatz", "1"));
      HOLApp.MixinImplementation.StreetAddress mixedAddress2 = ObjectFactory.Create<HOLApp.MixinImplementation.StreetAddress> (ParamList.Empty);
      Console.WriteLine ("Mixed Implementation StreetAddress: Both instances have the same values: {0}", Equals (mixedAddress1, mixedAddress2));

      Console.ReadKey();
    }
  }
}
