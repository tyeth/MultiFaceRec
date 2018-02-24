using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace MultiFaceRec
{
  
    public static class ShFormMessageBox
    {
        [Flags]
        public enum MessageBoxCheckFlags : uint
        {
            MB_OK = 0x00000000,
            MB_OKCANCEL = 0x00000001,
            MB_YESNO = 0x00000004,
            MB_ICONHAND = 0x00000010,
            MB_ICONQUESTION = 0x00000020,
            MB_ICONEXCLAMATION = 0x00000030,
            MB_ICONINFORMATION = 0x00000040
        }
        
        [DllImport("shlwapi.dll", EntryPoint = "SHMessageBoxCheckA" /*"#185"*/, ExactSpelling = true, PreserveSig = false)]
        public static extern int SHMessageBoxCheck(

            [In][Optional] IntPtr hwnd,

            [In] String pszText,

            [In] String pszTitle,

             MessageBoxCheckFlags uType,

             int iDefault,

            [In] string pszRegVal

        );

        /// <summary>
        /// This code displays a dialog box with a "Don't show me this dialog again" checkbox and an OK button.In normal circumstances, result will always be 0 on return. 
        /// </summary>
        /// <param name="form">pass in a form or call as static extension method</param>
        /// <param name="text">Message Text</param>
        /// <param name="title">Message Title</param>
        /// <param name="flags">MessageBoxCheckFlags.OK and ICONINFORMATION by default</param>
        /// <returns>0 or -1 if ignored/errored</returns>
        public static int MessageBoxCheck(this Form form,string text, string title,MessageBoxCheckFlags flags=MessageBoxCheckFlags.MB_OK|MessageBoxCheckFlags.MB_ICONINFORMATION)
        {
            //Found via http://www.pinvoke.net/default.aspx/shlwapi.SHMessageBoxCheck
            //
            // Reg Key The Windows Shell (Explorer) stores your preference in the following registry key:
            // 
            // HKEY_CURRENT_USER
            //  Software
            //      Microsoft
            //          Windows
            //              CurrentVersion
            //                  Explorer
            //                      DontShowMeThisDialogAgain

            /* This code displays a dialog box with a "Don't show me this dialog again" checkbox and an OK button.  In normal circumstances, result will always be 0 on return. */

            int result;

            try
            {
                result = SHMessageBoxCheck(
                    form.Handle,
                    text,
                    title,
                    flags,
                    -1,
                    Application.ExecutablePath+"!"+form.Name // This last argument is the value of the registry key
                );
            }
            catch (Exception e)
            {
                // Note that the only exceptions we can get here are inter-op exceptions, I think.
                result = -1;
            }

            if (result == -1)
            {
                // The dialog didn't show up, so do some alternate action here.
            }

            return result;
        }

       

    }

    //public interface IFormMessageBox
    //{
    //    int MessageBox(Form form, string text, string title,
    //        ShFormMessageBox.MessageBoxCheckFlags flags = ShFormMessageBox.MessageBoxCheckFlags.MB_OK |
    //                                                      ShFormMessageBox.MessageBoxCheckFlags.MB_ICONINFORMATION);
    //}
}
