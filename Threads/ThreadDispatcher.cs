﻿using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace TopDownMedieval.Plugins.Commons.Utils
{
	public class ThreadDispatcher : MonoBehaviour
	{
		public static void RunAsync(Action action) {
			ThreadPool.QueueUserWorkItem(o => action());
		}
 
		public static void RunAsync(Action<object> action, object state) {
			ThreadPool.QueueUserWorkItem(o => action(o), state);
		}
 
		public static void RunOnMainThread(Action action)
		{
			lock(_backlog) {
				_backlog.Add(action);
				_queued = true;
			}
		}
 
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Initialize()
		{
			if(_instance == null) {
				_instance = new GameObject("ThreadDispatcher").AddComponent<ThreadDispatcher>();
				DontDestroyOnLoad(_instance.gameObject);
			}
		}

		void Update()
		{
			if(_queued)
			{
				lock(_backlog) {
					List<Action> tmp = _actions;
					_actions = _backlog;
					_backlog = tmp;
					_queued = false;
				}
 
				foreach(var action in _actions)
					action();
 
				_actions.Clear();
			}
		}
 
		static ThreadDispatcher _instance;
		static volatile bool _queued = false;
		static List<Action> _backlog = new List<Action>(8);
		static List<Action> _actions = new List<Action>(8);
	}

}