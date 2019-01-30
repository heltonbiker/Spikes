using OpenMRS_Clone.Core;
using OpenMRS_Clone.Persons;

namespace OpenMRS_Clone.Providers
{
	public class Provider : BaseCustomizableMetadata<ProviderAttribute>
	{
		public int ProviderId { get; set; }
		public Person Person { get; set; }
		public string Identifier { get; set; }
	}
}