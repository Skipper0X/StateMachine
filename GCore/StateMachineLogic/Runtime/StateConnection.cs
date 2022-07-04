using System;
using GCore.StateMachineLogic.Runtime.Framework;

namespace GCore.StateMachineLogic.Runtime
{
	/// <inheritdoc cref="IStateConnection"/>
	public readonly struct StateConnection : IStateConnection
	{
		/// <summary>
		/// TypeOf <see cref="IState"/> SwitchTo...
		/// </summary>
		public readonly Type ToStateType { get; }

		/// <summary>
		/// <see cref="IStateConnection.CanSwitch"/> Returns True If <see cref="IState"/>'s Switch Is Ready...
		/// </summary>
		public bool CanSwitch() => _condition();

		private readonly Func<bool> _condition;

		public StateConnection(Type toStateType, Func<bool> condition)
		{
			ToStateType = toStateType;
			_condition = condition;
		}
	}
}