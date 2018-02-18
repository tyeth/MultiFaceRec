using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiFaceRec
{
    public delegate void SensLogonEventHandler(string userName);
    public class SensLogon
    {
        private static SensLogonInterop eventCatcher;
        static SensLogon() { }

        //...SensLogonInterop goes here

        #region Event Registration Code

        private static int registerCount = 0;
        private static bool IsRegistered
        {
            get
            {
                return (registerCount > 0);
            }
        }

        private static SensLogonEventHandler RegisterEvent(SensLogonEventHandler original,
         SensLogonEventHandler newDel)
        {
            bool shouldRegister = (original == null);
            original = original + newDel;
            if (shouldRegister)
            {
                if (registerCount <= 0)
                {
                    if (SensLogon.eventCatcher == null)
                        SensLogon.eventCatcher = new SensLogonInterop();
                    registerCount = 1;
                }
                else
                {
                    //Just count them.
                    registerCount++;
                }
            }
            return original;
        }

        private static SensLogonEventHandler UnregisterEvent(SensLogonEventHandler original,
         SensLogonEventHandler oldDel)
        {
            original = original - oldDel;
            if (original == null)
            {
                registerCount--;
                if (registerCount == 0)
                {
                    //unregister for those events.
                    SensLogon.eventCatcher.Dispose();
                    SensLogon.eventCatcher = null;
                }
            }
            return original;
        }

        #endregion

        #region ISensLogon Event Raising Members

        internal static void OnDisplayLock(string bstrUserName)
        {
            if (SensLogon.displayLock != null)
                SensLogon.displayLock(bstrUserName);
        }
        internal static void OnDisplayUnlock(string bstrUserName)
        {
            if (SensLogon.displayUnlock != null)
                SensLogon.displayUnlock(bstrUserName);
        }
        ///...

        #endregion

        #region Event Declarations

        private static SensLogonEventHandler displayLock = null;
        private static SensLogonEventHandler displayUnlock = null;
        ///...

        public static event SensLogonEventHandler DisplayLock
        {
            add
            {
                SensLogon.displayLock = SensLogon.RegisterEvent(SensLogon.displayLock, value);
            }
            remove
            {
                SensLogon.displayLock = SensLogon.UnregisterEvent(SensLogon.displayLock, value);
            }
        }
        public static event SensLogonEventHandler DisplayUnlock
        {
            add
            {
                SensLogon.displayUnlock = SensLogon.RegisterEvent(SensLogon.displayUnlock, value);
            }
            remove
            {
                SensLogon.displayUnlock = SensLogon.UnregisterEvent(SensLogon.displayUnlock, value);
            }
        }
        ///...
        #endregion
    }
}
