using Shouldly;

namespace WorldDomination.SimpleAzure.Storage.HybridQueues.Tests.IntegrationTests.AddMessagesAsyncTests;

public class AddMessagesAsyncTests : CustomAzuriteTestContainer
{
    [Fact]
    public async Task AddMessagesAsync_GivenSomeSimpleStrings_ShouldAddThemAllToTheQueue()
    {
        // Arrange.
        var messages = new[] { "aaa", "bbb", "ccc", "ddd" };

        // Act.
        await HybridQueue.AddMessagesAsync(messages, default);

        // Assert.
        var retrievedMessage = await HybridQueue.GetMessagesAsync<string>();
        retrievedMessage.ShouldNotBeNull();

        retrievedMessage
            .Select(hm => hm.Content)
            .ToArray()
            .OrderBy(s => s)
            .ShouldBeEquivalentTo(messages.OrderBy(s => s));
    }

    [Fact]
    public async Task AddMessagesAsync_GivenSomeComplexInstances_ShouldAddThemAllToTheQueue()
    {
        // Arrange.
        var messages = new[]
        {
            new FakeMessage(20),
            new FakeMessage(20),
            new FakeMessage(20),
            new FakeMessage(20),
            new FakeMessage(20)
        };

        // Act.
        await HybridQueue.AddMessagesAsync(messages, default);

        // Assert.
        var retrievedMessage = await HybridQueue.GetMessagesAsync<FakeMessage>();
        retrievedMessage.ShouldNotBeNull();

        retrievedMessage
            .Select(hm => hm.Content!)
            .ToArray()
            .OrderBy(fm => fm.Content)
            .ShouldBeEquivalentTo(messages.OrderBy(fm => fm.Content));
    }

    [Fact]
    public async Task AddMessagesAsync_GivenSomeLargeComplexInstances_ShouldAddThemAllToTheblobAndQueue()
    {
        // Arrange.
        var messages = new[]
        {
            new FakeMessage(QueueClient.MessageMaxBytes + 1),
            new FakeMessage(QueueClient.MessageMaxBytes + 1),
            new FakeMessage(QueueClient.MessageMaxBytes + 1),
            new FakeMessage(QueueClient.MessageMaxBytes + 1),
            new FakeMessage(QueueClient.MessageMaxBytes + 1)
        };

        // Act.
        await HybridQueue.AddMessagesAsync(messages, default);

        // Assert.
        var retrievedMessage = await HybridQueue.GetMessagesAsync<FakeMessage>();
        retrievedMessage.ShouldNotBeNull();

        retrievedMessage
            .Select(hm => hm.Content!)
            .ToArray()
            .OrderBy(fm => fm.Content)
            .ShouldBeEquivalentTo(messages.OrderBy(fm => fm.Content));
    }
}
