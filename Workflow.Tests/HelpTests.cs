﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Chauffeur;
using Chauffeur.TestingTools;
using Xunit;

namespace Workflow.Tests
{
    public class HelpTests : UmbracoHostTestBase
    {
        [Fact]
        public async Task Help_Will_Be_Successful()
        {
            DeliverableResponse result = await Host.Run(new[] { "help" });

            Assert.Equal(DeliverableResponse.Continue, result);
        }
    }
}