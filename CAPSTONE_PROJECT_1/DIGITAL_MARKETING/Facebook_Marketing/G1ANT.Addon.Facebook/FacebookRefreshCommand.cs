﻿using G1ANT.Language;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Facebook
{
    [Command(Name = "facebook.refresh", Tooltip = "This command refreshes the current tab content in the web browser")]
    public class FacebookRefreshCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }
        public FacebookRefreshCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                SeleniumManager.CurrentWrapper.Refresh();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while refreshing selenium instance. Message: {ex.Message}", ex);
            }
        }
    }
}