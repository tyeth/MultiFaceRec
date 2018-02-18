    
//Using Statements
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiFaceRec
{
    class LogOnLogOffLockUnlockEvents
    {
        SensLogon sens = new SensLogon();
        sens.DisplayLock += new ISensLogon_DisplayLockEventHandler(sens_DisplayLock);
        sens.DisplayUnlock += new ISensLogon_DisplayUnlockEventHandler(sens_DisplayUnlock);
        Console.WriteLine("Registered For SENS Events");
        Console.ReadLine();

    }


}
