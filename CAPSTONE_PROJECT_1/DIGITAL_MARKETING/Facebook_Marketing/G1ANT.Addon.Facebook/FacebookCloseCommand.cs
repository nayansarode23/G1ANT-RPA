﻿using G1ANT.Language;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Facebook
{
    [Command(Name = "facebook.close", Tooltip = "This command is use to closes a web browser in which Facebook is opened")]
    public class FacebookCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }
        public FacebookCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                SeleniumManager.QuitCurrentWrapper();

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while closing selenium instance. Message: {ex.Message}", ex);
            }
        }
    }
}