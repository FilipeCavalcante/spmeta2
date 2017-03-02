﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPMeta2.CSOM.ModelHandlers;
using SPMeta2.CSOM.Services;
using SPMeta2.CSOM.Standard.ModelHandlers.Fields;
using SPMeta2.CSOM.Standard.Services;
using SPMeta2.ModelHandlers;
using SPMeta2.Models;
using SPMeta2.Services;
using SPMeta2.SSOM.Services;
using SPMeta2.SSOM.Standard.Services;
using SPMeta2.Standard.Definitions.Taxonomy;
using SPMeta2.Standard.Syntax;
using SPMeta2.Syntax.Default;
using SPMeta2.Utils;
using SPMeta2.Interfaces;

namespace SPMeta2.Regression.Impl.Tests
{
    [TestClass]
    public class ModelHandlersTest
    {
        #region init

        [ClassInitialize]
        public static void Init(TestContext context)
        {

        }

        [ClassCleanup]
        public static void Cleanup()
        {

        }

        #endregion

        #region ProvisionServices

        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices")]
        [TestCategory("CI.Core")]
        public void Can_CreateFoundationProvisionService()
        {
            Assert.IsNotNull(new CSOMProvisionService());
            Assert.IsNotNull(new SSOMProvisionService());

        }

        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices")]
        [TestCategory("CI.Core")]
        public void Can_CreateStandardProvisionService()
        {
            Assert.IsNotNull(new StandardCSOMProvisionService());
            Assert.IsNotNull(new StandardSSOMProvisionService());

        }

        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices.CSOM")]
        [TestCategory("CI.Core")]
        public void EnsureCSOMModelHandlers()
        {
            var service = new CSOMProvisionService();
            var modelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.CSOM.ModelHandlers.FieldModelHandler).Assembly);

            foreach (var srcHandlerImplType in modelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }
        }

        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices.CSOM")]
        [TestCategory("CI.Core")]
        public void EnsureStandardCSOMModelHandlers()
        {
            var service = new StandardCSOMProvisionService();

            var modelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.CSOM.ModelHandlers.FieldModelHandler).Assembly);
            var standardModelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.CSOM.Standard.ModelHandlers.Fields.TaxonomyFieldModelHandler).Assembly);

            foreach (var srcHandlerImplType in modelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }

            foreach (var srcHandlerImplType in standardModelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }
        }


        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices.SSOM")]
        [TestCategory("CI.Core")]
        public void EnsureSSOMModelHandlers()
        {
            var service = new SSOMProvisionService();
            var modelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.SSOM.ModelHandlers.FieldModelHandler).Assembly);

            foreach (var srcHandlerImplType in modelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }
        }

        [TestMethod]
        [TestCategory("Regression.Impl.ProvisionServices.SSOM")]
        [TestCategory("CI.Core")]
        public void EnsureStandardSSOMModelHandlers()
        {
            var service = new StandardSSOMProvisionService();

            var modelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.SSOM.ModelHandlers.FieldModelHandler).Assembly);
            var standardModelHandlers = ReflectionUtils.GetTypesFromAssembly<ModelHandlerBase>(typeof(SPMeta2.SSOM.Standard.ModelHandlers.Fields.TaxonomyFieldModelHandler).Assembly);

            foreach (var srcHandlerImplType in modelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }

            foreach (var srcHandlerImplType in standardModelHandlers)
            {
                var dstHandlerImpl = service.ModelHandlers.Values.FirstOrDefault(h => h.GetType() == srcHandlerImplType);
                Assert.IsNotNull(dstHandlerImpl);
            }
        }

        #endregion

        #region incremental proivision services

        [TestMethod]
        [TestCategory("Regression.Impl.IncrementalProvisionService")]
        [TestCategory("CI.Core")]
        public void Can_Create_IncrementaProvisionServices()
        {
            var services = new List<ProvisionServiceBase>();

            services.Add(new CSOMIncrementalProvisionService());
            services.Add(new StandardCSOMIncrementalProvisionService());

            services.Add(new SSOMIncrementalProvisionService());
            services.Add(new StandardSSOMIncrementalProvisionService());

            foreach (var service in services)
            {
                var incrementalService = service as IIncrementalProvisionService;

                Assert.IsNotNull(incrementalService.PreviousModelHash);
                Assert.IsNotNull(incrementalService.CurrentModelHash);
            }
        }

        [TestMethod]
        [TestCategory("Regression.Impl.IncrementalProvisionService")]
        [TestCategory("CI.Core")]
        public void Can_Create_IncrementaProvisionServices_With_FluentAPI()
        {
            var services = new List<ProvisionServiceBase>();

            services.Add(new CSOMProvisionService());
            services.Add(new StandardCSOMProvisionService());

            services.Add(new SSOMProvisionService());
            services.Add(new StandardSSOMProvisionService());

            foreach (var service in services)
            {
                var incrementalService = service.SetIncrementalMode();

                var currentModelHash = incrementalService.GetIncrementalModelHash();

                Assert.IsNotNull(currentModelHash);
            }
        }

        #endregion
    }
}
