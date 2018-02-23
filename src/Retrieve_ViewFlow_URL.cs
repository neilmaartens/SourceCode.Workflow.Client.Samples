using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Retrieve_ViewFlow_URL
    {
        public void RetrieveViewFlowURL()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //Opening the Process Instance from the Connection object
                ProcessInstance pi = K2Conn.OpenProcessInstance(1); //TODO: Change to your process instance ID
                //get the View Flow URL
                string url1 = pi.ViewFlow;

                //Alternate: Opening the Process Instance from a worklist item
                string serialNo = "[serialnumber]"; //TODO: Change to your serial number
                WorklistItem wli = K2Conn.OpenWorklistItem(serialNo);
                //get the View Flow URL
                string url2 = wli.ProcessInstance.ViewFlow;
            }
        }
    }
}
