﻿using System;
using Microsoft.UI.Dispatching;

namespace Microsoft.Maui.Dispatching
{
	public partial class Dispatcher : IDispatcher
	{
		static IDispatcher? GetForCurrentThreadImplementation()
		{
			var q = DispatcherQueue.GetForCurrentThread();
			if (q == null)
				return null;

			return new Dispatcher(q);
		}

		readonly DispatcherQueue _dispatcherQueue;

		Dispatcher(DispatcherQueue dispatcherQueue)
		{
			_dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));
		}

		bool IsInvokeRequiredImplementation() =>
			!_dispatcherQueue.HasThreadAccess;

		void BeginInvokeOnMainThreadImplementation(Action action) =>
			_dispatcherQueue.TryEnqueue(() => action());
	}
}