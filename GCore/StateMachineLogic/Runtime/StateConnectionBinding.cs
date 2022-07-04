using System;
using GCore.StateMachineLogic.Runtime.Framework;

namespace GCore.StateMachineLogic.Runtime
{
	/// <inheritdoc cref="IStateConnectionBinding"/>
	public sealed class StateConnectionBinding : IStateConnectionBinding
	{
		private Type _toStateType;
		private Func<bool> _condition;
		private event Action<IStateConnection> OnCreate;

		/// <inheritdoc cref="IStateConnectionBinding.To{TState}"/>
		public IStateConnectionCondition To<TState>() where TState : IState
		{
			_toStateType = typeof(TState);
			return this;
		}

		/// <inheritdoc cref="IStateConnectionBinding.If"/>
		public void If(Func<bool> condition)
		{
			_condition = condition;
			OnCreate?.Invoke(new StateConnection(_toStateType, _condition));
		}

		/// <summary>
		/// <see cref="WhenCreate"/> Is Concrete State To Register For <see cref="IStateConnection"/>'s Creation....
		/// </summary>
		/// <param name="onCreate"></param>
		public void WhenCreate(Action<IStateConnection> onCreate)
			=> this.OnCreate = onCreate;
	}
}