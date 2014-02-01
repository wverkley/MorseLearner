using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{

	public enum MorsePlayerActionEnum {Dit, Dah, IntraChar, InterChar, InterWord, Character, Completed}

	public class MorsePlayerActionArgs : IDisposable
	{
		private MorsePlayerActionEnum action;
		public MorsePlayerActionEnum Action
		{
			get { return action; }
		}

		private string character;
		public string Character
		{
			get { return character; }
		}

		private bool cancelled;
		public bool Cancelled
		{
			get { return cancelled; }
		}

		private AutoResetEvent completedEvent;
		private bool completed;

		public MorsePlayerActionArgs (MorsePlayerActionEnum action, string character, AutoResetEvent completedEvent)
		{
			this.action = action;
			this.character = character;
			this.completedEvent = completedEvent;
		}

		public MorsePlayerActionArgs (MorsePlayerActionEnum action, string character)
			: this (action, character, null)
		{
		}

		public MorsePlayerActionArgs (MorsePlayerActionEnum action)
			: this (action, null, null)
		{
			this.action = action;
		}

		public void Dispose()
		{
			if (completedEvent != null)
				completedEvent.Close();
		}

		public void WaitCompleted()
		{
			if (completed)
				return;

			if (completedEvent != null)
				completedEvent.WaitOne();
			completed = true;
		}

		public void Cancel()
		{
			cancelled = true;
		}
	}


	public delegate void MorsePlayerActionDelegate (object sender, MorsePlayerActionArgs args);


	public class MorsePlayerStoppingException : Exception
	{
	}
		
		
	public class MorsePlayer : IDisposable
	{
		public const int DefaultWpm = 18;
		public const int DefaultOverallWpm = 5;
		public const int DefaultFrequency = 875;

		public static readonly MorsePlayerActionEnum[] NoActions = new MorsePlayerActionEnum[] {};
		public static readonly MorsePlayerActionEnum[] AllActions = new MorsePlayerActionEnum[] 
		{
			MorsePlayerActionEnum.Dit,
			MorsePlayerActionEnum.Dah,
			MorsePlayerActionEnum.IntraChar,
			MorsePlayerActionEnum.InterChar,
			MorsePlayerActionEnum.InterWord,
			MorsePlayerActionEnum.Character,
			MorsePlayerActionEnum.Completed
		};
		public static readonly MorsePlayerActionEnum[] DisplayActions = new MorsePlayerActionEnum[] 
		{
			MorsePlayerActionEnum.Dit,
			MorsePlayerActionEnum.Dah,
			MorsePlayerActionEnum.InterWord,
			MorsePlayerActionEnum.Character,
			MorsePlayerActionEnum.Completed
		};

		private int WaveOutWriteBufferSize = 1024 * 10;
		private int WriteAheadDurationMin = 500;
		private int WriteAheadDurationMax = 1000;
				
		private AutoResetEvent invokeEvent = new AutoResetEvent(false);
		private AutoResetEvent playClosedEvent = new AutoResetEvent(false);
		private AutoResetEvent invokeClosedEvent = new AutoResetEvent(false);

		private ArrayList invokeActions = new ArrayList();
		private MorsePlayerActionEnum[] reportActions = {};

		private bool playing = false;
		private bool stopping = false;
		private bool closing = false;

		private WaveGenerator generator;
		private WaveOutWriter waveOutWriter;

		private Control control;
		public Control Control
		{
			get { return control; }
		}

		private int wpm = DefaultWpm;
		public int Wpm
		{
			get { return wpm; }
			set { wpm = value; }
		}

		private int overallWpm = DefaultOverallWpm;
		public int OverallWpm
		{
			get { return overallWpm; }
			set { overallWpm = value; }
		}

		private int frequency = DefaultFrequency;
		public int Frequency
		{
			get { return frequency; }
			set { frequency = value; }
		}

		private MorsePlayerActionDelegate onAction = null;
		public MorsePlayerActionDelegate OnAction
		{
			get { return onAction; }
			set { onAction = value; }
		}


		public MorsePlayer (Control control)
		{
			this.control = control;
			generator = new WaveGenerator();
			waveOutWriter = new WaveOutWriter (generator.WaveFormat);

			ThreadPool.QueueUserWorkItem (new WaitCallback (InvokerWork), null);
		}

		public void Dispose()
		{
			Stop();

			closing = true;
			while (!invokeClosedEvent.WaitOne (10, false))
				Application.DoEvents();
			
			waveOutWriter.Dispose();
			invokeEvent.Close();
			playClosedEvent.Close();
			invokeClosedEvent.Close();
		}

		private void CheckStopping ()
		{
			if (stopping)
				throw new MorsePlayerStoppingException();
		}


		private void InvokerWork (object state)
		{
			try
			{
				while (!closing)
				{
					invokeEvent.WaitOne (10, false);
					if (closing)
						break;

					MorsePlayerActionArgs args = null;
					bool hasActions;
					lock (invokeActions)
					{
						hasActions = (invokeActions.Count > 0);
					}
					
					while (hasActions)
					{
						try
						{
							lock (invokeActions)
							{
								args = (MorsePlayerActionArgs) invokeActions[0];
							}
					
							args.WaitCompleted();
							if ((control != null) && !args.Cancelled)
								control.Invoke (new WaitCallback (InvokeCallback), new object[] {args});
						}
						finally
						{
							if (args != null)
								lock (invokeActions)
								{
									invokeActions.Remove (args);
									hasActions = (invokeActions.Count > 0);
								}
						}
					}
				}
			}
			finally
			{
				invokeClosedEvent.Set();
			}
		}


		private void InvokeCallback (object state)
		{
			if (closing)
				return;

			MorsePlayerActionArgs args = (MorsePlayerActionArgs) state;
			if (!args.Cancelled)
				onAction (this, args);
		}


		private void CancelInvokerActions ()
		{
			lock (invokeActions)
			{
				foreach (MorsePlayerActionArgs args in invokeActions)
					args.Cancel();
			}
		}


		private void AddInvokerAction (MorsePlayerActionEnum action, string character)
		{
			if (Array.IndexOf (reportActions, action) == -1)
				return;

			// write a small silence and mark it with an event that has to complete before the 
			// action is fired back to the UI
			AutoResetEvent completedEvent = new AutoResetEvent(false);
			waveOutWriter.Write (new byte[generator.WaveFormat.BlockAlign], completedEvent, null);

			lock (invokeActions)
			{
				invokeActions.Add (new MorsePlayerActionArgs (action, character, completedEvent));
			}
			invokeEvent.Set();
		}


		private void AddInvokerAction (MorsePlayerActionEnum action)
		{
			AddInvokerAction (action, null);
		}


		private void WriteWave (byte[] data)
		{
			waveOutWriter.Write (data);
			while (waveOutWriter.AheadDuration > WriteAheadDurationMax)
			{
				waveOutWriter.WaitAheadDurationBelow (WriteAheadDurationMin, 10);
				CheckStopping();
			}
		}


		private void WriteMorseWave (int length, bool on)
		{
			generator.Duration = length;
			generator.Frequency = frequency;
			if (on)
				generator.Volume = WaveGenerator.DefaultVolume;
			else
				generator.Volume = 0;

			byte[] data = generator.GenerateFirst (WaveOutWriteBufferSize);
			while (data != null)
			{
				WriteWave (data);
				CheckStopping();
				data = generator.GenerateNext();
			}
		}


		private void PlayTextWorkChar (string character)
		{
			MorseConstant c = MorseConstant.GetByCharacter (character.ToString());
				
			bool firstAction = true;
			foreach (char action in c.Morse)
			{
				if (!firstAction)
				{
					AddInvokerAction (MorsePlayerActionEnum.IntraChar, "");
					WriteMorseWave (MorseConstant.IntraCharLength (wpm), false);
					CheckStopping();
				}
				firstAction = false;

				if (action == '-')
				{
					AddInvokerAction (MorsePlayerActionEnum.Dah, character);
					WriteMorseWave (MorseConstant.DahLength (wpm), true);
					CheckStopping();
				}
				else if (action == '.')
				{
					AddInvokerAction (MorsePlayerActionEnum.Dit, character);
					WriteMorseWave (MorseConstant.DitLength (wpm), true);
					CheckStopping();
				}
			}
		}


		private void PlayTextWork (object state)
		{
			try
			{
				string message = (string) state;
				waveOutWriter.Write (new byte[10]); // write some zeros to get rid of crackle

				bool inWord = false;
				foreach (char character in message.ToCharArray())
				{
					MorseConstant morse = MorseConstant.GetByCharacter (character.ToString());
					if (morse != null)
					{
						if (inWord)
						{
							AddInvokerAction (MorsePlayerActionEnum.InterChar);
							WriteMorseWave (MorseConstant.InterCharLength (wpm, overallWpm), false);
							CheckStopping();
						}
						inWord = true;

						PlayTextWorkChar (character.ToString());
						AddInvokerAction (MorsePlayerActionEnum.Character, character.ToString());
					}
					else
					{
						if (inWord)
						{
							WriteMorseWave (MorseConstant.InterWordLength (wpm, overallWpm), false);
							CheckStopping();
							AddInvokerAction (MorsePlayerActionEnum.InterWord, " ");
						}
						inWord = false;
					}
				}

				AddInvokerAction (MorsePlayerActionEnum.Completed);

				waveOutWriter.Write (new byte[100]); // write some zeros to get rid of crackle
			}
			catch (MorsePlayerStoppingException)
			{
				waveOutWriter.Stop();
			}
			finally
			{
				// waveOutWriter.WaitAll();
				playClosedEvent.Set();
				//playing = false;
				//completed = true;
			}
		}


		private void PlayMorseDocWork (object state)
		{
			try
			{
				MorseDocument morseDoc = (MorseDocument) state;
				MorseDocumentParser parser = new MorseDocumentParser (morseDoc);
				waveOutWriter.Write (new byte[10]); // write some zeros to get rid of crackle

				int wpm = this.wpm;
//				if (wpm == 0)
//					wpm = morseDoc.EstimatedWpm;

				MorseDocumentParser.ParseResult mpr = parser.Parse (wpm, false);
				while ((mpr != null) && mpr.Complete)
				{
					if (mpr.Type == MorseDocumentParser.ParseResultType.Character)
					{
						bool on = true;
						int i = 0;
						foreach (int interval in mpr.Intervals)
						{
							if (on)
							{
								if (mpr.Morse[i].ToString() == MorseConstant.DitStr)
								{
									WriteMorseWave (interval, true);
									CheckStopping();
									AddInvokerAction (MorsePlayerActionEnum.Dit);
								}
								else if (mpr.Morse[i].ToString() == MorseConstant.DahStr)
								{
									WriteMorseWave (interval, true);
									CheckStopping();
									AddInvokerAction (MorsePlayerActionEnum.Dah);
								}
								i++;
							}
							else
							{
								WriteMorseWave (interval, false);
								CheckStopping();
								AddInvokerAction (MorsePlayerActionEnum.IntraChar);
							}

							on = !on;
						}

						AddInvokerAction (MorsePlayerActionEnum.Character, mpr.Character);
					}
					else if (mpr.Type == MorseDocumentParser.ParseResultType.InterChar)
					{
						WriteMorseWave (mpr.TotalInterval, false);
						CheckStopping();
						AddInvokerAction (MorsePlayerActionEnum.InterChar);
					}
					else if (mpr.Type == MorseDocumentParser.ParseResultType.InterWord)
					{
						WriteMorseWave (mpr.TotalInterval, false);
						CheckStopping();
						AddInvokerAction (MorsePlayerActionEnum.InterWord);
					}

					mpr = parser.Parse (wpm, false);
				}
				AddInvokerAction (MorsePlayerActionEnum.Completed);

				waveOutWriter.Write (new byte[100]); // write some zeros to get rid of crackle
			}
			catch (MorsePlayerStoppingException)
			{
				waveOutWriter.Stop();
			}
			finally
			{
				// waveOutWriter.WaitAll();
				playClosedEvent.Set();
				//playing = false;
				//completed = true;
			}
		}


		public void Stop ()
		{
			if (!playing)
				return;

			stopping = true;
			waveOutWriter.Stop();

			CancelInvokerActions();

			playClosedEvent.WaitOne();
			
			stopping = false;
			playing = false;
		}


		public void Play (string message, MorsePlayerActionEnum[] reportActions)
		{
			if (playing)
				Stop();

			this.reportActions = reportActions;
			
			playing = true;
			stopping = false;

			invokeClosedEvent.Reset();
			playClosedEvent.Reset();
			invokeClosedEvent.Reset();

			ThreadPool.QueueUserWorkItem (new WaitCallback (PlayTextWork), message);
		}
		
		public void Play (string message)
		{
			Play (message, new MorsePlayerActionEnum[] {});
		}


		public void Play (MorseDocument morseDoc, MorsePlayerActionEnum[] reportActions)
		{
			if (playing)
				Stop();

			this.reportActions = reportActions;
			
			playing = true;
			stopping = false;
			invokeClosedEvent.Reset();
			playClosedEvent.Reset();
			invokeClosedEvent.Reset();

			ThreadPool.QueueUserWorkItem (new WaitCallback (PlayMorseDocWork), morseDoc);
		}
		
		public void Play (MorseDocument morseDoc)
		{
			Play (morseDoc, new MorsePlayerActionEnum[] {});
		}
	
	}
}

