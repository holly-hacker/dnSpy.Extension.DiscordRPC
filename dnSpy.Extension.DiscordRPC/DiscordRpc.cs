using System;
using System.ComponentModel.Composition;
using System.Timers;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using dnSpy.Contracts.App;
using dnSpy.Contracts.Documents.Tabs;
using dnSpy.Contracts.Extension;
using dnSpy.Contracts.Output;
using HoLLy.dnSpyExtension.DiscordRPC.PresenceProviders;

namespace HoLLy.dnSpyExtension.DiscordRPC
{
	[ExportAutoLoaded(LoadType = AutoLoadedLoadType.AppLoaded)]
	public class DiscordRpc : IDisposable, IAutoLoaded
	{
		private DateTime StartTime { get; }
		private Timer UpdateTimer { get; }
		private DiscordRpcClient Client { get; }

		private readonly IPresenceProvider[] _presenceProviders;
		private readonly IAppWindow _appWindow;
		private readonly OutputPaneLogger _outputPaneLogger;

		[ImportingConstructor]
		public DiscordRpc(IOutputService outputService, IDocumentTabService tabService, IAppWindow appWindow)
		{
			_appWindow = appWindow;
			StartTime = DateTime.UtcNow;

			_presenceProviders = new IPresenceProvider[]
			{
				new CurrentTabPresenceProvider(tabService),
				new FallbackPresenceProvider(),
			};

			var outputPane = outputService.Create(Constants.LoggerOutputPaneGuid, "Discord RPC");
			_outputPaneLogger = new OutputPaneLogger(outputPane)
			{
				Level = LogLevel.Info,
			};

			UpdateTimer = new Timer(1000) {AutoReset = true};
			UpdateTimer.Elapsed += OnTimerTick;
			Client = new DiscordRpcClient(Constants.DiscordApplicationId, autoEvents: true)
			{
				SkipIdenticalPresence = true,
				Logger = _outputPaneLogger,
			};
			Client.OnReady += OnClientReady;

			Client.Initialize();
		}

		private void OnClientReady(object sender, ReadyMessage args)
		{
			UpdateTimer.Start();

			// start with constant values
			Client.SetPresence(new RichPresence
			{
				Timestamps = new Timestamps(StartTime),
				Assets = new Assets
				{
					LargeImageKey = Constants.LargeImageKeyDnSpy,
					LargeImageText = "dnSpy",
				},
			});
		}

		private void OnTimerTick(object sender, ElapsedEventArgs e)
		{
			// running from UI context so we don't get thread exceptions
			_appWindow.MainWindow.Dispatcher.Invoke(() =>
			{
				foreach (var provider in _presenceProviders)
				{
					try
					{
						if (!provider.CanProvidePresence())
							continue;

						var state = provider.GetState();
						Client.UpdateDetails(provider.Details);
						Client.UpdateState(state);
						break;
					}
					catch (Exception ex)
					{
						// ignored, continue with next
						_outputPaneLogger.Error($"Exception during provider {provider.GetType().Name}: {ex.Message}");
					}
				}
			});
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