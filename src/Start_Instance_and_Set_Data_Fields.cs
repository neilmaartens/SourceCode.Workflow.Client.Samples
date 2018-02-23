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
    class Start_Instance_and_Set_Data_Fields
    {
        /// <summary>
        /// shows how to start a new workflow instance and set data values while doing so
        /// </summary>
        public void Start_Workflow_Instance()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used, for simplicity
                K2Conn.Open("localhost");

                //create a new process instance object, using the full name of the workflow
                SourceCode.Workflow.Client.ProcessInstance K2Proc = K2Conn.CreateProcessInstance(@"[project]\[folder]\[workflowname]"); //TODO: determine your full workflow name

                //Set some properties for the process instance
                //setting the folio
                K2Proc.Folio = "ProcessFolio";
                //setting datafields (datafields are accessed and set by name.) 
                //Take care to specify a value of the correct data type for the targeted field 
                K2Proc.DataFields["StringDataField"].Value = "somevalue";
                K2Proc.DataFields["IntegerDatafield"].Value = 1;
                //XML fields are set using a XML-formatted string
                System.Xml.XmlDocument xmlDoc = new XmlDocument();
                //do some work with the XML document
                //pass the XML document to the XML field as a string
                K2Proc.XmlFields["XMLDataField"].Value = xmlDoc.ToString();

                //you can iterate over the data fields collection as well
                foreach (DataField dataField in K2Proc.DataFields)
                {
                    //do something with each datafield, e.g. read name or set value
                    string fieldName = dataField.Name;
                    string fieldValue = dataField.Value.ToString();
                }

                //Start the process instance
                K2Conn.StartProcessInstance(K2Proc);

                //Starting a process instance synchronously (in other words, do not return the call until the first wait-state is reached in the workflow) 
                K2Conn.StartProcessInstance(K2Proc, true);

                //If needed, read the resulting process instance ID
                int ProcessInstanceId = K2Proc.ID;
            }
        }
    }
}
