using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Using Statements
using System.Diagnostics;
using System.Runtime.InteropServices;
using EventSystemLib;

namespace MultiFaceRec
{
    //In the ManagesSENS namespace
    [ComImport, Guid("4E14FBA2-2E22-11D1-9964-00C04FBBB345")]
    class EventSystem { }
    [ComImport, Guid("7542E960-79C7-11D1-88F9-0080C7D771BF")]
    class EventSubcription { }
    [ComImport, Guid("AB944620-79C6-11d1-88F9-0080C7D771BF")]
    class EventPublisher { }
    [ComImport, Guid("cdbec9c0-7a68-11d1-88f9-0080c7d771bf")]
    class EventClass { }

    public class EventSystemRegistrar
    {

        private const string PROGID_EventSubscription = "EventSystem.EventSubscription";
        static EventSystemRegistrar() { }

        private static IEventSystem es = null;
        private static IEventSystem EventSystem
        {
            get
            {
                if (es == null)
                    es = new EventSystem() as IEventSystem;
                return es;
            }
        }

        public static void SubscribeToEvents(string description, string subscriptionName, string
            subscriptionID, object subscribingObject, Type subscribingType)
        {
            // activate subscriber
            try
            {
                //create and populate a subscription object
                IEventSubscription sub = new EventSubcription() as IEventSubscription;
                sub.Description = description;
                sub.SubscriptionName = subscriptionName;
                sub.SubscriptionID = subscriptionID;
                //Get the GUID from the ISensLogon interface
                sub.InterfaceID = GetInterfaceGuid(subscribingType);
                sub.SubscriberInterface = subscribingObject;
                //Store the actual Event.
                EventSystem.Store(PROGID_EventSubscription, sub);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string GetInterfaceGuid(Type theType)
        {
            object[] attributes = theType.GetCustomAttributes(typeof(GuidAttribute), true);
            if (attributes.Length > 0)
            {
                return "{" + ((GuidAttribute)attributes[0]).Value + "}";
            }
            else
            {
                throw new ArgumentException("GuidAttribute not present on the Type.", "theType");
            }
        }

        public static void UnsubscribeToEvents(string subscriptionID)
        {
            try
            {
                string strCriteria = "SubscriptionID == " + subscriptionID;
                int errorIndex = 0;
                EventSystem.Remove("EventSystem.EventSubscription", strCriteria, out errorIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
