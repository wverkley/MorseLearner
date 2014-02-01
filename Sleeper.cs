using System;
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{

	public class Sleeper : ASynchWorker
	{
		public Sleeper (Control control) : base (control)
		{}

		private void SleepWork (object state)
		{
			Thread.Sleep ((int) state);
		}

		public void Sleep (WaitCallback completedCallback, int millisecondsTimeout)
		{
			Work (new WaitCallback (SleepWork), completedCallback, millisecondsTimeout);
		}
	}

}
