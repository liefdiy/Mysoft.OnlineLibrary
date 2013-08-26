using Mysoft.Platform.DataAccess;

namespace Mysoft.Platform.OnlineLibrary
{
	public class Bootstrapper
	{
		public static void Start()
		{
			//initial database
			RepositoryManager.Init();
		}
	}
}