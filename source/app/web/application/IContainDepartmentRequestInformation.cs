using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.web.core;

namespace app.web.application
{
	public interface IContainDepartmentRequestInformation : IContainRequestInformation
	{
		Department Department { get; set; }
	}
}
