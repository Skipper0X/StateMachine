namespace VStateMachine.Runtime.Core.Framework
{
	public interface IStateConnectionSet
	{
		/// <summary>
		/// Check If Any Connection Of The Given <see cref="IState"/> Is Ready
		/// </summary>
		/// <param name="stateConnection"><see cref="IStateConnection"/> Which Is Ready To Switch..</param>
		/// <returns>Returns True If Connection Is Ready & State Can Be Switch..</returns>
		bool CheckIfAnyConnectionIsReady(out IStateConnection stateConnection);

		/// <summary>
		/// Set New <see cref="IStateConnection"/> In This <see cref="IStateConnectionSet"/>
		/// </summary>
		/// <param name="stateConnection"></param>
		void SetConnection(IStateConnection stateConnection);

		/// <summary>
		/// Check If This Set Already Has Given <see cref="IStateConnection"/>...
		/// </summary>
		/// <param name="stateConnection"></param>
		/// <returns></returns>
		bool Contains(IStateConnection stateConnection);
	}
}