using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class NetClient{
		
		private int _port = 23333;
		
		public void ConnectToServer(string ip)
		{
			Network.Connect(ip,_port);
		}
		
	}
}
