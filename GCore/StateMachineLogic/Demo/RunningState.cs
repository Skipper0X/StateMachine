using GCore.StateMachineLogic.Runtime.Framework;
using UnityEngine;

namespace GCore.StateMachineLogic.Demo
{
	public class RunningState : IState
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