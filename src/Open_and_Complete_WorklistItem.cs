using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Open_and_Complete_WorklistItem
    {
        /// <summary>
        /// shows how to open and complete a worklist item
        /// </summary>
        public void OpenAndCompleteWorklistItem()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //TODO: get the task serial number from somewhere (e.g. query string or worklist)
                string serialNumber = "[ProcessInstanceId_ActivityInstanceDestinationId]";

                //open worklist item with serial number
                //or locate worklistitem in worklist collection, and open it using K2WLItem.Open();
                //opening the worklist item allocates it to the current user by default
                //you can use the Alloc parameter to adjust whether the task is allocated or not  
                WorklistItem K2WListItem = K2Conn.OpenWorklistItem(serialNumber);

                //once item is open, read some data from the item, or update values
                string oldFolio = K2WListItem.ProcessInstance.Folio;
                K2WListItem.ProcessInstance.Folio = "NewFolioValue";

                //iterate over the data fields collection
			    foreach(DataField dataField in K2WListItem.ProcessInstance.DataFields)
                {
                    //do something with each datafield, e.g. read or set value
                    string fieldName = dataField.Name;
                    string fieldValue = dataField.Value.ToString();
                }

                //read the available actions for the task
                foreach (SourceCode.Workflow.Client.Action action in K2WListItem.Actions)
                {
                    //do something, like outputting available actions to a drop-down list
                }

                //update datafields before completing the task, if needed
                K2WListItem.ProcessInstance.DataFields["[StringDataFieldName]"].Value = "[NewValue]";

                //to finish the task, call Action.Execute method
                //with the appropriate action name
                K2WListItem.Actions["[ActionName]"].Execute();
            }
        }
    }
}