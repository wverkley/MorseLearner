using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

using System.Windows.Forms;

namespace Wxv.MorseLearner  
{

	public class KeyActivityHook : IDisposable 
	{
		public KeyActivityHook() 
		{
		}
		
		public void Dispose()
		{ 
			Close();
		} 

		public event KeyEventHandler OnKeyDown;
		public event KeyPressEventHandler OnKeyPress;
		public event KeyEventHandler OnKeyUp;

		public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

		public int hKeyboardHook = 0; //Declare keyboard hook handle as int.

		//value from Winuser.h in Microsoft SDK.
		public const int WH_KEYBOARD_LL = 13;	//keyboard hook constant	

		HookProc KeyboardHookProcedure; //Declare KeyboardHookProcedure as HookProc type.
			
		//Declare wrapper managed POINT class.
		[StructLayout(LayoutKind.Sequential)]
			public class POINT 
		{
			public int x;
			public int y;
		}

		//Declare wrapper managed KeyboardHookStruct class.
		[StructLayout(LayoutKind.Sequential)]
			public class KeyboardHookStruct
		{
			public int vkCode;	//Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
			public int scanCode; // Specifies a hardware scan code for the key. 
			public int flags;  // Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
			public int time; // Specifies the time stamp for this message.
			public int dwExtraInfo; // Specifies extra information associated with the message. 
		}


		//Import for SetWindowsHookEx function.
		//Use this function to install a hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, 
			IntPtr hInstance, int threadId);

		//Import for UnhookWindowsHookEx.
		//Call this function to uninstall the hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern bool UnhookWindowsHookEx(int idHook);
		
		//Import for CallNextHookEx.
		//Use this function to pass the hook information to next hook procedure in chain.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern int CallNextHookEx(int idHook, int nCode, 
			Int32 wParam, IntPtr lParam);  

		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern short GetKeyState (int virtKey);

		public void Open()
		{
			// install Keyboard hook 
			if(hKeyboardHook == 0)
			{
				KeyboardHookProcedure = new HookProc(KeyboardHookProc);
				hKeyboardHook = SetWindowsHookEx( WH_KEYBOARD_LL,
					KeyboardHookProcedure, 
					Marshal.GetHINSTANCE(
					Assembly.GetExecutingAssembly().GetModules()[0]),
					0);

				//If SetWindowsHookEx fails.
				if(hKeyboardHook == 0 )	
				{
					Close();
					throw new Exception("SetWindowsHookEx ist failed.");
				}
			}
		}

		public void Close()
		{
			bool retKeyboard = true;
			
			if(hKeyboardHook != 0)
			{
				retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
				hKeyboardHook = 0;
			} 
			
			//If UnhookWindowsHookEx fails.
			if (!retKeyboard) 
				throw new Exception("UnhookWindowsHookEx failed.");
		}

		//The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding character or characters. The function translates the code using the input language and physical keyboard layout identified by the keyboard layout handle.
		[DllImport("user32")] 
		public static extern int ToAscii(int uVirtKey, //[in] Specifies the virtual-key code to be translated. 
			int uScanCode, // [in] Specifies the hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up (not pressed). 
			byte[] lpbKeyState, // [in] Pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function, only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys is ignored.
			byte[] lpwTransKey, // [out] Pointer to the buffer that receives the translated character or characters. 
			int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 

		//The GetKeyboardState function copies the status of the 256 virtual keys to the specified buffer. 
		[DllImport("user32")] 
		public static extern int GetKeyboardState(byte[] pbKeyState);

		private const int WM_KEYDOWN 		= 0x100;
		private const int WM_KEYUP 			= 0x101;
		private const int WM_SYSKEYDOWN 	= 0x104;
		private const int WM_SYSKEYUP 		= 0x105;

		private const int VK_SHIFT			= 16;
		private const int VK_CONTROL		= 17;
		private const int VK_MENU			= 18;

		private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			bool handled = false;

			// it was ok and someone listens to events
			if ((nCode >= 0) && (OnKeyDown != null || OnKeyUp != null || OnKeyPress != null))
			{
				KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
				// raise KeyDown
				if (OnKeyDown!=null && (wParam ==WM_KEYDOWN || wParam==WM_SYSKEYDOWN ))
				{
					Keys keyData=(Keys)MyKeyboardHookStruct.vkCode;
					if ((GetKeyState(VK_CONTROL) & 0x8000) != 0) keyData |= Keys.Control;
					if ((GetKeyState(VK_MENU) & 0x8000) != 0) keyData |= Keys.Alt;
					if ((GetKeyState(VK_SHIFT) & 0x8000) != 0) keyData |= Keys.Shift;
					KeyEventArgs e = new KeyEventArgs(keyData);
					OnKeyDown(this, e);
					handled = handled | e.Handled;
				}
				
				// raise KeyPress
				if (OnKeyPress != null && wParam == WM_KEYDOWN )
				{
					byte[] keyState = new byte[256];
					GetKeyboardState(keyState);

					byte[] inBuffer= new byte[2];
					if (ToAscii(MyKeyboardHookStruct.vkCode,
						MyKeyboardHookStruct.scanCode,
						keyState,
						inBuffer,
						MyKeyboardHookStruct.flags)==1) 
					{
						KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
						OnKeyPress(this, e);
						handled = handled | e.Handled;
					}
				}
				
				// raise KeyUp
				if (OnKeyUp != null && (wParam ==WM_KEYUP || wParam == WM_SYSKEYUP))
				{
					Keys keyData=(Keys)MyKeyboardHookStruct.vkCode;
					if ((GetKeyState(VK_CONTROL) & 0x8000) != 0) keyData |= Keys.Control;
					if ((GetKeyState(VK_MENU) & 0x8000) != 0) keyData |= Keys.Alt;
					if ((GetKeyState(VK_SHIFT) & 0x8000) != 0) keyData |= Keys.Shift;	
					KeyEventArgs e = new KeyEventArgs(keyData);
					OnKeyUp(this, e);
					handled = handled | e.Handled;
				}

			}

			if (!handled)
				return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam); 
			else
				return 1;
		}
	}
}
