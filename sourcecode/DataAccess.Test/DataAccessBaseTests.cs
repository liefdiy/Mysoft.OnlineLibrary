using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Mysoft.Platform.DataAccess.Test
{
	[TestFixture]
	public class DataAccessBaseTests
	{
		public DataAccessBaseTests()
		{
			RepositoryManager.Init();
		}
	}
}
