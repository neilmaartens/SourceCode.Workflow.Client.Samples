using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Force_Go_To_Actvity
    {
        /// <summary>
        /// shows how to force a process to go to another activity in the workflow. 
        /// </summary>
        public void GoToActivitySample()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection
                K2Conn.Open("localhost");

                //use the serial number to open the worklist item
                string serialnumber = "[ProcinstId]_[ActInstDestId]"; //TODO: use your own serial number
                WorklistItem wli = null;
                wli = K2Conn.OpenWorklistItem(serialnumber); 

                //force the current process instance expire all current activities and create an instance of the Activity "SomeActivity". 
    			wli.GotoActivity("SomeActivity",false, true); 
				//force the current process instance to only expire the activity that this specific worklist item belongs to. Any other activities will remain active
                wli = K2Conn.OpenWorklistItem(serialnumber); 
				wli.GotoActivity("SomeActivity",true,false);                   
            }
        }
    }
}
