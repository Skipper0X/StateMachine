using System;

namespace VStateMachine.Runtime.Core
{
	public class StateMachineException : Exception
	{
		public StateMachineException(string msg) : base(msg)
		{
			
		}
	}
}