using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	public class MorseDocumentStateRecorder
	{
		private MorseDocument doc;
		private DateTime lastStateTime;
		private bool lastState;


		public MorseDocument MorseDocument
		{
			get { return doc; }
			set 
			{
				doc = value; 
				Clear();
			}
		}


		public MorseDocumentStateRecorder()
		{
			doc = new MorseDocument();
			Clear();
		}


		public MorseDocumentStateRecorder(MorseDocument doc)
		{
			this.doc = doc;
			Clear();
		}


		public int GetLastStateInterval (DateTime time)
		{
			if (lastStateTime == DateTime.MinValue)
				return 0;

			TimeSpan ts = time - lastStateTime;
			int result = (int) ts.TotalMilliseconds;
			if (result < 0)
				return 0;
			else
				return result;
		}

		
		public int GetLastStateInterval()
		{
			return GetLastStateInterval (DateTime.Now);
		}


		public void AddState (bool state, DateTime time)
		{
			doc.AddInterval (GetLastStateInterval(), lastState);

			if (state && (doc.StartTime == DateTime.MinValue))
				doc.StartTime = time;

			lastState = state;
			lastStateTime = time;
		}


		public void AddState (bool state)
		{
			AddState (state, DateTime.Now);
		}


		public void Clear()
		{
			lastStateTime = DateTime.MinValue;
			lastState = false;
		}
	}

}
