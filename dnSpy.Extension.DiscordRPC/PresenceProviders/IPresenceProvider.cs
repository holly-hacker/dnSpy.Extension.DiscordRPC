namespace HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders
{
	public interface IPresenceProvider
	{
		string Details { get; }
		bool CanProvidePresence();
		string? GetState();
	}
}