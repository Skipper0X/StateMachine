using GCore.StateMachineLogic.Runtime;
using GCore.StateMachineLogic.Runtime.Framework;
using UnityEngine;

namespace GCore.StateMachineLogic.Demo
{
	public class StateMachineDemo : MonoBehaviour, IStateMachineOwner
	{
		[SerializeField] private bool _canRun;

		private IStateMachine _stateMachine;

		private void Start()
		{
			_stateMachine = StateMachine.Create(true, this);

			_stateMachine.RegisterState<WalkingState>(new WalkingState());
			_stateMachine.RegisterState<RunningState>(new RunningState());

			_stateMachine.StateConnectionContext.CreateOf<WalkingState>().To<RunningState>().If(IsRunning);
			_stateMachine.StateConnectionContext.CreateOf<RunningState>().To<WalkingState>().If(IsWalking);

			_stateMachine.RunWith<WalkingState>();
		}

		private void Update() => _stateMachine.OnUpdate();


		private bool IsRunning() => _canRun;

		private bool IsWalking() => _canRun == false;

		private void OnDestroy() => _stateMachine.Shutdown();

		string IStateMachineOwner.GetName() => this.name;
	}
}