using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Retrieve_the_worklist_with_criteria
    {
        /// <summary>
        /// shows how to filter the work.ist by passing in criteria
        /// </summary>
        public void Retrieve_Worklist_With_Criteria()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection
                K2Conn.Open("localhost");

                //build up criteria for filtering and sorting
                SourceCode.Workflow.Client.WorklistCriteria K2Crit = new WorklistCriteria();
                //example: filter for workflows in MyFolder 
                K2Crit.AddFilterField(WCField.ProcessFolder, WCCompare.Equal, "MyFolder");
                //example: filter for workflows with priority 1
                K2Crit.AddFilterField(WCLogical.And, WCField.ProcessPriority, WCCompare.Equal, 1);
                //example: sort by workflow start date, descending
                K2Crit.AddSortField(WCField.ProcessStartDate, WCSortOrder.Descending);

                //open the worklist with the given criteria 
                SourceCode.Workflow.Client.Worklist K2WList = K2Conn.OpenWorklist(K2Crit);

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

        /// <summary>
        /// shows how to set values to retrieve the worklist faster with NoData and Platform
        /// </summary>
        public void Retrieve_Worklist_With_Criteria_Faster()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection
                K2Conn.Open("localhost");

                //use criteria to build a faster worklist retrieval
                SourceCode.Workflow.Client.WorklistCriteria K2Crit = new WorklistCriteria();
                K2Crit.NoData = true; //faster, but does not return the data for each item 
                K2Crit.Platform = "ASP"; //helps when multiple platform are used 
                //Open the Worklist and get the worklistitem count 
                SourceCode.Workflow.Client.Worklist K2WList = K2Conn.OpenWorklist(K2Crit);
                //get the number of items in the worklist
                int workItemCount = K2WList.TotalCount;
                //iterate over the worklist items in the worklist
                foreach (SourceCode.Workflow.Client.WorklistItem K2WLItem in K2WList)
                {
                    //do something with the worklist item
                    //you can query properties/objects contained in the worklist item object
                    string serialNumber = K2WLItem.SerialNumber;
                    string Folio = K2WLItem.ProcessInstance.Folio;
                }
            }
        }
    }
}
