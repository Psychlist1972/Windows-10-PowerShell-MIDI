// source: http://www.dev-one.com/2014/03/asynchronous-methods-in-powershell/

using Microsoft.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PeteBrown.PowerShellMidi
{
    public abstract class AsyncPSCmdlet : PSCmdlet
    {
        protected virtual Task ProcessRecordAsync()
        {
            return Task.Delay(0);
        }

        protected sealed override void ProcessRecord()
        {
            AsyncPump.Run(async delegate
            {
                await ProcessRecordAsync();
            });
        }
    }
}
