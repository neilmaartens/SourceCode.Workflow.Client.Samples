using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    class Delegated_and_OOO_WorklistItems
    {
        /// <summary>
        /// shows how to open a worklist item from a managed user's (i.e. subordinate's) tasklist
        /// </summary>
        public static void ManagedUser ()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //get the task serial number from somewhere (e.g. query string or worklist)
                string serialNumber = "[ProcessInstanceId_ActivityInstanceDestinationId]";

                //if you want to open a worklist item from a managed user's (i.e. subordinate's) tasklist
                //use the OpenManagedWorklistItem method as pass the managed user's username
                WorklistItem K2WListItemManagedUser = K2Conn.OpenManagedWorklistItem("[managedUserUsername]", serialNumber);
            }
        }

        /// <summary>
        /// Hows how to open delegated tasks or OOO tasks 
        /// </summary>
        public static void DelegatedUser()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //get the task serial number from somewhere (e.g. query string or worklist)
                string serialNumber = "[ProcessInstanceId_ActivityInstanceDestinationId]";

                //if you are opening another user's worklist item that was manually delegated to the current account, or 
                //delegated with Out of Office, use the OpenSharedWorklistItem method and pass the original user's username
                //leaving the managed user value empty
                
                //if you do not know the original user name, you can obtain it by
                //retrieving the task using the worklist and querying the AllocatedUser property
                //string originalUser = K2WLItem.AllocatedUser;

                WorklistItem K2WListItemDelegated = K2Conn.OpenSharedWorklistItem("[originalUserName]", string.Empty, serialNumber);
            }
        }

        /// <summary>
        /// Shows how to open tasks for a delegated managed user
        /// </summary>
        public static void DelegatedManagedUser()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                K2Conn.Open("localhost");

                //get the task serial number from somewhere (e.g. query string or worklist)
                string serialNumber = "[ProcessInstanceId_ActivityInstanceDestinationId]";

                //if you are opening another user's worklist item that was delegated to the current account 
                //or delegated with Out of Office and also uses the managed user value
                //use the OpenSharedWorklistItem method and pass the original user's username
                //as well as managed user

                //if you do not know the original user name, you can obtain it by
                //retrieving the task using the worklist and querying the AllocatedUser property
                //string originalUser = K2WLItem.AllocatedUser;

                WorklistItem K2WListItemDelegatedManagedUser = K2Conn.OpenSharedWorklistItem("[originalUserName]", "[managedUserUsername]", serialNumber);
            }

        }
    }
}