using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace Scraper
{
	public static class DebugConsole
	{
		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		private static IntPtr console = IntPtr.Zero;

		#region Public Static Properties
		public static string TimestampFormat = "[HH:mm:ss] ";
		#endregion

		#region Public Methods
		public static void ShowConsole()
		{
			if (console == IntPtr.Zero)
			{
				AllocConsole();
				Console.Title = "4chanscraper - Console";

				console = FindWindow(null, "4chanscraper - Console");
			}

			ShowWindow(DebugConsole.console, 1);
		}

		public static void HideConsole()
		{
			if (console != IntPtr.Zero)
				ShowWindow(DebugConsole.console, 0);
		}

		public static void ShowInfo(string Text, string LineTerminator)
		{
			StringBuilder Formatted = new StringBuilder();
			if (TimestampFormat != "") Formatted.Append(DateTime.Now.ToString(TimestampFormat));
			Formatted.Append("[ \x1B[37mINFO\x1B[0m ]    ");
			Formatted.Append(Text);
			Formatted.Append(LineTerminator);
			WriteParseANSI(Formatted.ToString());
		}
		public static void ShowInfo(string Text) { ShowInfo(Text, "\n"); }

		public static void ShowStatus(string Text, string LineTerminator)
		{
			StringBuilder Formatted = new StringBuilder();
			if (TimestampFormat != "") Formatted.Append(DateTime.Now.ToString(TimestampFormat));
			Formatted.Append("[ \x1B[32mSTATUS\x1B[0m ]  ");
			Formatted.Append(Text);
			Formatted.Append(LineTerminator);
			WriteParseANSI(Formatted.ToString());
		}
		public static void ShowStatus(string Text) { ShowStatus(Text, "\n"); }

		public static void ShowWarning(string Text, string LineTerminator)
		{
			StringBuilder Formatted = new StringBuilder();
			if (TimestampFormat != "") Formatted.Append(DateTime.Now.ToString(TimestampFormat));
			Formatted.Append("[ \x1B[33mWARNING\x1B[0m ] ");
			Formatted.Append(Text);
			Formatted.Append(LineTerminator);
			WriteParseANSI(Formatted.ToString());
		}
		public static void ShowWarning(string Text) { ShowWarning(Text, "\n"); }

		public static void ShowError(string Text, string LineTerminator)
		{
			StringBuilder Formatted = new StringBuilder();
			if (TimestampFormat != "") Formatted.Append(DateTime.Now.ToString(TimestampFormat));
			Formatted.Append("[ \x1B[31mERROR\x1B[0m ]   ");
			Formatted.Append(Text);
			Formatted.Append(LineTerminator);
			WriteParseANSI(Formatted.ToString());
		}
		public static void ShowError(string Text) { ShowError(Text, "\n"); }

		public static void ShowDebug(string Text, string LineTerminator)
		{
#if DEBUG
			StringBuilder Formatted = new StringBuilder();
			if (TimestampFormat != "") Formatted.Append(DateTime.Now.ToString(TimestampFormat));
			Formatted.Append("[ \x1B[36mDEBUG\x1B[0m ]   ");
			Formatted.Append(Text);
			Formatted.Append(LineTerminator);
			WriteParseANSI(Formatted.ToString());
#endif
		}
		public static void ShowDebug(string Text) { ShowDebug(Text, "\n"); }

		public static void WriteParseANSI(string Text)
		{
			lock (System.Console.Out)
			{
				char[] Characters = Text.ToCharArray();
				string ToWrite = "";
				string Sequence = "";
				for (int i = 0; i < Characters.Length; i++)
				{
					if (Characters[i] == "\x1B".ToCharArray()[0])
					{
						System.Console.Write(ToWrite);
						for (Sequence = ToWrite = ""; Characters[i] != "m".ToCharArray()[0]; i++)
						{
							Sequence += Characters[i];
						}
						switch (Sequence.Trim().ToLower())
						{
							case "\x1B[0": System.Console.ResetColor(); break;
							case "\x1B[30": System.Console.ForegroundColor = ConsoleColor.Black; break;
							case "\x1B[31": System.Console.ForegroundColor = ConsoleColor.Red; break;
							case "\x1B[32": System.Console.ForegroundColor = ConsoleColor.Green; break;
							case "\x1B[33": System.Console.ForegroundColor = ConsoleColor.Yellow; break;
							case "\x1B[34": System.Console.ForegroundColor = ConsoleColor.Blue; break;
							case "\x1B[35": System.Console.ForegroundColor = ConsoleColor.Magenta; break;
							case "\x1B[36": System.Console.ForegroundColor = ConsoleColor.Cyan; break;
							case "\x1B[37": System.Console.ForegroundColor = ConsoleColor.White; break;
							case "\x1B[39": System.Console.ForegroundColor = ConsoleColor.Gray; break;
						}
						++i;
					}
					ToWrite += Characters[i];
				}
				System.Console.Write(ToWrite);
			}
		}

		public static void WriteEventLog(string Text)
		{
			WriteEventLog(Text, EventLogEntryType.Information);
		}
		public static void WriteEventLog(string Text, EventLogEntryType type)
		{
			try
			{
				if (EventLog.SourceExists("DWM"))
				{
					DebugConsole.ShowDebug("Event log write [" + type.ToString() + "]: " + Text);
					DebugConsole.Pause();
					EventLog log = new EventLog("Desktop Window Manager", ".", "DWM");
					log.WriteEntry(Text, type);
				}
			}
			catch (Exception) { }
		}
		
		public static void ClearLine()
		{
			int Width = System.Console.WindowWidth - 1;
			string Spaces;
			for (Spaces = " "; Spaces.Length < Width; Spaces += " ") { continue; }
			Console.Write("\r" + Spaces + "\r");
		}

		public static void Pause()
		{
			lock (System.Console.In)
			{
				Console.WriteLine("Press any key to continue...");
				System.Console.ReadKey(true);
			}
		}

		#endregion
	}
}