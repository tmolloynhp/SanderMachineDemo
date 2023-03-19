#region StandardUsing
using System;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.OPCUAServer;
using FTOptix.Report;
using FTOptix.EventLogger;
#endregion

/*
 * NetLogic that creates the model for the combo box of the Locales extrapolating them from those defined on the project.
 */

public class LocaleComboBoxLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        var localeCombo = (ComboBox)Owner;

        var projectLocales = (string[])Project.Current.GetVariable("Locales").Value;
        var modelLocales = InformationModel.MakeObject("Locales");
        modelLocales.Children.Clear();

        foreach (var locale in projectLocales)
        {
            var language = InformationModel.MakeVariable(locale, OpcUa.DataTypes.String);
            language.Value = locale;
            modelLocales.Children.Add(language);
        }

        LogicObject.Children.Add(modelLocales);
        localeCombo.Model = modelLocales.NodeId;
    }

    public override void Stop()
    {
    }
}
