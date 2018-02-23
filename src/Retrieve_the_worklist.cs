using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Retrieve_the_worklist
    {
        public void GetWorklist()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //retrieve the entire worklist for the connected user
                SourceCode.Workflow.Client.Worklist K2WList = K2Conn.OpenWorklist();

                //iterate over the worklist items in the worklist
                foreach (SourceCode.Workflow.Client.WorklistItem K2WLItem in K2WList)
                {
                    //do something with the worklist item
                    //you can query properties/objects contained in the worklist item object
                    string serialNumber = K2WLItem.SerialNumber;
                    string status = K2WLItem.Status.ToString();
                    string Folio = K2WLItem.ProcessInstance.Folio;
                }
            }
        }
    }
}
