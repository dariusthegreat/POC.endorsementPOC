using System.Text;
using TechQ.Core.Extensions;
// ReSharper disable EventNeverSubscribedTo.Global

// ReSharper disable UnusedMember.Local
// ReSharper disable EventNeverSubscribedTo.Local

#pragma warning disable CS0067 // Event is never used


namespace TechQ.DocumentManagement.Tests;

//[TestClass]
public class EventHandlingTests
{
	public EventHandlingTests()
	{
		EventHandlersChanged += OnEventHandlersModified;
	}

	private readonly List<string> _eventHandlingResults = new();

	private int RegisteredHandlersCount => _somethingElseHappenedDelegate?.GetInvocationList().Length ?? 0;

	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public TestContext TestContext { get; set; }

	private void WriteLine(string message) => TestContext.WriteLine(message);

	private void Reset()
	{
		RemoveAllRegisteredEventHandlersFromSomethingElseHappened();
		_eventHandlingResults.Clear();
	}

	private event EventHandler<MessageEventArgs> SomethingElseHappenedHandlersModified;
	private event EventHandler<MessageEventArgs> SomethingElseHappenedHandlerAdded;
	private event EventHandler<MessageEventArgs> SomethingElseHappenedHandlerRemoved;

	[TestMethod]
	public void RegisteredHandlersShouldHandleEvents()
	{
		SomethingElseHappened += (_, args) =>
		{
			_eventHandlingResults.Add($"single registered event handler - {args}");
		};

		_eventHandlingResults.Clear();
		FireSomethingElseHappened("event #1");

		Assert.AreEqual(1, _eventHandlingResults.Count);
		Assert.AreEqual("single registered event handler - Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		
		SomethingElseHappened += (_, args) => _eventHandlingResults.Add($"second handler - {args}");
		_eventHandlingResults.Clear();
		FireSomethingElseHappened("event #2");
		Assert.AreEqual(2, _eventHandlingResults.Count);
		Assert.AreEqual("second handler - Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
	}

	[TestMethod]
	public void RegisteredEventHandlerMethodsShouldHandleEvents()
	{
		SomethingElseHappened += Handler1;
		
		_eventHandlingResults.Clear();

		FireSomethingElseHappened("event #1");
		Assert.AreEqual(1, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);

		FireSomethingElseHappened("event #2");
		Assert.AreEqual(2, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);

		FireSomethingElseHappened("event #3");
		Assert.AreEqual(3, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #3'}", _eventHandlingResults[2]);

		FireSomethingElseHappened("event #4");
		Assert.AreEqual(4, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #3'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #4'}", _eventHandlingResults[3]);

		SomethingElseHappened += Handler2;

		FireSomethingElseHappened("event #5");
		Assert.AreEqual(6, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #3'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #4'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[5]);

		FireSomethingElseHappened("event #6");
		Assert.AreEqual(8, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #3'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #4'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #6'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #6'}", _eventHandlingResults[7]);

		FireSomethingElseHappened("event #7");
		Assert.AreEqual(10, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #3'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #4'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #5'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #6'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #6'}", _eventHandlingResults[7]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'event #7'}", _eventHandlingResults[8]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'event #7'}", _eventHandlingResults[9]);
	}

	[TestMethod]
	public void RegisteringAnEventHandlerMoreThanOnceShouldHaveNoEffect()
	{
		SomethingElseHappened += Handler1;

		_eventHandlingResults.Clear();
		FireSomethingElseHappened("#1");
		Assert.AreEqual(1, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);

		SomethingElseHappened += Handler1;
		FireSomethingElseHappened("#2");
		Assert.AreEqual(3, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);

		SomethingElseHappened += Handler1;
		FireSomethingElseHappened("#3");
		Assert.AreEqual(6, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[5]);

		SomethingElseHappened += Handler2;
		FireSomethingElseHappened("#4");
		Assert.AreEqual(10, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[7]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[8]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#4'}", _eventHandlingResults[9]);

		SomethingElseHappened += Handler2;
		FireSomethingElseHappened("#5");
		Assert.AreEqual(15, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[7]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[8]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#4'}", _eventHandlingResults[9]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[10]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[11]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[12]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[13]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[14]);

		SomethingElseHappened += Handler2;
		FireSomethingElseHappened("#6");
		Assert.AreEqual(21, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[7]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[8]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#4'}", _eventHandlingResults[9]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[10]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[11]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[12]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[13]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[14]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[15]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[16]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[17]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[18]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[19]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[20]);

		FireSomethingElseHappened("#7");
		Assert.AreEqual(27, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#3'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[6]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[7]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#4'}", _eventHandlingResults[8]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#4'}", _eventHandlingResults[9]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[10]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[11]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#5'}", _eventHandlingResults[12]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[13]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#5'}", _eventHandlingResults[14]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[15]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[16]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#6'}", _eventHandlingResults[17]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[18]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[19]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#6'}", _eventHandlingResults[20]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#7'}", _eventHandlingResults[21]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#7'}", _eventHandlingResults[22]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: '#7'}", _eventHandlingResults[23]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#7'}", _eventHandlingResults[24]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#7'}", _eventHandlingResults[25]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: '#7'}", _eventHandlingResults[26]);
	}

	[TestMethod]
	public void RemovingTheSameHandlerFromAnEventMoreThanOnceShouldHaveNoImpact()
	{
		Assert.AreEqual(0, RegisteredHandlersCount);

		SomethingElseHappened += Handler1;
		Assert.AreEqual(1, RegisteredHandlersCount);

		SomethingElseHappened += Handler2;
		Assert.AreEqual(2, RegisteredHandlersCount);

		_eventHandlingResults.Clear();

		FireSomethingElseHappened("test3/event#1");
		Assert.AreEqual(2, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);

		FireSomethingElseHappened("test3/event#2");
		Assert.AreEqual(4, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);

		SomethingElseHappened -= Handler1;
		Assert.AreEqual(1, RegisteredHandlersCount);

		FireSomethingElseHappened("test3/event#3");
		Assert.AreEqual(5, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#3'}", _eventHandlingResults[4]);

		SomethingElseHappened -= Handler1;
		Assert.AreEqual(1, RegisteredHandlersCount);

		FireSomethingElseHappened("test3/event#4");
		Assert.AreEqual(6, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#4'}", _eventHandlingResults[5]);

		SomethingElseHappened -= Handler1;
		Assert.AreEqual(1, RegisteredHandlersCount);

		FireSomethingElseHappened("test3/event#5");
		Assert.AreEqual(7, _eventHandlingResults.Count);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#4'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#5'}", _eventHandlingResults[6]);

		SomethingElseHappened -= Handler2;
		Assert.AreEqual(0, RegisteredHandlersCount);

		FireSomethingElseHappened("test3/event#6");
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#4'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#5'}", _eventHandlingResults[6]);

		SomethingElseHappened -= Handler2;
		Assert.AreEqual(0, RegisteredHandlersCount);

		FireSomethingElseHappened("test3/event#7");
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[0]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#1'}", _eventHandlingResults[1]);
		Assert.AreEqual("handler invoked: Handler1 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[2]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#2'}", _eventHandlingResults[3]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#3'}", _eventHandlingResults[4]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#4'}", _eventHandlingResults[5]);
		Assert.AreEqual("handler invoked: Handler2 for event: Message Event Args {message: 'test3/event#5'}", _eventHandlingResults[6]);
	}

	private void TakeAction(string action)
	{
		var regex = new Regex(@"^((?<add>Register)|(?<remove>Remove))\sHandler\s(?<number>1|2)\s*$");
		var match = regex.Match(action);
		Assert.IsTrue(match.Success);

		var handlerNumber = Convert.ToInt32(match.Groups["number"].Value);
		EventHandler<MessageEventArgs> handler = handlerNumber == 1 ? Handler1 : Handler2;

		if (match.Groups["add"].Success)
		{
			SomethingElseHappened += handler;
			WriteLine($"action: {action}. Added handler #{handlerNumber}");
			return;
		}

		if (match.Groups["remove"].Success)
		{
			SomethingElseHappened -= handler;
			WriteLine($"action: {action}. Removed handler #{handlerNumber}");
			return;
		}

		Assert.Fail($"invalid action: {action}");
	}
	
	private void Handler1(object sender, EventArgs eventArgs) => _eventHandlingResults.Add($"handler invoked: {nameof(Handler1)} for event: {eventArgs}");
	private void Handler2(object sender, EventArgs eventArgs) => _eventHandlingResults.Add($"handler invoked: {nameof(Handler2)} for event: {eventArgs}");

	public class MessageEventArgs : EventArgs
	{
		public string Message { get; init; }

		public override string ToString() => $"Message Event Args {{message: '{Message}'}}";

		public static implicit operator string(MessageEventArgs args) => args.ToString();
	}

	

	public	event EventHandler<MessageEventArgs>	SomethingHappened;
	
	// ReSharper disable once InconsistentNaming
	private event EventHandler<MessageEventArgs>	_somethingElseHappenedDelegate;
	
	public	event EventHandler<MessageEventArgs>	SomethingElseHappened
	{
		add
		{
			WriteLine("adding handler to event 'something else happened'.");
			WriteLine($"handler: {value}");
			_somethingElseHappenedDelegate += value;
			FireEventHandlersModified(_somethingElseHappenedDelegate, value, EventHandlerChanged.EventModificationType.Added);
		}
		remove
		{
			WriteLine("removing handler from event 'something else happened'.");
			WriteLine($"handler: {value}");
			_somethingElseHappenedDelegate -= value;
			FireEventHandlersModified(_somethingElseHappenedDelegate, value, EventHandlerChanged.EventModificationType.Removed);
		}
	}


	/// <summary>
	/// Fires an event (in case there are any handlers currently subscribed to the event) informing subscribers that an event has gained or lost a handler.
	/// </summary>
	/// <param name="modifiedEvent">event which got modified</param>
	/// <param name="handler">event handler that was added or removed</param>
	/// <param name="modificationType">"added" or "removed"</param>
	/// <exception cref="NotImplementedException"></exception>
	private void FireEventHandlersModified<T>(EventHandler<T> modifiedEvent, EventHandler<T> handler, EventHandlerChanged.EventModificationType modificationType) where T : EventArgs
	{
		ArgumentNullException.ThrowIfNull(handler);

		var modifiedMessageEvent = CastEvent(modifiedEvent);
		if( handler is not EventHandler<MessageEventArgs> messageEventHandler) throw new ArgumentException($"invalid delegate type: {handler.GetType()}", nameof(handler));

		EventHandlersChanged?.Invoke(this, new(modifiedMessageEvent, messageEventHandler, modificationType));

		static EventHandler<MessageEventArgs> CastEvent(EventHandler<T> modifiedEvent)
		{
			if (modifiedEvent == null)
				return null;

			if(modifiedEvent is not EventHandler<MessageEventArgs> modifiedMessageEvent) 
				throw new ArgumentException($"invalid delegate type: {modifiedEvent.GetType()}", nameof(modifiedEvent));

			return modifiedMessageEvent;
		}
	}
	

	private event EventHandler<EventHandlerChanged<MessageEventArgs>> EventHandlersChanged;


	[TestMethod]
	public void RegisteredEventHandlersShouldGetInvokededExactlyOnceRegardlessOfHowManyTimesTheyAreRegisteredToHandleTheEvent()
	{
		RemoveAllRegisteredEventHandlersFromSomethingElseHappened();
		Assert.AreEqual(0, RegisteredHandlersCount);

		FireSomethingElseHappened("No handlers registered");
		Assert.AreEqual(0, _eventHandlingResults.Count);

		const int cycleCount = 4;

		int expectedResultsSize = 0;

		
		for (int i=0;i<cycleCount;i++)
		{
			Assert.AreEqual(i, RegisteredHandlersCount);
			SomethingElseHappened += Handler1;
			Assert.AreEqual(i+1, RegisteredHandlersCount);
			FireSomethingElseHappened($"registered handler #1 for the {(i+1).ToOrdinal()} time");
			Assert.AreEqual(expectedResultsSize += RegisteredHandlersCount, _eventHandlingResults.Count);
		}

		for (int i = 0; i < cycleCount; i++)
		{
			Assert.AreEqual(cycleCount + i, RegisteredHandlersCount);
			SomethingElseHappened += Handler2;
			SomethingElseHappened += DocumentGeneratorFactoryTests_SomethingElseHappened;
			Assert.AreEqual(cycleCount + i + 1, RegisteredHandlersCount);
			FireSomethingElseHappened($"registered handler #2 for the {(i + 1).ToOrdinal()} time");
			expectedResultsSize += RegisteredHandlersCount;
			Assert.AreEqual(expectedResultsSize, _eventHandlingResults.Count);
		}

		for (int i = 0; i < cycleCount; i++)
		{
			Assert.AreEqual(2 * cycleCount - i , RegisteredHandlersCount);
			SomethingElseHappened -= Handler2;
			Assert.AreEqual(2 * cycleCount - i - 1, RegisteredHandlersCount);
			FireSomethingElseHappened($"removed handler #2 for the {(i + 1).ToOrdinal()} time");
			Assert.AreEqual((expectedResultsSize += RegisteredHandlersCount), _eventHandlingResults.Count);
		}

		for (int i = 0; i < cycleCount; i++)
		{
			Assert.AreEqual(cycleCount + i, RegisteredHandlersCount);
			SomethingElseHappened += Handler2;
			Assert.AreEqual(cycleCount + i + 1, RegisteredHandlersCount);
			FireSomethingElseHappened($"re-added handler #2 for the {(i + 1).ToOrdinal()} time.");
			Assert.AreEqual((expectedResultsSize += RegisteredHandlersCount), _eventHandlingResults.Count);
		}

		for (int i = 0; i < cycleCount; i++)
		{
			Assert.AreEqual(2 * cycleCount - i, RegisteredHandlersCount);
			SomethingElseHappened -= Handler1;
			Assert.AreEqual(2 * cycleCount - i - 1, RegisteredHandlersCount);
			FireSomethingElseHappened($"removed handler #1 for the {(i + 1).ToOrdinal()} time");
			Assert.AreEqual((expectedResultsSize += RegisteredHandlersCount), _eventHandlingResults.Count);
		}

		for (int i = 0; i < cycleCount; i++)
		{
			Assert.AreEqual(cycleCount - i, RegisteredHandlersCount);
			SomethingElseHappened -= Handler2;
			Assert.AreEqual(cycleCount - i - 1, RegisteredHandlersCount);
			FireSomethingElseHappened($"removed handler #2 for the {(i + 1).ToOrdinal()} time");
			Assert.AreEqual((expectedResultsSize += RegisteredHandlersCount), _eventHandlingResults.Count);
		}
	}
	
	private void RemoveAllRegisteredEventHandlersFromSomethingElseHappened()
	{
		if (_somethingElseHappenedDelegate == null)
		{
			WriteLine($"{nameof(_somethingElseHappenedDelegate)} == null");
			return;
		}

		var invocationList = _somethingElseHappenedDelegate.GetInvocationList()
			.Select(x => (EventHandler<MessageEventArgs>)x)
			.ToArray();

		WriteLine($"registered event handlers count before clearing: {invocationList.Length}");

		foreach (var handler in invocationList)
			SomethingElseHappened -= handler;

		int newCount = _somethingElseHappenedDelegate.GetInvocationList().Length;
		WriteLine($"registered event handlers count after clearing: {newCount}");
		Assert.AreEqual(0, newCount);
	}


	[TestMethod]
	public void TestEventHandling()
	{
		FireSomethingElseHappened("there are no registered handlers at the moment, so no event should be fired at this time.");

		WriteLine("adding event handler to event 'something else happened'.");
		SomethingElseHappened += SomethingHappenedTestHandler;
		FireSomethingElseHappened("there is one handler for the event, so the event should be fired.");

		WriteLine("adding second handler to event 'something else happened'");
		SomethingElseHappened += AnotherHandlerForSomethingHappenedEvent;
		FireSomethingElseHappened("there are two handlers for the event, so the event should be fired.");

		WriteLine("adding first handler to event 'something else happened' again.");
		SomethingElseHappened += AnotherHandlerForSomethingHappenedEvent;
		FireSomethingElseHappened("The first handler was added twice. This redundant addition should have no effect.");

		WriteLine("removing first event handler (SomethingHappenedTestHandler) from event 'something else happened'.");
		SomethingElseHappened -= SomethingHappenedTestHandler;
		FireSomethingElseHappened("there is one handler for the event, so the event should be fired.");

		WriteLine("removing first event handler (SomethingHappenedTestHandler) from event 'something else happened' again. This should have no effect.");
		SomethingElseHappened -= SomethingHappenedTestHandler;
		FireSomethingElseHappened("there is one handler for the event, so the event should be fired.");

		WriteLine("removing second event handler (AnotherHandlerForSomethingHappenedEvent) from event 'something else happened'.");
		SomethingElseHappened -= AnotherHandlerForSomethingHappenedEvent;
		FireSomethingElseHappened("there are no registered handlers at the moment, so no event should be fired at this time.");
	}

	private void FireSomethingElseHappened(string message) => FireSomethingElseHappened(new MessageEventArgs { Message = message });

	private void FireSomethingElseHappened(MessageEventArgs args) => _somethingElseHappenedDelegate?.Invoke(this, args);

	private void SomethingHappenedTestHandler(object sender, MessageEventArgs eventArgs)
	{
		WriteLine($"event handler invoked: {nameof(SomethingHappenedTestHandler)}");
		WriteLine($"event args: {eventArgs}");
	}

	private void AnotherHandlerForSomethingHappenedEvent(object sender, MessageEventArgs eventArgs)
	{
		WriteLine($"event handler invoked: {nameof(AnotherHandlerForSomethingHappenedEvent)}");
		WriteLine($"event args: {eventArgs}");
	}


	private void DocumentGeneratorFactoryTests_SomethingElseHappened(object sender, MessageEventArgs e)
	{
		throw new NotImplementedException();
	}

	private void DocumentGeneratorFactoryTests_SomethingHappened(object sender, MessageEventArgs e)
	{
		throw new NotImplementedException();
	}


	private void OnEventHandlersModified<T>(object sender, EventHandlerChanged<T> e) where T : EventArgs => new StringBuilder()
																											.AppendLine("\n\n")
																											.AppendLine(new string('-', 120))
																											.AppendLine($"event handler {(e.ModificationType == EventHandlerChanged.EventModificationType.Added ? "added to" : "removed from")} event: {e.ModifiedEvent}")
																											.AppendLine($"modified event: {e.ModifiedEvent}")
																											.AppendLine($"event handler: {e.Handler}")
																											.AppendLine($"modification type: {e.ModificationType}")
																											.WriteTo(TestContext);
}


public static class Extensions
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static StringBuilder WriteTo(this StringBuilder stringBuilder, TestContext context)
	{
		context.WriteLine(stringBuilder.ToString());
		return stringBuilder;
	}
}

public abstract class EventHandlerChanged : EventArgs
{
	public enum EventModificationType { Removed = -1, Added = 1 }
}

public class EventHandlerChanged<T>(EventHandler<T>                           modifiedEvent,
                                    EventHandler<T>                           handler,
                                    EventHandlerChanged.EventModificationType modificationType) : EventHandlerChanged where T : EventArgs
{
	public EventHandler<T>       ModifiedEvent    { get; } = modifiedEvent;
	public EventHandler<T>       Handler          { get; } = handler;
	public EventModificationType ModificationType { get; } = modificationType;
}