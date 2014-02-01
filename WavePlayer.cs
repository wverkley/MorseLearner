using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace Wxv.MorseLearner
{
	public class WavePlayer : ASynchWorker
	{
		public WavePlayer (Control control) : base (control)
		{}

		private void PlayBuffer (object state)
		{
			((WaveBuffer) state).Play();
		}

		public void Play (WaitCallback completedCallback, WaveBuffer buffer)
		{
			if (buffer == null)
				throw new Exception ("No wave buffer specified");
			Work (new WaitCallback (PlayBuffer), completedCallback, buffer);
		}
	}

}
