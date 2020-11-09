using System;
using System.ComponentModel.Composition;
using System.Timers;
using DiscordRPC;
using DiscordRPC.Message;
using dnSpy.Contracts.Extension;

namespace HoLLy.dnSpyExtension.DiscordRPC
{
	[ExportAutoLoaded(LoadType = AutoLoadedLoadType.AppLoaded)]
	public class DiscordRpc : IDisposable, IAutoLoaded
	{
		private DateTime StartTime { get; }
		private Timer UpdateTimer { get; }
		private DiscordRpcClient Client { get; }

		[ImportingConstructor]
		public DiscordRpc()
		{
			StartTime = DateTime.UtcNow;
			UpdateTimer = new Timer(1000) {AutoReset = true};
			UpdateTimer.Elapsed += OnTimerTick;
			Client = new DiscordRpcClient(Constants.DiscordApplicationId, autoEvents: true)
				{SkipIdenticalPresence = true};
			Client.OnReady += OnClientReady;

			Client.Initialize();
		}

		private void OnClientReady(object sender, ReadyMessage args)
		{
			UpdateTimer.Start();

			// start with constant values
			Client.SetPresence(new RichPresence
			{
				Timestamps = new Timestamps(DateTime.UtcNow),
				Assets = new Assets
				{
					LargeImageKey = Constants.LargeImageKeyDnSpy,
					LargeImageText = "dnSpy",
				},
			});
		}

		private void OnTimerTick(object sender, ElapsedEventArgs e)
		{
			Client.UpdateDetails("details!");
			Client.UpdateState(Guid.NewGuid().ToString());
		}

		public void Dispose()
		{
			UpdateTimer.Stop();
			UpdateTimer.Elapsed -= OnTimerTick;
			UpdateTimer.Dispose();

			Client.OnReady -= OnClientReady;
			Client.ClearPresence();
			Client.Dispose();
		}
	}
}