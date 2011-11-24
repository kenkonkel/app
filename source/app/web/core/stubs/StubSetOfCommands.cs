using System.Collections;
using System.Collections.Generic;
using app.web.application;
using app.web.application.stubs;

namespace app.web.core.stubs
{
  public class StubSetOfCommands : IEnumerable<IProcessOneRequest>
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerator<IProcessOneRequest> GetEnumerator()
    {
		yield return new RequestCommand(x => x.IsForSubDepartments, new ViewReport<IEnumerable<Department>>(new GetDepartmentsInDepartment()));
		yield return new RequestCommand(x => x.IsForMainDepartments, new ViewReport<IEnumerable<Department>>(new GetTheMainDepartments()));
    }
  }

  public class GetDepartmentsInDepartment : IRunQuery<IEnumerable<Department>>
  {
    public IEnumerable<Department> run_using(IContainRequestInformation request)
    {
      return Stub.with<StubFindInformationInTheStore>().get_the_departments_in_a_department(request.map<Department>());
    }
  }

  public class GetTheMainDepartments : IRunQuery<IEnumerable<Department>>
  {
    public IEnumerable<Department> run_using(IContainRequestInformation request)
    {
      return Stub.with<StubFindInformationInTheStore>().get_the_main_departments();
    }
  }
}