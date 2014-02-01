using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

using System.Windows.Forms;

namespace Wxv.MorseLearner  
{

	public class MouseActivityHook : IDisposable
	{
		public MouseActivityHook() 
		{
		}
		
		public void Dispose()
		{ 
			Close();
		} 

		public event MouseEventHandler OnMouseDown;
		public event MouseEventHandler OnMouseUp;

		public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

		static int hMouseHook = 0; //Declare mouse hook handle as int.

		//values from Winuser.h in Microsoft SDK.
		public const int WH_MOUSE_LL 	= 14;	//mouse hook constant

		HookProc MouseHookProcedure; //Declare MouseHookProcedure as HookProc type.

		//Declare wrapper managed POINT class.
		[StructLayout(LayoutKind.Sequential)]
		public class POINT 
		{
			public int x;
			public int y;
		}

		//Declare wrapper managed MouseHookStruct class.
		[StructLayout(LayoutKind.Sequential)]
		public class MouseHookStruct 
		{
			public POINT pt;
			public int hwnd;
			public int wHitTestCode;
			public int dwExtraInfo;
		}

		//Import for SetWindowsHookEx function.
		//Use this function to install a hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto,
			 CallingConvention=CallingConvention.StdCall)]
		public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, 
			IntPtr hInstance, int threadId);

		//Import for UnhookWindowsHookEx.
		//Call this function to uninstall the hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto,
			 CallingConvention=CallingConvention.StdCall)]
		public static extern bool UnhookWindowsHookEx(int idHook);
		
		//Import for CallNextHookEx.
		//Use this function to pass the hook information to next hook procedure in chain.
		[DllImport("user32.dll",CharSet=CharSet.Auto,
			 CallingConvention=CallingConvention.StdCall)]
		public static extern int CallNextHookEx(int idHook, int nCode, 
			Int32 wParam, IntPtr lParam);  

		public void Open()
		{
			// install Mouse hook 
			if(hMouseHook == 0)
			{
				// Create an instance of HookProc.
				MouseHookProcedure = new HookProc(MouseHookProc);

				hMouseHook = SetWindowsHookEx( WH_MOUSE_LL,
					MouseHookProcedure, 
					Marshal.GetHINSTANCE(
					Assembly.GetExecutingAssembly().GetModules()[0]),
					0);

				//If SetWindowsHookEx fails.
				if(hMouseHook == 0 )	
				{
					Close();
					throw new Exception("SetWindowsHookEx failed.");
				}
			}
		}

		public void Close()
		{
			bool retMouse = true;
			if(hMouseHook != 0)
			{
				retMouse = UnhookWindowsHookEx(hMouseHook);
				hMouseHook = 0;
			} 
			
			//If UnhookWindowsHookEx fails.
			if (!retMouse) 
				throw new Exception("UnhookWindowsHookEx failed.");
		}

		private const int WM_MOUSEMOVE = 0x200;
		private const int WM_LBUTTONDOWN = 0x201;
		private const int WM_RBUTTONDOWN = 0x204;
		private const int WM_MBUTTONDOWN = 0x207;
		private const int WM_LBUTTONUP = 0x202;
		private const int WM_RBUTTONUP = 0x205;
		private const int WM_MBUTTONUP = 0x208;
		private const int WM_LBUTTONDBLCLK = 0x203;
		private const int WM_RBUTTONDBLCLK = 0x206;
		private const int WM_MBUTTONDBLCLK = 0x209;

		private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			MouseButtons button = MouseButtons.None;
			switch (wParam)
			{
				case WM_LBUTTONDOWN : button = MouseButtons.Left; break;
				case WM_LBUTTONUP : button = MouseButtons.Left; break;
				// case WM_LBUTTONDBLCLK : return;
				case WM_RBUTTONDOWN : button=MouseButtons.Right; break; 
				case WM_RBUTTONUP : button=MouseButtons.Right; break;
				// case WM_RBUTTONDBLCLK : return;
			}

			// if ok and someone listens to our events
			if ((nCode >= 0) && (button != MouseButtons.None) && (OnMouseDown != null || OnMouseUp != null))
			{
				System.Diagnostics.Debug.WriteLine (nCode + "," + wParam + "," + lParam.ToString());

				int clicks = 0;
				if (button!=MouseButtons.None)
					if (wParam==WM_LBUTTONDBLCLK || wParam==WM_RBUTTONDBLCLK) 
						clicks = 2;
					else 
						clicks = 1;
				
				//Marshall the data from callback.
				MouseHookStruct MyMouseHookStruct = (MouseHookStruct) Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
				MouseEventArgs e= new MouseEventArgs (
					button, 
					clicks, 
					MyMouseHookStruct.pt.x, 
					MyMouseHookStruct.pt.y, 
					0 );
				
				switch (wParam)
				{
					case WM_LBUTTONDOWN : 
						System.Diagnostics.Debug.WriteLine ("WM_LBUTTONDOWN");
						OnMouseDown (this, e); 
						break;
					case WM_RBUTTONDOWN : 
						System.Diagnostics.Debug.WriteLine ("WM_RBUTTONDOWN");
						OnMouseDown (this, e); 
						break;
					case WM_RBUTTONUP : 
						System.Diagnostics.Debug.WriteLine ("WM_RBUTTONUP");
						OnMouseUp (this, e); 
						break; 
					case WM_LBUTTONUP : 
						System.Diagnostics.Debug.WriteLine ("WM_LBUTTONUP");
						OnMouseUp (this, e); 
						break;
				}
			}

			return CallNextHookEx(hMouseHook, nCode, wParam, lParam); 
		}
	}
}
