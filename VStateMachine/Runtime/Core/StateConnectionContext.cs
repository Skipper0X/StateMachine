using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VStateMachine.Runtime.Core.Framework;

namespace VStateMachine.Runtime.Core
{
	/// <inheritdoc cref="IStateConnectionContext"/>
	public sealed class StateConnectionContext : IStateConnectionContext
	{
		private readonly Dictionary<Type, IStateConnectionSet> _connectionsRegistry =
			new Dictionary<Type, IStateConnectionSet>();

		/// <summary>
		/// Create An <see cref="IStateConnection"/> Of Given <see cref="IState"/>'s Type...
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		/// <returns><see cref="IStateConnectionBinding"/> Which Gives A Connection Builder..</returns>
		public IStateConnectionTo CreateOf<TState>() where TState : IState
		{
			var stateConnectionBinding = new StateConnectionBinding();
			stateConnectionBinding.WhenCreate(OnStateConnectionCreate);

			return stateConnectionBinding;

			///////////////////////////////////////////////////////////////////////
			void OnStateConnectionCreate(IStateConnection stateConnection)
			{
				var connections = GetOrCreateSetOf(typeof(TState));
				if (connections.Contains(stateConnection))
				{
					Debug.LogError($"StateConnectionContext: Trying To Create ConnectionOf " +
					               $"{typeof(TState).Name} With {stateConnection.ToStateType.Name} More Then One Time.");
				}

				connections.SetConnection(stateConnection);
			}
		}

		/// <summary>
		/// Remove An <see cref="IStateConnection"/> Of Given <see cref="IState"/>'s Type & Returns The Status Of Remove Op.
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <returns></returns>
		public bool RemoveOf<TState>() where TState : IState
			=> _connectionsRegistry.Remove(typeof(TState));

		/// <summary>
		/// <see cref="IStateConnectionSet"/> Of Given <see cref="IState"/>'s Type....
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <returns></returns>
		public IStateConnectionSet GetConnectionSetOf<TState>() where TState : IState
			=> GetConnectionSetOf(typeof(TState));

		/// <summary>
		/// <see cref="IStateConnectionSet"/> Of Given <see cref="IState"/>'s Type....
		/// </summary>
		/// <returns></returns>
		public IStateConnectionSet GetConnectionSetOf(Type stateType)
			=> HasConnectionSetInRegistry(stateType) ? GetOrCreateSetOf(stateType) : null;

		/// <summary>
		/// <see cref="IStateConnectionContext.OnReset"/> Is Going To Reset <see cref="IStateConnectionContext"/>
		/// </summary>
		public void OnReset() => _connectionsRegistry.Clear();

		//////////////////////////////////////////////////////////////////////////////////////////////
		private IStateConnectionSet GetOrCreateSetOf(Type stateType)
		{
			if (HasConnectionSetInRegistry(stateType)) return _connectionsRegistry[stateType];

			var stateConnectionSet = new StateConnectionSet();
			_connectionsRegistry.Add(stateType, stateConnectionSet);
			return stateConnectionSet;
		}

		private bool HasConnectionSetInRegistry(Type stateType)
			=> _connectionsRegistry.ContainsKey(stateType);
	}
}