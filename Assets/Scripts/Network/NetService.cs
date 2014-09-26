using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class NetService {
		
		private int _maxplayer = 2;
		private int _port = 23333;
		
		public NetworkConnectionError InitService()
		{
			if(Network.peerType != NetworkPeerType.Disconnected) {return NetworkConnectionError.AlreadyConnectedToServer;}
			
			//var useNat = !Network.HavePublicAddress();
			NetworkConnectionError result = Network.InitializeServer(_maxplayer,_port, false);
			return result;
		}
		public NetworkConnectionError InitService(string password)
		{
			if(Network.peerType != NetworkPeerType.Disconnected) {return NetworkConnectionError.AlreadyConnectedToServer;}
			
			Network.incomingPassword = password;
			//bool useNat = !Network.HavePublicAddress();
			NetworkConnectionError result = Network.InitializeServer(_maxplayer,_port, false);
			return result;
		}
		
		public void ShowAllPlayer()
		{
			foreach(NetworkPlayer a in Network.connections)
			{
				Debug.Log("IP:" + a.ipAddress +":"+a.port);
			}
		}
	}
}