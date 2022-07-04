using System;

namespace GCore.StateMachineLogic.Runtime.Framework
{
	/// <summary>
	/// <see cref="IStateConnectionTo"/> Is Going To Set The Connection To The Given <see cref="IState"/>'s Type...
	/// </summary>
	public interface IStateConnectionTo
	{
		/// <summary>
		/// <see cref="To{TState}"/> Is Going To Set The Connection To The Given <see cref="IState"/>'s Type...
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		/// <returns>Returns The <see cref="IStateConnectionCondition"/> To Continue Binding...</returns>
		IStateConnectionCondition To<TState>() where TState : IState;
	}

	/// <summary>
	/// <see cref="IStateConnectionCondition"/> Is Going To Set The Condition Of <see cref="IStateConnectionBinding"/>
	/// With The Given <see cref="IState"/>'s Type...
	/// </summary>
	public interface IStateConnectionCondition
	{
		/// <summary>
		/// <see cref="If"/> Is Going To Contain The Condition Which Determines Whether To Switch Or Not....
		/// </summary>
		/// <param name="condition"></param>
		/// <returns></returns>
		void If(Func<bool> condition);
	}

	/// <summary>
	/// <see cref="IStateConnectionBinding"/> Is A Policy Design Of <see cref="IStateConnectionTo"/>,
	/// <see cref="IStateConnectionCondition"/> Which Is Going To Create Connections For <see cref="IState"/>
	/// </summary>
	public interface IStateConnectionBinding : IStateConnectionTo, IStateConnectionCondition
	{
		// Just Policy............
	}
}