using dnSpy.Contracts.Documents.Tabs;

namespace HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders
{
	public class CurrentTabPresenceProvider : IPresenceProvider
	{
		public string Details => "Looking at tab";
		private readonly IDocumentTabService _tabService;

		public CurrentTabPresenceProvider(IDocumentTabService tabService)
		{
			_tabService = tabService;
		}

		public bool CanProvidePresence() => !(GetCurrentTabTitle() is null);
		public string? GetState() => GetCurrentTabTitle();

		private string? GetCurrentTabTitle()
		{
			return _tabService.ActiveTab?.Content.Title;
		}
	}
}