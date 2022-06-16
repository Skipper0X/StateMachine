using System;
using UnityEngine;
using VStateMachine.Runtime.Core;
using VStateMachine.Runtime.Core.Framework;

namespace VStateMachine.Demo
{
	public class StateMachineDemo : MonoBehaviour
	{
		[SerializeField] private bool _canRun;
		private readonly IStateMachine _stateMachine = new StateMachine();


		private void Start()
		{
			_stateMachine.SetLogging(true);

			_stateMachine.RegisterState<WalkingState>(new WalkingState());
			_stateMachine.RegisterState<RunningState>(new RunningState());


			_stateMachine.StateConnectionContext.CreateOf<WalkingState>().To<RunningState>().If(IsRunning);
			_stateMachine.StateConnectionContext.CreateOf<RunningState>().To<WalkingState>().If(IsWalking);

			_stateMachine.RunWith<WalkingState>();
		}

		private void Update()
		{
			_stateMachine.OnUpdate();
		}
		
		private bool IsRunning() => _canRun;

		private bool IsWalking() => _canRun == false;

		private void OnDestroy()
		{
			_stateMachine.Shutdown();
		}
	}
}