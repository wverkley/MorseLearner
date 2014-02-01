using System;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{
	public class FlickerFreePanel : Panel
	{
		public FlickerFreePanel()
		{
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
		}
	}
}
