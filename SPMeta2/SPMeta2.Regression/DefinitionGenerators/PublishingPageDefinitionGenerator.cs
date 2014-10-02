﻿using SPMeta2.BuiltInDefinitions;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Regression.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SPMeta2.Syntax.Default;

namespace SPMeta2.Regression.DefinitionGenerators
{
    public class PublishingPageDefinitionGenerator : TypedDefinitionGeneratorServiceBase<PublishingPageDefinition>
    {
        public override DefinitionBase GenerateRandomDefinition(Action<DefinitionBase> action)
        {
            return WithEmptyDefinition(def =>
            {
                def.Title = Rnd.String();
                def.FileName = Rnd.String() + ".aspx";

                def.Description = Rnd.String();
                def.PageLayoutFileName = BuiltInPublishingPageLayoutNames.ArticleLeft;

                def.NeedOverride = true;
            });
        }

        public override IEnumerable<DefinitionBase> GetAdditionalArtifacts()
        {
            var sitePublishing = BuiltInSiteFeatures.SharePointServerPublishingInfrastructure
                               .Inherit(f =>
                               {
                                   f.Enable = true;
                               });

            var webPublishing = BuiltInWebFeatures.SharePointServerPublishing
                              .Inherit(f =>
                              {
                                  f.Enable = true;
                              });

            return new[] { sitePublishing, webPublishing };
        }

        public override DefinitionBase GetCustomParenHost()
        {
            return BuiltInListDefinitions.Pages.Inherit<ListDefinition>(def =>
            {
                def.RequireSelfProcessing = false;
            });
        }
    }
}