using System;
using FTOptix.Core;
using CoreBase = FTOptix.CoreBase;
using HMIProject = FTOptix.HMIProject;
using UAManagedCore;
using FTOptix.UI;
using FTOptix.Report;
using FTOptix.EventLogger;

/*
 * Library script that updates a model variable with the current time every one second.
 */

public class ClockLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    private void UpdateTime()
    {
        var timeVar = LogicObject.Children.Get<IUAVariable>("Time");
        timeVar.Value = DateTime.UtcNow;
    }

    private PeriodicTask periodicTask;
}
