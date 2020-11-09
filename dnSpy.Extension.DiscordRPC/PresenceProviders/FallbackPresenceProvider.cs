namespace HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders
{
	public class FallbackPresenceProvider : IPresenceProvider
	{
		public string Details => "Doing nothing";

		public bool CanProvidePresence() => true;

		public string? GetState() => null;
	}
}