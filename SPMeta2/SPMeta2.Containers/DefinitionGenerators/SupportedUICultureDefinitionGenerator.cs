﻿using System;
using SPMeta2.Containers.Services;
using SPMeta2.Containers.Services.Base;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Base;

namespace SPMeta2.Containers.DefinitionGenerators
{
    public class SupportedUICultureDefinitionGenerator :
        TypedDefinitionGeneratorServiceBase<SupportedUICultureDefinition>
    {
        public override DefinitionBase GenerateRandomDefinition(Action<DefinitionBase> action)
        {
            return WithEmptyDefinition(def =>
            {
                //- German
                //- French
                //- Dutch
                //- Italian
                //- Russian
                //- Spanish 
                //- Swedish 
                var languages = new int[]
                {
                    1031,
                    1036,
                    1043,
                    1040,
                    1049,
                    1034,
                    1053
                };

                def.LCID = Rnd.RandomFromArray(languages);
            });
        }
    }
}
