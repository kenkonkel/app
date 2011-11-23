using System.Collections.Generic;
using Machine.Specifications;
using app.web.application;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
  [Subject(typeof(ViewTheDepartmentsInADepartment))]
	public class ViewTheDepartmentsInADepartmentSpecs
  {
    public abstract class concern : Observes<IImplementAUseCase,
									  ViewTheDepartmentsInADepartment>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        
      	department = new Department();
		departments = new List<Department> {new Department()};

		request = fake.an<IContainRequestInformation>();
      	request.setup(x => x.Department).Return(department);
		  
        department_finder = depends.on<IDepartmentFinder>();
        response_engine = depends.on<IDisplayReportModels>();

		department_finder.setup(x => x.get_the_departments_for_department(department)).Return(departments);
      };

      Because b = () =>
        sut.process(request);

      It should_get_the_departments_in_the_department = () =>
		department_finder.received(x => x.get_the_departments_for_department(department));

      It should_display_the_report_model = () =>
        response_engine.received(x => x.display(departments));

      static IDepartmentFinder department_finder;
      static IContainRequestInformation request;
      static IDisplayReportModels response_engine;
    	private static Department department;
    	private static List<Department> departments;
    }
  }
}