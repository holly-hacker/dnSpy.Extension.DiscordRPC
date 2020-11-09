using dnSpy.Contracts.Documents.Tabs;
using dnSpy.Contracts.Documents.TreeView;

namespace HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders
{
	public class TreeNodePresenceProvider : IPresenceProvider
	{
		public string Details => "Decompiling";
		private readonly IDocumentTabService _tabService;

		public TreeNodePresenceProvider(IDocumentTabService tabService)
		{
			_tabService = tabService;
		}

		public bool CanProvidePresence() => !(GetCurrentTabTitle() is null);
		public string? GetState() => GetCurrentTabTitle();

		private string? GetCurrentTabTitle()
		{
			return _tabService.DocumentTreeView.TreeView.SelectedItem switch
			{
				DocumentTreeNodeData nodeData => nodeData.ToString(),
				null => null,
				_ => null
			};
		}
	}
}