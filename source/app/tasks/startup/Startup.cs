using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Compilation;
using app.utility.containers;
using app.utility.containers.basic;
using app.web.core;
using app.web.core.aspnet;
using app.web.core.aspnet.stubs;
using app.web.core.stubs;

namespace app.tasks.startup
{
	public class Startup
	{
		static IDictionary<Type, ICreateADependency> dependencies;
		static IFetchDependencies container;

		public static void run()
		{
			dependencies = new Dictionary<Type, ICreateADependency>();
			container = new DependencyContainer(dependencies);
			ContainerFacadeResolution facade_resolution = () => container;
			Container.facade_resolution = facade_resolution;

			wire_everything_up();
		}

		static void wire_everything_up()
		{
			register<IBuildRequestMatchers, RequestMatchBuilder>();
			register<IFindPathsToViews, StubPathRegistry>();
			register<ICreateAResponse, PageFactory>();
			register<IDisplayReportModels, WebResponseEngine>();
			register<IFindCommands, CommandRegistry>();
			register<ICreateRequests, StubRequestFactory>();
			register<IProcessOneRequest>(() => Stub.with<StubMissingCommand>());
			register<CurrentContextResolver>(() => HttpContext.Current);
			register<IEnumerable<IProcessOneRequest>>(() => Stub.with<StubSetOfCommands>());
			
			register<IProcessRequests>(() => new FrontController(Container.fetch.an<IFindCommands>()));
		}

		static void register<Contract, Implementation>()
		{
			dependencies.Add(typeof(Contract), new AutomaticDependencyFactory(container, new GreedyConstructorSelectionStrategy(), typeof(Implementation)));
		}
		static void register<RegisteredType>(Func<object> factory_method)
		{
			dependencies.Add(typeof(RegisteredType), new SimpleFactory(factory_method));
		}
	}
}