using UnityEngine;
using VStateMachine.Runtime.Core.Framework;

namespace VStateMachine.Demo
{
	public class WalkingState : IState
	{
		public void OnEnter()
		{
		}

		public void OnUpdate()
		{
			Debug.Log($"{this.GetType().Name} :: OnUpdate();");
		}

		public void OnExit()
		{
		}
	}
}