namespace GCore.StateMachineLogic.Runtime.Framework
{
	/// <summary>
	/// <see cref="IState"/> Is A Contract Of Logical State Which Is Compute By <see cref="IStateMachine"/>
	/// </summary>
	public interface IState
	{
		/// <summary>
		/// <see cref="OnEnter"/> Is Going To Dispatch Once This <see cref="IState"/> Get Active In <see cref="IStateMachine"/>
		/// </summary>
		void OnEnter();

		/// <summary>
		/// <see cref="OnUpdate"/> Is Going To Dispatch Once Every Frame By The <see cref="IStateMachine"/>
		/// </summary>
		void OnUpdate();

		/// <summary>
		/// <see cref="OnExit"/> Is Going To Dispatch When State Is Switched In <see cref="IStateMachine"/>...
		/// </summary>
		void OnExit();
	}
}