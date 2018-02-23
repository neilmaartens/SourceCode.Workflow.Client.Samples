using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Client;
using System.IO;

namespace SourceCode.Workflow.Client.Samples
{
    class Comments_and_Attachments
    {

        /// <summary>
        /// shows how to add a comment
        /// </summary>
        public void AddComment()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Add a Comment using the Connection class and the Process Instance ID 
                IWorkflowComment comment = K2Conn.AddComment(_worklistItem.ProcessInstance.ID, "Hello World");
                
                //Add a Comment using the Connection class and the Worklist Item SerialNumber 
                IWorkflowComment comment2 = K2Conn.AddComment("[SerialNumber]", "Hello World");
                
                //Add a Comment using the Connection class and the Process Instance ID and Activity Instance Destination ID 
                IWorkflowComment comment3 = K2Conn.AddComment(_worklistItem.ProcessInstance.ID, _worklistItem.ActivityInstanceDestination.ID, "Hello World");
                
                //Add a Comment using the Process Instance _processInstance = cn.OpenProcessInstance(_processinstanceID);
                IWorkflowComment procInstComment = _worklistItem.ProcessInstance.AddComment("Hello World");
                
                //Add a Comment using the Worklistltem _worklistItem = cn.OpenWorklistItem(_serialNo); 
                IWorkflowComment WLItemComment = _worklistItem.AddComment("Hello World");
            }
        }

        /// <summary>
        /// shows how to retrieve comments
        /// </summary>
        public void RetrieveComments()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Get all the Comments using the Process Instance ID 
                IEnumerable<IWorkflowComment> comments1 = K2Conn.GetComments(_worklistItem.ProcessInstance.ID);

                //Get all the Comments using a Worklist Item SerialNumber 
                IEnumerable<IWorkflowComment> comments2 = K2Conn.GetComments("[SerialNumber]");

                //Get all the Comments using a Process Instance ID and Activity Instance Destination"s ID 
                IEnumerable<IWorkflowComment> comments3 = K2Conn.GetComments(_worklistItem.ProcessInstance.ID, _worklistItem.ActivityInstanceDestination.ID);

                //Get all the Comments from the Process Instance Comments
                IEnumerable<IWorkflowComment> procInstComments = _worklistItem.ProcessInstance.Comments;
                foreach (IWorkflowComment procInstComment in procInstComments)
                {
                    //do something with the comment value
                    string comment = procInstComment.Message;
                }

                //Get all the Comments from the Worklist Item Comments
                IEnumerable<IWorkflowComment> wlItemComments = _worklistItem.Comments;
                foreach (IWorkflowComment wlItemComment in wlItemComments)
                {
                    //do something with the comment value
                    string comment = wlItemComment.Message;
                }
            }

        }

        /// <summary>
        /// shows how to add an attachment
        /// </summary>
        public void AddAttachments()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //TODO: stream in the file you want to add. 
                //in this example we're loading from the file system
                System.IO.Stream _fileStream = null;
                string _fileName = "[FileName.doc]";
                string fullPath = string.Format(@"C:\Temp\{0}", _fileName);
                //Check if the file exists at the location.
                if (File.Exists(fullPath))
                {
                    //Get the FileStream
                    _fileStream = File.OpenRead(fullPath);
                }

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Add an Attachment using the Connection class and the Process Instance ID 
                IWorkflowAttachment attachment1 = K2Conn.AddAttachment(_worklistItem.ProcessInstance.ID, _fileName, _fileStream);

                //Add an Attachment using the Connection class and the Worklist Item SerialNumber 
                IWorkflowAttachment attachment2 = K2Conn.AddAttachment("[SerialNumber]", _fileName, _fileStream);

                //Add an Attachment using the Connection class and the Process Instance ID and Activity Instance Destination ID 
                IWorkflowAttachment attachment3 = K2Conn.AddAttachment(_worklistItem.ProcessInstance.ID, _worklistItem.ActivityInstanceDestination.ID, _fileName, _fileStream);

                //Add an Attachment using the Process Instance 
                IWorkflowAttachment ProcInstAttachment = _worklistItem.ProcessInstance.AddAttachment(_fileName, _fileStream);

                //Add an Attachment using the Worklistltem 
                IWorkflowAttachment WLItemAttachment = _worklistItem.AddAttachment(_fileName, _fileStream);
            }

        }

        /// <summary>
        /// shows how to add an attachment asynchronously 
        /// </summary>
        public void UploadAttachmentAsync()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //TODO: stream in the file you want to add. 
                //in this example we're loading from the file system
                System.IO.Stream _fileStream = null;
                string _fileName = "[FileName.doc]";
                string fullPath = string.Format(@"C:\Temp\{0}", _fileName);
                //Check if the file exists at the location.
                if (File.Exists(fullPath))
                {
                    //Get the FileStream
                    _fileStream = File.OpenRead(fullPath);
                }

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Add an "empty" attachment without the file content by passing null as the content
                //This approach can be used for async purposes
                //to create the attachment metadata first, and then upload the file. 
                IWorkflowAttachment attachmentWithoutContent = K2Conn.AddAttachment(_worklistItem.ProcessInstance.ID, _fileName, null);
                //Upload the actual file content
                //Note: You can only upload the file once for an "empty" attachment. 
                IAttachment attachmentWithContent = K2Conn.UploadAttachmentContent(attachmentWithoutContent.Id, _fileStream); 
            }
        }

        /// <summary>
        /// shows how to retrieve attachments
        /// </summary>
        public void RetrieveAttachments()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Get all the Attachments using the Process Instance ID 
                //By default this call will always return the attachment files. 
                IEnumerable<IWorkflowAttachment> attachments1 = K2Conn.GetAttachments(_worklistItem.ProcessInstance.ID);

                //Get all the Attachments using the SerialNumber 
                //By default this call will always return the attachment files. 
                IEnumerable<IWorkflowAttachment> attachments2 = K2Conn.GetAttachments("[SerialNumber]");

                //Get all the Attachments using the Process Instance ID and Activity Instance Destination ID 
                //By default this call will always return the attachment"s files. 
                IEnumerable<IWorkflowAttachment> attachments3 = K2Conn.GetAttachments(_worklistItem.ProcessInstance.ID, _worklistItem.ActivityInstanceDestination.ID);

                //Get all the Attachments from the Process Instance "Attachments" property 
                IEnumerable<IWorkflowAttachment> procInstAttachments = _worklistItem.ProcessInstance.Attachments;

                //Get all the Attachments from the Worklist Item "Attachments" property 
                _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");
                IEnumerable<IWorkflowAttachment> wlitemAttachments = _worklistItem.Attachments; 

                //once you have retrieved the attachments, you can iterate over them
                foreach (IWorkflowAttachment attachment in wlitemAttachments)
                {
                    string filename = attachment.FileName;
                }

            }
        }

        /// <summary>
        /// shows how to retrieve attachments without retrieving the actual file content (faster) 
        /// </summary>
        public void RetrieveAttachmentsExcludingFileContent()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                //get a handle on a process instance or worklist item. 
                //In this example we will open a worklist item 
                WorklistItem _worklistItem = K2Conn.OpenWorklistItem("[SerialNumber]");

                //Get all the Attachments using the Process Instance ID 
                //pass "false" for the includeFile parameter to only load the file on demand.
                IEnumerable<IWorkflowAttachment> attachments1 = K2Conn.GetAttachments(_worklistItem.ProcessInstance.ID, false);

                //Get all the Attachments using the SerialNumber 
                //pass "false" for the includeFile parameter to only load the file on demand.
                IEnumerable<IWorkflowAttachment> attachments2 = K2Conn.GetAttachments("[SerialNumber]", false);

                //Get all the Attachments using the Process Instance ID and Activity Instance Destination ID 
                //pass "false" for the includeFile parameter to only load the file on demand.
                IEnumerable<IWorkflowAttachment> attachments3 = K2Conn.GetAttachments(_worklistItem.ProcessInstance.ID, _worklistItem.ActivityInstanceDestination.ID, false);

                //once you have retrieved the attachments, you can iterate over them
                foreach (IWorkflowAttachment attachment in attachments3)
                {
                    string filename = attachment.FileName;
                    //use the attachment.GetFile() method to load the attachments file 
                    using (System.IO.Stream downloadStream = attachment.GetFile())
                    {
                        //do something with the downloaded filestream
                    }
                }
            }

        }

        /// <summary>
        /// shows how to retrieve an attachment's content by passing in the ID of the attachment 
        /// </summary>
        public void RetrieveAttachmentById()
        {
            using (SourceCode.Workflow.Client.Connection K2Conn = new Connection())
            {
                //simple connection used in this example
                K2Conn.Open("localhost");

                int _attachmentID = 1;
                //Get the Attachment by passing the Attachment ID 
                //By default, this call will always return the attachment file content. 
                IWorkflowAttachment attachment = K2Conn.GetAttachment(_attachmentID);

                //to load the file content on-demand, pass "false" for the includeFile parameter. 
                IWorkflowAttachment attachmentNoFile = K2Conn.GetAttachment(_attachmentID, false);
                //when ready, get the file content
                System.IO.Stream _fileStream = attachmentNoFile.GetFile();
            }
        }
    }
}
