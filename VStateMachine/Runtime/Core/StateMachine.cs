using System;
using System.Collections.Generic;
using UnityEngine;
using VStateMachine.Runtime.Core.Framework;

namespace VStateMachine.Runtime.Core
{
	/// <inheritdoc cref="IStateMachine"/>
	public sealed class StateMachine : IStateMachine
	{
		private bool _logging = false;
		private IStateConnectionSet _currentStateConnectionSet = null;
		private readonly Dictionary<Type, IState> _statesRegistry = new Dictionary<Type, IState>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// <see cref="IStateMachine.IsRunning"/> Returns This <see cref="IStateMachine"/> Is Running Or In Stop Now....
		/// </summary>
		public bool IsRunning { get; private set; } = false;

		/// <summary>
		/// Returns Current Active <see cref="IState"/> Of This <see cref="IStateMachine"/>
		/// </summary>
		public IState CurrentState { get; private set; } = null;

		/// <summary>
		///  <see cref="IStateConnectionContext"/> Contains Operations Of State Connections Against A Given <see cref="IState"/>'s Type
		/// </summary>
		public IStateConnectionContext StateConnectionContext { get; } = new StateConnectionContext();

		/// <summary>
		/// <see cref="IStateMachineOwner"/>'s Owner's Ref..........
		/// </summary>
		public IStateMachineOwner StateMachineOwner { get; private set; } = null;

		/// <summary>
		/// <see cref="IStateMachine.RunWith{TState}"/> Will Set Active <see cref="IState"/>
		/// & KickOf This <see cref="IStateMachine"/> & Set <see cref="IStateMachine.IsRunning"/> To True..
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		public void RunWith<TState>() where TState : IState
		{
			if (IsRunning) return;
			IsRunning = true;

			SwitchState<TState>();
		}

		/// <summary>
		/// Register An <see cref="IState"/> Of Given Type In <see cref="IStateMachine"/>
		/// </summary>
		/// <param name="state"><see cref="IState"/>'s Object</param>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		public void RegisterState<TState>(TState state) where TState : IState
		{
			var stateType = typeof(TState);
			if (HasStateInRegistry(stateType)) return;
			_statesRegistry.Add(stateType, state);
		}

		/// <summary>
		/// UnRegister An <see cref="IState"/> Of Given Type From <see cref="IStateMachine"/>
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		public void UnRegisterState<TState>() where TState : IState
		{
			var stateType = typeof(TState);
			if (HasStateInRegistry(stateType) == false) return;
			_statesRegistry.Remove(stateType);
		}

		/// <summary>
		/// Switch <see cref="IStateMachine"/>'s Active <see cref="IState"/> To Given <see cref="IState"/>'s Type...
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		public void SwitchState<TState>() where TState : IState => SwitchStateLogic(typeof(TState));

		/// <summary>
		/// <see cref="IStateMachine.OnUpdate"/> This <see cref="IStateMachine"/> At Every GameTick...
		/// </summary>
		public void OnUpdate()
		{
			if (IsRunning == false) return;
			CurrentState.OnUpdate();
			CheckIfAnyStateConnectionIsReady();
		}

		/// <summary>
		/// <see cref="IStateMachine.Stop"/> This <see cref="IStateMachine"/> & Freeze All Operation & <see cref="IStateMachine.IsRunning"/> to: false
		/// </summary>
		public void Stop() => IsRunning = false;

		/// <summary>
		/// <see cref="IStateMachine.Shutdown"/> & Reset Private States Of <see cref="IStateMachine"/>
		/// </summary>
		public void Shutdown()
		{
			_statesRegistry.Clear();
			_currentStateConnectionSet = null;

			IsRunning = false;
			CurrentState = null;
			StateConnectionContext.OnReset();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		private void CheckIfAnyStateConnectionIsReady()
		{
			if (_currentStateConnectionSet == null) return;
			if (_currentStateConnectionSet.CheckIfAnyConnectionIsReady(out var stateConnection))
			{
				SwitchStateLogic(stateConnection.ToStateType);
			}
		}

		private void SwitchStateLogic(Type stateType)
		{
			if (IsRunning == false) return;

			if (CurrentState != null)
			{
				CurrentState.OnExit();
				LogInfo($"{CurrentState.GetType().Name}.OnExit();");
			}

			_currentStateConnectionSet = StateConnectionContext.GetConnectionSetOf(stateType);
			CurrentState = GetStateOf(stateType);
			CurrentState.OnEnter();

			LogInfo($"{CurrentState.GetType().Name}.OnEnter();");
		}

		private IState GetStateOf(Type stateType)
		{
			if (HasStateInRegistry(stateType)) return _statesRegistry[stateType];
			throw new StateMachineException(
				$"StateMachineException: {stateType.Name} Is Not Found. Check StateRegistry Of StateMachine!");
		}

		private void SetLogging(bool canLog) => _logging = canLog;
		private void SetOwner(IStateMachineOwner owner) => StateMachineOwner = owner;
		private bool HasStateInRegistry(Type stateType) => _statesRegistry.ContainsKey(stateType);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		private void LogInfo(string info)
		{
			if (_logging == false) return;
			var ownerName = StateMachineOwner != null ? StateMachineOwner.GetName() : "UnKnown";
			Debug.Log($"{ownerName} -> StateMachine :: {info}");
		}

		/// <summary>
		/// Create A new <see cref="StateMachine"/>'s Object & Returns It's Contract <see cref="IStateMachine"/>
		/// </summary>
		/// <param name="logging">Set If <see cref="IStateMachine"/> Can Log...</param>
		/// <param name="stateMachineOwner"></param>
		/// <returns></returns>
		public static IStateMachine Create(bool logging = false, IStateMachineOwner stateMachineOwner = null)
		{
			var stateMachine = new StateMachine();
			stateMachine.SetOwner(stateMachineOwner);
			stateMachine.SetLogging(logging);
			return stateMachine;
		}
	}
}