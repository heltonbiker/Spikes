using Jot;
using Jot.Storage;

namespace HelloJotConfig
{
	public static class ConfigurationService
	{
		public static Tracker Tracker { get; } 
			= new Tracker(new JsonFileStore(@"C:\Miotec\"));

		static ConfigurationService()
		{
			Tracker.Configure<ViewModel>()
				.Properties(vm => new {vm.Value1, vm.Value2})
				.PersistOn(nameof(ViewModel.PropertyChanged));
		}
	}
}