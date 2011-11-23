using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.web.core;

namespace app.web.application
{
	public class ViewTheDepartmentsInADepartment : IImplementAUseCase
	{
		IDepartmentFinder department_finder;
		IDisplayReportModels response_gateway;

		public ViewTheDepartmentsInADepartment(IDepartmentFinder departmentFinder, IDisplayReportModels responseGateway)
		{
			department_finder = departmentFinder;
			response_gateway = responseGateway;
		}

		public void process(IContainRequestInformation request)
		{
			response_gateway.display(department_finder.get_the_departments_for_department(request.Department));
		}
	}
}
