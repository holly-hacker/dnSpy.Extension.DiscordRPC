using dnSpy.Contracts.Debugger;

namespace HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders
{
	public class DebuggerPresenceProvider : IPresenceProvider
	{
		public string Details => "Debugging";
		private readonly DbgManager _dbgManager;

		public DebuggerPresenceProvider(DbgManager dbgManager)
		{
			_dbgManager = dbgManager;
		}

		public bool CanProvidePresence() => _dbgManager.IsDebugging && !(GetString() is null);
		public string? GetState() => GetString();

		private string? GetString() => _dbgManager.CurrentProcess.Current?.Name;
	}
}