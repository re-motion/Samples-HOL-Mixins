using System;
using EqualsShowCase.MixinImplementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace HOLApp
{
  class Program
  {
    static void Main (string[] args)
    {
      // BasicImplementation();
      //InheritanceImplementation();
      // UtilityImplementation();
      StrategyImplementation();
      // MixinImplementation();

      Console.ReadKey();
    }

    private static void StrategyImplementation ()
    {
      EqualsShowCase.StrategyImplementation.StreetAddress streetaddress3 = new EqualsShowCase.StrategyImplementation.StreetAddress ();
      EqualsShowCase.StrategyImplementation.StreetAddress streetaddress4 = new EqualsShowCase.StrategyImplementation.StreetAddress ();
      streetaddress3.Street = "Test";
      streetaddress4.Street = "Test2";
      Console.WriteLine ("Strategy Implementation StreetAddress: Both instances have the same values: {0}", Equals (streetaddress3, streetaddress4));
    }

    static void BasicImplementation ()
    {
      EqualsShowCase.BasicImplementation.Address address1 = new EqualsShowCase.BasicImplementation.Address ();
      EqualsShowCase.BasicImplementation.Address address2 = new EqualsShowCase.BasicImplementation.Address ();

      Console.WriteLine ("Basic Implementation: Both instances have the same values: {0}", Equals (address1, address2));
    }

    static void InheritanceImplementation ()
    {
      EqualsShowCase.InheritanceImplementation.Address address3 = new EqualsShowCase.InheritanceImplementation.Address ();
      EqualsShowCase.InheritanceImplementation.Address address4 = new EqualsShowCase.InheritanceImplementation.Address ();
      Console.WriteLine ("Inheritance Implementation: Both instances have the same values: {0}", Equals (address3, address4));

      EqualsShowCase.InheritanceImplementation.StreetAddress streetaddress1 = new EqualsShowCase.InheritanceImplementation.StreetAddress ();
      EqualsShowCase.InheritanceImplementation.StreetAddress streetaddress2 = new EqualsShowCase.InheritanceImplementation.StreetAddress ();

      streetaddress1.Street = "Test";
      streetaddress2.Street = "Test2";

      // Value is not as might be expected
      Console.WriteLine ("(Value not as expected)Inheritance Implementation StreetAddress: Both instances have the same values: {0}", Equals (streetaddress1, streetaddress2));
    }

    static void UtilityImplementation ()
    {
      EqualsShowCase.UtilityImplementation.StreetAddress streetaddress3 = new EqualsShowCase.UtilityImplementation.StreetAddress ();
      EqualsShowCase.UtilityImplementation.StreetAddress streetaddress4 = new EqualsShowCase.UtilityImplementation.StreetAddress ();
      streetaddress3.Street = "Test";
      streetaddress4.Street = "Test2";
      Console.WriteLine ("Utility Implementation StreetAddress: Both instances have the same values: {0}", Equals (streetaddress3, streetaddress4));
    }

    static void MixinImplementation ()
    {
      EqualsShowCase.MixinImplementation.StreetAddress mixedAddress1 = ObjectFactory.Create<EqualsShowCase.MixinImplementation.StreetAddress> (ParamList.Create (1010, "Wien", "Stephansplatz", "1"));
      EqualsShowCase.MixinImplementation.StreetAddress mixedAddress2 = ObjectFactory.Create<EqualsShowCase.MixinImplementation.StreetAddress>(ParamList.Create(1010, "Wien", "Stephansplatz", "1"));
      Console.WriteLine ("Mixed Implementation StreetAddress: Both instances have the same values: {0}", Equals (mixedAddress1, mixedAddress2));

      EqualsShowCase.MixinImplementation.StreetAddress mixedAddress3 = ObjectFactory.Create<EqualsShowCase.MixinImplementation.StreetAddress> (ParamList.Create (1010, default (string), "Stephansplatz", "1"));
      EqualsShowCase.MixinImplementation.StreetAddress mixedAddress4 = ObjectFactory.Create<EqualsShowCase.MixinImplementation.StreetAddress> (ParamList.Create (1010, new City().Name = "Wien", "Stephansplatz", "1"));
      Console.WriteLine ("Mixed Implementation StreetAddress: Both instances have the same values: {0}", Equals (mixedAddress1, mixedAddress3));
      Console.WriteLine ("Mixed Implementation StreetAddress: Both instances have the same values: {0}", Equals (mixedAddress1, mixedAddress4));
    }
  }
}
