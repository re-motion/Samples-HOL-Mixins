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
using EqualsShowCase.MixinImplementation;
using Remotion.Mixins;
using Remotion.Reflection;

namespace HOLApp
{
  class Program
  {
    static void Main (string[] args)
    {
      BasicImplementation();
      InheritanceImplementation();
      UtilityImplementation();
      StrategyImplementation();
      MixinImplementation();

      Console.ReadKey();
    }

    static void BasicImplementation ()
    {
      var wien1 = new EqualsShowCase.BasicImplementation.Address { City = "Wien" };
      var wien2 = new EqualsShowCase.BasicImplementation.Address { City = "Wien" };
      var berlin = new EqualsShowCase.BasicImplementation.Address { City = "Berlin" };

      Console.WriteLine ("Basic Implementation: Wien = Wien: {0}", Equals (wien1, wien2));
      Console.WriteLine ("Basic Implementation: Wien = Berlin: {0}", Equals (wien1, berlin));
    }

    static void InheritanceImplementation ()
    {
      var wien1 = new EqualsShowCase.InheritanceImplementation.Address { City = "Wien" };
      var wien2 = new EqualsShowCase.InheritanceImplementation.Address { City = "Wien" };;
      var berlin = new EqualsShowCase.InheritanceImplementation.Address { City = "Berlin" };
      Console.WriteLine ("Inheritance Implementation: Wien = Wien: {0}", Equals (wien1, wien2));
      Console.WriteLine ("Inheritance Implementation: Wien = Berlin: {0}", Equals (wien1, berlin));

      var kaerntnerStrasse1 = new EqualsShowCase.InheritanceImplementation.StreetAddress { City = "Wien", Street = "Kärntner Straße" };
      var kaerntnerStrasse2 = new EqualsShowCase.InheritanceImplementation.StreetAddress { City = "Wien", Street = "Kärntner Straße" };
      var amGraben = new EqualsShowCase.InheritanceImplementation.StreetAddress { City = "Wien", Street = "Am Graben"  };
      Console.WriteLine ("Inheritance Implementation: Kärntner Straße = Kärntner Straße: {0}", Equals (kaerntnerStrasse1, kaerntnerStrasse2));
      Console.WriteLine ("Inheritance Implementation: Kärntner Straße = Am Graben: {0} (should be false!)", Equals (kaerntnerStrasse1, amGraben));
    }

    static void UtilityImplementation ()
    {
      var wien1 = new EqualsShowCase.UtilityImplementation.StreetAddress { City = "Wien" };
      var wien2 = new EqualsShowCase.UtilityImplementation.StreetAddress { City = "Wien" };
      var berlin = new EqualsShowCase.UtilityImplementation.StreetAddress { City = "Berlin" };
      Console.WriteLine ("Utility Class Implementation: Wien = Wien: {0}", Equals (wien1, wien2));
      Console.WriteLine ("Utility Class Implementation: Wien = Berlin: {0}", Equals (wien1, berlin));
    }

    private static void StrategyImplementation ()
    {
      var wien1 = new EqualsShowCase.StrategyImplementation.StreetAddress { City = "Wien" };
      var wien2 = new EqualsShowCase.StrategyImplementation.StreetAddress { City = "Wien" };
      var berlin = new EqualsShowCase.StrategyImplementation.StreetAddress { City = "Berlin" };

      Console.WriteLine ("Strategy Implementation: Wien = Wien: {0}", Equals (wien1, wien2));
      Console.WriteLine ("Strategy Implementation: Wien = Berlin: {0}", Equals (wien1, berlin));
    }
    
    static void MixinImplementation ()
    {
      var wien1 = StreetAddress.NewObject (1010, "Wien", "Stephansplatz", "1");
      var wien2 = StreetAddress.NewObject (1010, "Wien", "Stephansplatz", "1");
      var berlin = StreetAddress.NewObject (11011, "Berlin", "Pariser Platz", "1");

      Console.WriteLine ("Mixin Implementation: Wien = Wien: {0}", Equals (wien1, wien2));
      Console.WriteLine ("Mixin Implementation: Wien = Berlin: {0}", Equals (wien1, berlin));

      // show that the correct constructor overload is called even if the type of the city parameter cannot 
      // be detected at runtime (could be string or City). Would result in an AmbiguityException otherwise
      var nullCitymixedAddress3 = StreetAddress.NewObject (1010, null, "Stephansplatz", "1");
      Console.WriteLine ("Mixin Implementation: Wien = null: {0}", Equals (wien1, null));
    }
  }
}
