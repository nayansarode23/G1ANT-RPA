﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;
using OpenQA.Selenium;

namespace G1ANT.Addon.Messenger
{
    [Command(Name = "messenger.open", Tooltip = "This command opens a new instance of a chosen web browser and open Messenger in it.")]
    class MessengerOpenCommand : Command
    {
        public MessengerOpenCommand(AbstractScripter scripter) : base(scripter)
        {

        }
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Name of a web browser")]
            public TextStructure Type { get; set; } = new TextStructure("chrome");

            [Argument(DefaultVariable = "Url", Tooltip = "Website Url")]
            public TextStructure Url { get; set; } = new TextStructure("www.messenger.com");

            [Argument(DefaultVariable = "timeoutselenium", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(SeleniumSettings.SeleniumTimeout);

            [Argument(Tooltip = "By default, waits until the webpage fully loads")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "Result variable")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                SeleniumWrapper wrapper = SeleniumManager.CreateWrapper(
                    arguments.Type.Value,
                    arguments.Url?.Value,
                    arguments.Timeout.Value,
                    arguments.NoWait.Value,
                    Scripter.Log,
                    Scripter.Settings.UserDocsAddonFolder.FullName);
                int wrapperId = wrapper.Id;
                OnScriptEnd = () =>
                {
                    SeleniumManager.DisposeAllOpenedDrivers();
                    SeleniumManager.RemoveWrapper(wrapperId);
                    SeleniumManager.CleanUp();
                };
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(wrapper.Id));
                Thread.Sleep(5000);
            }
            catch (DriverServiceNotFoundException ex)
            {
                throw new ApplicationException("Driver not found", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while opening new selenium instance. Url '{arguments.Url.Value}'. Message: {ex.Message}", ex);
            }

        }


    }
}
