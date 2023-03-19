#region StandardUsing
using System;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Store;
using FTOptix.SQLiteStore;
using FTOptix.Retentivity;
using FTOptix.Recipe;
using FTOptix.OPCUAServer;
using FTOptix.Alarm;
using FTOptix.Report;
using FTOptix.EventLogger;
#endregion

/*
 * Logic that displays an error message for 5 seconds in case of incorrect login.
 */

public class IncorrectLoginMessageLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        loginPopup = (Popup)Owner.Owner.Owner;
        incorrectLoginMessageLabel = (FTOptix.UI.Label)Owner;

        task = new DelayedTask(() =>
        {
            incorrectLoginMessageLabel.Visible = false;
        }, 5000, LogicObject);
    }

    public override void Stop()
    {
        task?.Dispose();
    }

    [ExportMethod]
    public void DecideActionBasedOnLoginResult(bool loginSucceded)
    {
        if (loginSucceded)
            loginPopup.Close();
        else
            ViewIncorrectLoginMessage();
    }

    private void ViewIncorrectLoginMessage()
    {
        incorrectLoginMessageLabel.Visible = true;

        if (taskStarted)
        {
            task?.Cancel();
            taskStarted = false;
        }

        task.Start();
        taskStarted = true;
    }

    DelayedTask task;
    bool taskStarted = false;
    FTOptix.UI.Label incorrectLoginMessageLabel;
    Popup loginPopup;
}
