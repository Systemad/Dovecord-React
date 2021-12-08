using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using WebUI.Dtos.Channel;

namespace FunctionalTests.FunctionalTests.Channel;

public class CreateChannelTest : TestBase
{
    [Test]
    public async Task create_channel_return_created()
    {
        var fakeChannel = new ChannelDto
        {
            Id = Guid.NewGuid(),
            Name = "testchannel"
        };

        var result = await _client.PostAsJsonAsync(ApiRoutes.Channels.Create, fakeChannel);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}