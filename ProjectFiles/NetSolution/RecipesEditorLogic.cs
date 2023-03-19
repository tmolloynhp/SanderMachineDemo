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
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.OPCUAServer;
using FTOptix.Retentivity;
using FTOptix.Report;
using FTOptix.EventLogger;
#endregion

/*
 * Logic linked to the recipe editor and allows you to copy a recipe to the EditModel the first time you load the panel.
 */

public class RecipesEditorLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        var recipeSchemaPtr = Owner.Children.Get<IUAVariable>("RecipeSchema");
        if (recipeSchemaPtr == null)
            return;

        var nodeId = (NodeId)recipeSchemaPtr.Value;
        if (nodeId == null)
            return;

        var recipeSchema = LogicObject.Context.GetObject(nodeId);
        if (recipeSchema == null)
            return;

        var schema = recipeSchema as RecipeSchema;
        if (schema == null)
            return;

        var targetNode = schema.Children.Get<IUAVariable>("TargetNode");
        if (targetNode == null)
            return;

        if ((NodeId)targetNode.Value == NodeId.Empty)
            return;

        var target = LogicObject.Context.GetNode(targetNode.Value);
        if (target == null)
            return;

        schema.Copy(target.NodeId, schema.GetObject("EditModel").NodeId, CopyErrorPolicy.BestEffortCopy);
    }

    public override void Stop()
    {
        
    }

}
