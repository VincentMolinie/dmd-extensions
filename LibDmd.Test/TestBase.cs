﻿using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LibDmd.Frame;
using LibDmd.Test.Stubs;
using NUnit.Framework;

namespace LibDmd.Test
{
	public class TestBase
	{
		private TestContext testContextInstance;

		/// <summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		/// </summary>
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		protected static void Print(object obj)
		{
			TestContext.WriteLine(obj);
		}

		protected static async Task AssertFrame(ITestSource source, ITestDestination<DmdFrame> dest, DmdFrame srcFrame, DmdFrame expectedFrame)
		{
			Print(srcFrame);
			
			dest.Reset();
			source.AddFrame(srcFrame);
			var receivedFrame = await dest.Frame;
			
			Print(receivedFrame);
			
			receivedFrame.Data.Should().BeEquivalentTo(expectedFrame.Data);
			receivedFrame.BitLength.Should().Be(expectedFrame.BitLength);
			receivedFrame.Dimensions.Should().Be(expectedFrame.Dimensions);
		}
	}
}