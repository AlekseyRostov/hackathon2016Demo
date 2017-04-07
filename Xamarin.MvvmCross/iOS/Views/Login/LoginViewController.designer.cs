// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Feedback.iOS.Views.Login
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UIButton _loginBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_loginBtn != null) {
				_loginBtn.Dispose ();
				_loginBtn = null;
			}
		}
	}
}
