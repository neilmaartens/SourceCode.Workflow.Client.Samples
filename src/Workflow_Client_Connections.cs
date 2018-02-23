using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;

namespace SourceCode.Workflow.Client.Samples
{
    public class Workflow_Client_Connections
    {

        /// <summary>
        /// shows how to establish a simple connection with current user's Windows credentials
        /// </summary>
        private void SimpleConnection()
        {
            //simple usage of SourceCode.Workflow.Client.Connection
            SourceCode.Workflow.Client.Connection WorkflowClientConnection = new SourceCode.Workflow.Client.Connection();
            WorkflowClientConnection.Open("[ServerName]");
            
            //do things with the connection

            //connection must be closed once you are done
            WorkflowClientConnection.Close();
        }

        /// <summary>
        /// shows how to establish a connection with a SQL UM user
        /// </summary>
        private void SQLUMConnection()
        {
            //Make a connection that authenticates against the K2 SQL User Manager
            SourceCode.Hosting.Client.BaseAPI.SCConnectionStringBuilder builder = new SCConnectionStringBuilder();
            builder.Authenticate = true;
            builder.Host = "localhost"; //server name of the K2 host server
            builder.Port = 5555; //use port 5252 for SourceCode.Workflow.Client connections
            builder.Integrated = false;
            builder.IsPrimaryLogin = true;
            builder.SecurityLabelName = "K2SQL"; //the name of the security label to use for authenticating the credentials below. note K2SQL is used here
            builder.UserID = "username"; //user name to be authenticated
            builder.Password = "password"; //password for the user to be authenticated
            //open the connection
            SourceCode.Workflow.Client.Connection WorkflowClientConnection = new SourceCode.Workflow.Client.Connection();
            WorkflowClientConnection.Open("[ServerName]", builder.ToString());
            //do things with the connection
            //connection must be closed once you are done
            WorkflowClientConnection.Close();
        }

        /// <summary>
        /// shows how to use ConnectionSetup to construct connection settings
        /// </summary>
        private void ConnectionSetupSample()
        {
            SourceCode.Workflow.Client.Connection WorkflowClientConnection = new SourceCode.Workflow.Client.Connection();
            SourceCode.Workflow.Client.ConnectionSetup connectionSetup = new SourceCode.Workflow.Client.ConnectionSetup();
            connectionSetup.ConnectionParameters["Authenticate"] = "true"; //whether to authenticate the user"s credentials against the security provider. Usually true
            connectionSetup.ConnectionParameters["Host"] = "localhost"; //name of the K2 server
            connectionSetup.ConnectionParameters["Port"] = "5252"; //port for workflow client communication (usually 5252) 
            connectionSetup.ConnectionParameters["SecurityLabelName"] = "K2"; //the security provider label to use for the credentials passed in
            connectionSetup.ConnectionParameters["Integrated"] = "true"; //If true: use the logged on user. If false: use the specified username and password provided in the connection string.
            connectionSetup.ConnectionParameters["IsPrimaryLogin"] = "true"; //normally set to true, unless you are using cached security credentials. true: re-authenticate user. false: use cached security credentials if available.
            connectionSetup.ConnectionParameters["WindowsDomain"] = "[domain]"; //if this is a windows credentials, set the windows domain
            connectionSetup.ConnectionParameters["UserID"] = "[username]"; //pass username in
            connectionSetup.ConnectionParameters["Password"] = "[password]"; //pass password in
            WorkflowClientConnection.Open(connectionSetup);
        }

        /// <summary>
        /// shows different ways of managing workflow connections to ensure they are closed and disposed
        /// </summary>
        private void ManagingConnections()
        {
            //manually closing the connection
            SourceCode.Workflow.Client.Connection WorkflowClientConnection = new SourceCode.Workflow.Client.Connection();
            WorkflowClientConnection.Open("[ServerName]");
            //do something with the connection
            //close the connection when it is no longer needed
            WorkflowClientConnection.Close();

            //using a try..catch..finally block
            SourceCode.Workflow.Client.Connection WorkflowClientConnection1 = new SourceCode.Workflow.Client.Connection();
            WorkflowClientConnection1.Open("[ServerName]");
            try
            {
                //do something with the connection
            }
            catch
            {
                //do something with exceptions
            }
            finally
            {
                //make sure the connection is closed when it is no longer needed
                WorkflowClientConnection1.Close();
            }

            //using a using block will call close and dispose when the using statement exits
            using (SourceCode.Workflow.Client.Connection WorkflowClientConnection2 = new SourceCode.Workflow.Client.Connection())
            {
                WorkflowClientConnection2.Open("localhost");
                //do something with connection once opened
            }
        }
    }
}
