# SourceCode.Workflow.Client.Samples
This project contains sample code that demonstrates common uses of the SourceCode.Workflow.Client.dll API to interact with K2 workflows at runtime. 
Examples: 
* Starting a workflow
* retrieving workflow tasks/worklist items
* completing a workflwo task/worklist item
* working with comments and attachments 

### Prerequisites
The sample code has the following dependencies: 
* .NET Assemblies: 
** SourceCode.Workflow.Client.dll 
** SourceCode.HostClientAPI.dll 
(Both assemblies are included with K2 client-side tools install) 

### Getting started
* Use these code snippets to learn how to perform common tasks with k2 workflows through the workflow client API. 
* Note that these projects may compile, but will not actually run as-is, since they are intended as sample code only. You will need to edit the code snippets to work in your enviornment and with your artifacts. 
* Download the appropriate branch of this project for your version of K2 

**Getting started with Git and GitHub**

 * People new to GitHub should consider using [GitHub for Windows](http://windows.github.com/).
 * If you decide not to use GHFW you will need to:
  1. [Set up Git and connect to GitHub](http://help.github.com/win-set-up-git/)
  2. [Fork the SourceCode.Workflow.Client.Samples repository for your version of K2](http://help.github.com/fork-a-repo/)
 * Finally you should look into [git - the simple guide](http://rogerdudler.github.com/git-guide/)

**Rules for Our Git Repository**

 * We use ["A successful Git branching model"](http://nvie.com/posts/a-successful-git-branching-model/). What this means is that:
   * You need to branch off of the [develop branch](https://github.com/k2workflow/Clay) when creating new features or non-critical bug fixes.
   * Each logical unit of work must come from a single and unique branch:
     * A logical unit of work could be a set of related bugs or a feature.
     * You should wait for us to accept the pull request (or you can cancel it) before committing to that branch again.
     
### License

This project is licensed under the MIT license, which can be found in LICENSE.

**Additional Restrictions**

 * We only accept code that is compatible with the MIT license (essentially, MIT and Public Domain).
 * Copying copy-left (GPL-style) code is strictly forbidden.
