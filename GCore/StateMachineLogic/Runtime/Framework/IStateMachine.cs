namespace GCore.StateMachineLogic.Runtime.Framework
{
	/// <summary>
	/// <see cref="IStateMachine"/> Provides  A Mathematical Model of Computation(Finite Automata) For Finite Number Of <see cref="IState"/>'s
	/// </summary>
	public interface IStateMachine
	{
		/// <summary>
		/// <see cref="IsRunning"/> Returns This <see cref="IStateMachine"/> Is Running Or In Stop Now....
		/// </summary>
		bool IsRunning { get; }

		/// <summary>
		/// Returns Current Active <see cref="IState"/> Of This <see cref="IStateMachine"/>
		/// </summary>
		IState CurrentState { get; }

		/// <summary>
		///  <see cref="IStateConnectionContext"/> Contains Operations Of State Connections Against A Given <see cref="IState"/>'s Type
		/// </summary>
		IStateConnectionContext StateConnectionContext { get; }

		/// <summary>
		/// <see cref="IStateMachineOwner"/>'s Owner's Ref..........
		/// </summary>
		IStateMachineOwner StateMachineOwner { get; }

		/// <summary>
		/// <see cref="RunWith{TState}"/> Will Set Active <see cref="IState"/>
		/// & KickOf This <see cref="IStateMachine"/> & Set <see cref="IsRunning"/> To True..
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		void RunWith<TState>() where TState : IState;

		/// <summary>
		/// Register An <see cref="IState"/> Of Given Type In <see cref="IStateMachine"/>
		/// </summary>
		/// <param name="state"><see cref="IState"/>'s Object</param>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		void RegisterState<TState>(TState state) where TState : IState;

		/// <summary>
		/// UnRegister An <see cref="IState"/> Of Given Type From <see cref="IStateMachine"/>
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		void UnRegisterState<TState>() where TState : IState;

		/// <summary>
		/// Switch <see cref="IStateMachine"/>'s Active <see cref="IState"/> To Given <see cref="IState"/>'s Type...
		/// </summary>
		/// <typeparam name="TState">typeOf: <see cref="IState"/></typeparam>
		void SwitchState<TState>() where TState : IState;

		/// <summary>
		/// <see cref="OnUpdate"/> This <see cref="IStateMachine"/> At Every GameTick...
		/// </summary>
		void OnUpdate();

		/// <summary>
		/// <see cref="Stop"/> This <see cref="IStateMachine"/> & Freeze All Operation & <see cref="IsRunning"/> to: false
		/// </summary>
		void Stop();

		/// <summary>
		/// <see cref="Shutdown"/> & Reset Private States Of <see cref="IStateMachine"/>
		/// </summary>
		void Shutdown();
	}
}