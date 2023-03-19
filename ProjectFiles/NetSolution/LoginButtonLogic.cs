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
 * NetLogic used to perform the action of accessing a user, which returns the result. 
 */

public class LoginButtonLogic : FTOptix.NetLogic.BaseNetLogic
{
    [ExportMethod]
    public void PerformLogin(string username, string password, out bool loginResult)
    {
        try
        {
            loginResult = ChangeUser(username, password);
        }
        catch (Exception e)
        {
            Log.Error("LoginForm", e.Message);
            loginResult = false;
        }
    }

    private bool ChangeUser(string username, string password)
    {
        var coreCommandsNodeId = FTOptix.CoreBase.Objects.CoreCommands;
        var coreCommandsObject = LogicObject.Context.GetObject(coreCommandsNodeId);
        object[] outputArgs;
        coreCommandsObject.ExecuteMethod("ChangeUser", new object[] { username, password }, out outputArgs);
        return (bool)outputArgs[0];
    }
}
