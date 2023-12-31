﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language;


namespace G1ANT.Addon.Swiggy
{
    [Command(Name = "swiggy.login", Tooltip = "This command opens swiggy in a web browser provided in the Argument.")]
    public class swiggyloginCommand : Command
    {
        public swiggyloginCommand(AbstractScripter scripter) : base(scripter)
        {

        }
        public class Arguments : SeleniumCommandArguments
        {
            [Argument(Required = true, Tooltip = "Enter the phone number here")]
            public TextStructure phnnumber { get; set; }

           
            [Argument(DefaultVariable = "timeoutselenium", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(SeleniumSettings.SeleniumTimeout);
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                SeleniumWrapper wrapper = SeleniumManager.CreateWrapper(
                        "chrome",
                        "swiggy.com",
                        arguments.Timeout.Value,
                        false,
                        Scripter.Log,
                        Scripter.Settings.UserDocsAddonFolder.FullName);
                int wrapperId = wrapper.Id;
                OnScriptEnd = () =>
                {
                    SeleniumManager.DisposeAllOpenedDrivers();
                    SeleniumManager.RemoveWrapper(wrapperId);
                    SeleniumManager.CleanUp();
                };
                arguments.Search.Value = "/html/body/div[1]/div[1]/div[1]/div/div[1]/div[1]/div/div[1]/div/a[1]";
                arguments.By.Value = "xpath";
                SeleniumManager.CurrentWrapper.Click(arguments, arguments.Timeout.Value);
                arguments.Search.Value = "mobile";
                arguments.By.Value = "id";
                SeleniumManager.CurrentWrapper.TypeText(arguments.phnnumber.Value, arguments, arguments.Timeout.Value);
                
                SeleniumManager.CurrentWrapper.PressKey("enter", arguments, arguments.Timeout.Value);
                RobotMessageBox.Show("Please enter the OTP generated on your phone ");
                var len = SeleniumManager.CurrentWrapper.RunScript("return document.getElementsByClassName(\"captcha -internal\").length");
                if (len == "1")
                {
                    RobotMessageBox.Show("Captcha detected, please solve the captcha");
                }
            }

            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while opening new selenium instance. Message: {ex.Message}", ex);
            }
        }
    }


}
