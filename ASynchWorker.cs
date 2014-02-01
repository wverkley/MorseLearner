using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace Wxv.MorseLearner
{
	delegate void ASynchWorkerThreadCompletedCallback (ASynchWorkerThread worker);

	class ASynchWorkerThread
	{
		private Control control;
		private WaitCallback workCallback;
		private WaitCallback completedCallback;
		private ASynchWorkerThreadCompletedCallback threadCompletedCallback;
		
		private DateTime started = DateTime.Now;
		private bool stopped = false;

		public ASynchWorkerThread (Control control, WaitCallback workCallback, WaitCallback completedCallback, ASynchWorkerThreadCompletedCallback threadCompletedCallback)
		{
			this.control = control;
			this.workCallback = workCallback;
			this.completedCallback = completedCallback;
			this.threadCompletedCallback = threadCompletedCallback;
		}

		public void Execute(object state)
		{
			ThreadPool.QueueUserWorkItem (new WaitCallback (ExecuteWork), state);
		}

		public void Close ()
		{
			stopped = true;
		}

		private void ExecuteWork (object state)
		{
			workCallback(state);
			if (!stopped && (completedCallback != null))
				control.Invoke (new WaitCallback (ExecuteCallback), new object[] {state});
			threadCompletedCallback (this);
		}

		private void ExecuteCallback (object state)
		{
			if (!stopped)
				completedCallback (state);
		}
	}

	public class ASynchWorker
	{
		private Control control;
		public Control Control { get { return control; } }

		private ASynchWorkerThreadCompletedCallback threadCompletedCallback;

		private ArrayList workers = new ArrayList();
		
		public ASynchWorker(Control control)
		{
			this.control = control;
			this.threadCompletedCallback = new ASynchWorkerThreadCompletedCallback (WorkerCompleted);
		}

		public void Work (WaitCallback workCallback, WaitCallback completedCallback, object state)
		{
			ASynchWorkerThread worker = new ASynchWorkerThread (control, workCallback, completedCallback, threadCompletedCallback);
			lock (workers)
				workers.Add (worker);
			worker.Execute(state);
		}

		private void WorkerCompleted (ASynchWorkerThread worker)
		{
			lock (workers)
				workers.Remove (worker);
		}

		public void Close (bool wait)
		{
			bool working = false;
			lock (workers)
				foreach (ASynchWorkerThread worker in workers)
				{
					worker.Close();
					working = true;
				}
			
			while (wait && working)
			{
				lock (workers)
					working = (workers.Count > 0);
				if (working)
					Application.DoEvents();
			}
		}

		public void Close ()
		{
			Close (true);
		}
	}

}
