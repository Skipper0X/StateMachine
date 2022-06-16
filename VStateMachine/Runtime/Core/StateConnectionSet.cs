using System.Collections.Generic;
using System.Linq;
using VStateMachine.Runtime.Core.Framework;

namespace VStateMachine.Runtime.Core
{
	public sealed class StateConnectionSet : IStateConnectionSet
	{
		private readonly HashSet<IStateConnection> _stateConnections = new HashSet<IStateConnection>();

		/// <summary>
		/// Check If Any Connection Of The Given <see cref="IState"/> Is Ready
		/// </summary>
		/// <param name="stateConnection"><see cref="IStateConnection"/> Which Is Ready To Switch..</param>
		/// <returns>Returns True If Connection Is Ready & State Can Be Switch..</returns>
		public bool CheckIfAnyConnectionIsReady(out IStateConnection stateConnection)
		{
			stateConnection = default;
			if (_stateConnections.Count == 0) return false;

			stateConnection = _stateConnections.FirstOrDefault(connection => connection.CanSwitch());
			return stateConnection != null;
		}

		/// <summary>
		/// Set New <see cref="IStateConnection"/> In This <see cref="IStateConnectionSet"/>
		/// </summary>
		/// <param name="stateConnection"></param>
		public void SetConnection(IStateConnection stateConnection)
		{
			if (Contains(stateConnection)) return;
			_stateConnections.Add(stateConnection);
		}

		/// <summary>
		/// Check If This Set Already Has Given <see cref="IStateConnection"/>...
		/// </summary>
		/// <param name="stateConnection"></param>
		/// <returns></returns>
		public bool Contains(IStateConnection stateConnection)
			=> _stateConnections.Contains(stateConnection);
	}
}