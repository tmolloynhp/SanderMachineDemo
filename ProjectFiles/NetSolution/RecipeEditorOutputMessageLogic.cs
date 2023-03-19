#region StandardUsing
using System;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Recipe;
using FTOptix.OPCUAServer;
using FTOptix.Store;
using System.Timers;
using FTOptix.Report;
using FTOptix.EventLogger;
#endregion

/*
 * NetLogic used to display the result of an operation performed through the recipe editor.
 */

public class RecipeEditorOutputMessageLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        messageVariable = Owner.Children.Get<IUAVariable>("Message");
        if (messageVariable == null)
            throw new ArgumentNullException("Unable to find variable Message in OutputMessage label");
    }

	public override void Stop()
    {
        lock (lockObject)
        {
            task?.Dispose();
        }
	}

	[ExportMethod]
	public void SetOutputMessage(string message)
	{
        lock (lockObject)
        {
            task?.Dispose();

            messageVariable.Value = message;
            task = new DelayedTask(() => { messageVariable.Value = ""; }, 5000, LogicObject);
            task.Start();
        }
	}

	DelayedTask task;
	IUAVariable messageVariable;
    object lockObject = new object();
}
