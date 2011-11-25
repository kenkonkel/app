using System;
using System.Collections.Generic;

namespace app.utility.containers.basic
{
  public class AutomaticDependencyFactory : ICreateADependency
  {
  	IFetchDependencies fetcher;
  	IPickTheConstructorForAType iPicker;
  	Type my_type;

  	public AutomaticDependencyFactory(IFetchDependencies fetcher, IPickTheConstructorForAType iPicker, Type myType)
  	{
  		this.fetcher = fetcher;
  		this.iPicker = iPicker;
  		my_type = myType;
  	}

  	public object create()
  	{
  		var ctor = iPicker.get_applicable_constructor_on(my_type);
  		List<object> parms = new List<object>();

		// for each parameter in the constructor, fetch its dependency.
		foreach (var param in ctor.GetParameters())
		{
			var dependency = fetcher.an(param.ParameterType);
			parms.Add(dependency);
			// somehow go back and set the value on myType...??
		}

  		return ctor.Invoke(parms.ToArray());
  	}

  }
}