﻿using System.Collections.Generic;
using System.Linq;

namespace app.web.application.stubs
{
    public class StubFindInformationInTheStore : IFindInformationInTheStore
    {
        public IEnumerable<Department> get_the_main_departments()
        {
			return Enumerable.Range(1, 100).Select(x => new Department { name = x.ToString("Department 0"), has_departments = bool.Parse((x % 2).ToString()), has_products = !bool.Parse((x % 2).ToString()) });
        }

        public IEnumerable<Department> get_the_departments_in_a_department(Department department)
        {
            return Enumerable.Range(1, 10).Select( x => new Department {name = x.ToString("Sub Department 0"), has_products = true, has_departments = false});
        }

    	public IEnumerable<Product> get_the_products_in_a_department(Department map)
    	{
			return Enumerable.Range(1, 10).Select(x => new Product() { name = x.ToString("Product 0") });
    	}
    }
}