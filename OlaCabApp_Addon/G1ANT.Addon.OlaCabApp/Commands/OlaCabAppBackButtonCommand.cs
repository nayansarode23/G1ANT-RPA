﻿using System;
using System.Threading;
using G1ANT.Language;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.OlaCabApp
{
    [Command(Name = "olacabapp.backbutton", Tooltip = "This command clicks on a back button of android phone.")]
    public class OlaCabAppBackButtonCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Keycode of the button to be pressed")]
            public TextStructure KeyCode { get; set; } = new TextStructure("back");
        }

        public OlaCabAppBackButtonCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var driver = OpenCommand.GetDriver();
            string keycode = arguments.KeyCode.Value.ToLower();

            switch (keycode)
            {
                case "back":
                    driver.PressKeyCode(AndroidKeyCode.Back);
                    break;
                default:
                    throw new ArgumentException($"Provided button name is invalid.");
            }
            Thread.Sleep(2000);
        }
    }
}
