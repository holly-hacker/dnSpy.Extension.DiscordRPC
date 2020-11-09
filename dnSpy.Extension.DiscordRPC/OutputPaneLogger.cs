using DiscordRPC.Logging;
using dnSpy.Contracts.Output;
using dnSpy.Contracts.Text;

namespace HoLLy.dnSpyExtension.DiscordRPC
{
	public class OutputPaneLogger : ILogger
	{
		private readonly IOutputTextPane _outputPane;

		public OutputPaneLogger(IOutputTextPane outputPane)
		{
			_outputPane = outputPane;
		}

		public LogLevel Level { get; set; }

		public void Trace(string message, params object[] args)
		{
			if (Level <= LogLevel.Trace)
				_outputPane.WriteLine(TextColor.DebugLogTrace, string.Format(message, args));
		}

		public void Info(string message, params object[] args)
		{
			if (Level <= LogLevel.Info)
				_outputPane.WriteLine(TextColor.OutputWindowText, string.Format(message, args));
		}

		public void Warning(string message, params object[] args)
		{
			if (Level <= LogLevel.Warning)
				_outputPane.WriteLine(TextColor.Yellow, string.Format(message, args));
		}

		public void Error(string message, params object[] args)
		{
			if (Level <= LogLevel.Error)
				_outputPane.WriteLine(TextColor.Error, string.Format(message, args));
		}
	}
}