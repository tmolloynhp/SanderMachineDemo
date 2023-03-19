#region StandardUsing
using System;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.OPCUAServer;
using FTOptix.UI;
using FTOptix.Retentivity;
using FTOptix.Report;
using FTOptix.EventLogger;
#endregion

/*
 * NetLogic used by the user editor to display a page given the number of existing users.
 * If there is at least one user, the UserDetailPanel is shown, otherwise the NoUserDetailPanel.
 */

public class UserEditorPanelLoaderLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
    }

    public override void Stop()
    {
    }

	[ExportMethod]
	public void GoToUserDetailsPanel()
	{
		var userCountVariable = LogicObject.Get<IUAVariable>("UserCount");
		if (userCountVariable == null)
			return;

		var noUsersPanelVariable = LogicObject.Get<IUAVariable>("NoUsersPanel");
		if (noUsersPanelVariable == null)
			return;

		var userDetailPanelVariable = LogicObject.Get<IUAVariable>("UserDetailPanel");
		if (userDetailPanelVariable == null)
			return;

		var panelLoader = (PanelLoader)Owner;

		NodeId newPanelNodeId = userCountVariable.Value > 0 ? userDetailPanelVariable.Value : noUsersPanelVariable.Value;
		panelLoader.ChangePanel(newPanelNodeId, NodeId.Empty);
	}
}
