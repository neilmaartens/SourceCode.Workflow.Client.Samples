using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Set_Out_of_Office_Status
    {
        /// <summary>
        /// set OOO status "on" or "off" 
        /// </summary>
        public void SetOOFStatus()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");
                //set Out of Office ON for the current user
                K2Conn.SetUserStatus(UserStatuses.OOF);
                //set Out of Office OFF for the current user
                K2Conn.SetUserStatus(UserStatuses.Available); 

                //set Out of Office ON for a managed user
                K2Conn.SetUserStatus(@"[Label]:[Domain\Username]", UserStatuses.OOF); //e.g. K2:DOMAIN\Username 
                //set Out of Office OFF for a managed user
                K2Conn.SetUserStatus(@"[Label]:[Domain\Username]", UserStatuses.Available); //e.g. K2:DOMAIN\Username 
            }
        }


        /// <summary>
        /// Defining OOO Rules to determine where tasks should be delegated to while user is OOO
        /// </summary>
        public void DefineOOODelegationRules()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //Define Worklist criteria to retrieve worklistitems to be delegated to the other user 
                WorklistCriteria worklistcriteria = new WorklistCriteria();
                worklistcriteria.Platform = "ASP";
                //Define the user that the work must be delegated to as a Destination 
                Destinations worktypedestinations = new Destinations();
                worktypedestinations.Add(new Destination(@"[Label]:[Domain\Username]", DestinationType.User)); //e.g. K2:DOMAIN\Username 
                //Link the filters and destinations to the Work 
                WorkType worktype = new WorkType("MyWork", worklistcriteria, worktypedestinations);

                //optional: define a separate set of criteria for any "Exception" tasks that
                //must be delegated to a different user, in this sample, tasks for a specific workflow
                WorklistCriteria worklistexceptioncriteria = new WorklistCriteria();
                worklistexceptioncriteria.Platform = "ASP";
                worklistexceptioncriteria.AddFilterField(WCField.ProcessName, WCCompare.Equal, "[ExceptionProcessName]");
                //Define the user that the exception work must be delegated to as a Destination 
                Destinations worktypeexceptiondestinations = new Destinations();
                worktypedestinations.Add(new Destination(@"[Label]:[DOMAIN\ExceptionUserName]", DestinationType.User));
                // Link the filters and destinations to the Exception Work 
                WorkTypeException worktypeexception = new WorkTypeException("MyWorkException", worklistexceptioncriteria, worktypeexceptiondestinations);
                worktype.WorkTypeExceptions.Add(worktypeexception);

                //now set up the worklist Out of Office (OOF) delegation
                WorklistShare worklistshare = new WorklistShare();
                worklistshare.ShareType = ShareType.OOF;

                //Set time interval for the Sharing. These dates may be used to only share work for a specific period 
                worklistshare.StartDate = DateTime.MinValue;
                worklistshare.EndDate = DateTime.MinValue;

                //add the worktype
                worklistshare.WorkTypes.Add(worktype);
                //Share the worklist 
                K2Conn.ShareWorkList(worklistshare);
                //Share the worklist for a managed user
                K2Conn.ShareWorkList(@"[Label]:[Domain\Username]", worklistshare);

                //When the current user's status is set to OOF the worklist shares defined above will take effect
                K2Conn.SetUserStatus(UserStatuses.OOF);
            }
        }
    }
}
