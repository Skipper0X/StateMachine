using System;

namespace GCore.StateMachineLogic.Runtime.Framework
{
	/// <summary>
	/// <see cref="IStateConnection"/> Serves As A Link To Switch To Set <see cref="IState"/> As Per Condition...
	/// </summary>
	public interface IStateConnection
	{
		/// <summary>
		/// TypeOf <see cref="IState"/> SwitchTo...
		/// </summary>
		Type ToStateType { get; }

		/// <summary>
		/// <see cref="CanSwitch"/> Returns True If <see cref="IState"/>'s Switch Is Ready...
		/// </summary>
		bool CanSwitch();
	}
}