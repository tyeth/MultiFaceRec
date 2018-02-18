using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiFaceRec
{

    public class SensLogonInterop : ISensLogon, IDisposable
    {
        private const string SubscriptionViewerName = "ManagedSENS.SensLogonInterop";
        private static string SubscriptionViewerID = "{" +
                                                     typeof(SensLogonInterop).GUID.ToString().ToUpper() + "}"; // generate a subscriptionID 
        private const string SubscriptionViewerDesc = "ManagedSENS Event Subscriber";

        private bool registered;

        public SensLogonInterop()
        {
            registered = false;
            EventSystemRegistrar.SubscribeToEvents(SubscriptionViewerDesc, SubscriptionViewerName,
                SubscriptionViewerID, this, typeof(ISensLogon));
            registered = true;
        }

        #region Cleanup Code

        ~SensLogonInterop()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool isExplicit)
        {
            this.Deactivate();
        }

        private void Deactivate()
        {
            if (registered)
            {
                EventSystemRegistrar.UnsubscribeToEvents(SubscriptionViewerID);
                registered = false;
            }
        }

        #endregion

        #region ISensLogon Members

        public void DisplayLock(string bstrUserName)
        {
            SensLogon.OnDisplayLock(bstrUserName);
        }
        public void DisplayUnlock(string bstrUserName)
        {
            SensLogon.OnDisplayUnlock(bstrUserName);
        }
        //...More ISensLogon memmbers
        #endregion
    }
}
