﻿using System;
using Moq;
using PactNet.Verifier;
using Xunit;

namespace PactNet.Tests.Verifier
{
    public class PactUriOptionsTests
    {
        private static readonly Uri Uri = new Uri("https://example.org/pact.json");

        private readonly PactUriOptions options;

        private readonly Mock<IVerifierProvider> mockProvider;

        public PactUriOptionsTests()
        {
            this.mockProvider = new Mock<IVerifierProvider>(MockBehavior.Strict);

            this.options = new PactUriOptions(this.mockProvider.Object, Uri);
        }

        [Fact]
        public void Apply_NoAuthentication_SetsNoCredentials()
        {
            this.mockProvider
                .Setup(p => p.AddUrlSource(Uri, null, null, null))
                .Verifiable();

            this.options.Apply();

            this.mockProvider.Verify();
        }

        [Fact]
        public void Apply_BasicAuthentication_SetsBasicCredentials()
        {
            this.mockProvider
                .Setup(p => p.AddUrlSource(Uri, "user", "pass", null))
                .Verifiable();

            this.options.BasicAuthentication("user", "pass");
            this.options.Apply();

            this.mockProvider.Verify();
        }

        [Fact]
        public void Apply_TokenAuthentication_SetsTokenCredentials()
        {
            this.mockProvider
                .Setup(p => p.AddUrlSource(Uri, null, null, "token"))
                .Verifiable();

            this.options.TokenAuthentication("token");
            this.options.Apply();

            this.mockProvider.Verify();
        }
    }
}
