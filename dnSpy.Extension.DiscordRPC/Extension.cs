using System.Collections.Generic;
using dnSpy.Contracts.Extension;

namespace HoLLy.dnSpyExtension.DiscordRPC
{
	[ExportExtension]
	public class Extension : IExtension
	{
		public IEnumerable<string> MergedResourceDictionaries { get { yield break; } }

		public ExtensionInfo ExtensionInfo => new ExtensionInfo {
			ShortDescription = "Provides Discord Rich Presence integration"
		};

		public void OnEvent(ExtensionEvent e, object? obj) { }
	}
}