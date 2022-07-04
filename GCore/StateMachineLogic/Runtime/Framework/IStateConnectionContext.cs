using System;

namespace GCore.StateMachineLogic.Runtime.Framework
{
	/// <summary>
	/// <see cref="IStateConnectionContext"/> Contains Operations Of State Connections Against A Given <see cref="IState"/>'s Type
	/// </summary>
	public interface IStateConnectionContext
	{
		/// <summary>
		/// Create An <see cref="IStateConnection"/> Of Given <see cref="IState"/>'s Type...
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		/// <returns><see cref="IStateConnectionBinding"/> Which Gives A Connection Builder..</returns>
		IStateConnectionTo CreateOf<TState>() where TState : IState;

		/// <summary>
		/// Remove An <see cref="IStateConnection"/> Of Given <see cref="IState"/>'s Type & Returns The Status Of Remove Op.
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <returns></returns>
		bool RemoveOf<TState>() where TState : IState;

		/// <summary>
		/// <see cref="IStateConnectionSet"/> Of Given <see cref="IState"/>'s Type....
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <returns></returns>
		IStateConnectionSet GetConnectionSetOf<TState>() where TState : IState;

		/// <summary>
		/// <see cref="IStateConnectionSet"/> Of Given <see cref="IState"/>'s Type....
		/// </summary>
		/// <returns></returns>
		IStateConnectionSet GetConnectionSetOf(Type stateType);

		/// <summary>
		/// <see cref="OnReset"/> Is Going To Reset <see cref="IStateConnectionContext"/>
		/// </summary>
		void OnReset();
	}
}