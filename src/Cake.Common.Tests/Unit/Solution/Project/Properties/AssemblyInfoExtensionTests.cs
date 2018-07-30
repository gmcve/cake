﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Common.Solution.Project.Properties;
using Cake.Common.Tests.Fixtures;
using Xunit;

namespace Cake.Common.Tests.Unit.Solution.Project.Properties
{
    public sealed class AssemblyInfoExtensionTests
    {
        [Fact]
        public void Should_Add_CustomAttributes_If_Set()
        {
            // Given
            var fixture = new AssemblyInfoFixture();
            fixture.Settings.AddCustomAttribute("TestAttribute", "Test.NameSpace", "TestValue");

            // When
            var result = fixture.CreateAndReturnContent();

            // Then
            Assert.Contains("using Test.NameSpace;", result);
            Assert.Contains("[assembly: TestAttribute(\"TestValue\")]", result);
        }

        [Fact]
        public void Should_Add_MetadataAttributes_If_Set()
        {
            // Given
            var fixture = new AssemblyInfoFixture();
            fixture.Settings.AddMetadataAttribute("Key1", "TestValue1");
            fixture.Settings.AddMetadataAttribute("Key2", "TestValue2");
            fixture.Settings.AddMetadataAttribute("Key1", "TestValue3");

            // When
            var result = fixture.CreateAndReturnContent();

            // Then
            Assert.Contains("using System.Reflection;", result);
            Assert.Contains("[assembly: AssemblyMetadata(\"Key1\", \"TestValue3\")]", result);
            Assert.Contains("[assembly: AssemblyMetadata(\"Key2\", \"TestValue2\")]", result);
            Assert.DoesNotContain("[assembly: AssemblyMetadata(\"Key1\", \"TestValue1\")]", result);
        }
    }
}