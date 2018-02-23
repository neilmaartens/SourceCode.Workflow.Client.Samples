using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;
using System.Xml;


namespace SourceCode.Workflow.Client.Samples
{
    class Asynchronous_Event
    {
        /// <summary>
        /// Shows how to use asynchronous server events
        /// </summary>
        private void Asynchronous_Event_Sample()
        {
            ////In your Server Event, write a line to set event synchronous = false. 
            /////this tells K2 to wait for a client connection to complete the server event
            //K2.Synchronous = false;
            ////get the serial number, because you will need to pass it to the external system so it can use the serial number to complete the server event
            //string serialNo = K2.SerialNumber;
            ////TODO: pass the server item serial number to the other system 
            ////think of this as the "correlation id" the other system will use to complete the server item later on

            //fron the externla system, open a client connection and complete the waiting server event
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //open a simple connection for simplicity
                //note: the account executing this code will require "Server Event" rights in the workflow
                K2Conn.Open("k2servername");
                ServerItem svrItem = K2Conn.OpenServerItem("[serialNo]");
                //TODO: do something with the server item, like updating a datafield
                svrItem.ProcessInstance.DataFields["[StringFieldName]"].Value = "somevalue";
                //finish the server item to tell the workflow to continue 
                svrItem.Finish();
            }
        }
    }
}
