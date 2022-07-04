using System;

namespace GCore.StateMachineLogic.Runtime
{
	public class StateMachineException : Exception
	{
		public StateMachineException(string msg) : base(msg)
		{
			
		}
	}
}