using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite{
	
 	public class CheckChess : MonoBehaviour {
		public GameObject ChessmanInstance;
		
		AudioSource audio;
		
		public bool NetGame;
		public bool IsWaittingPlayer;
		
		private AI aiscript;
		
		public Controls control;
		
		public ChessmanState MyColor;
		
		bool JumpControlFlag = false;
		
		// Use this for initialization
		void Start () {
			control = new Controls(ChessmanInstance);
			aiscript = (AI)GameObject.Find("Main Camera").GetComponent("AI");
			audio = this.GetComponent<AudioSource>();
			
			InitGame();
			
			if(NetGame)
			{
				if(Menu.IPAddress == "")
				{
					//创建服务端
					Network.InitializeServer(2,23333,false);
					IsWaittingPlayer = true;
					MyColor = ChessmanState.BlackChessman;
					JumpControlFlag = true;
				}
				else 
				{
					//连接服务器
					GameObject.Find("Net").SetActive(false);
					
					Network.Connect(Menu.IPAddress,23333);
					
					MyColor = ChessmanState.WhiteChessman;
				}
			}
		}
		
		void Update(){
			
		}
		
		//游戏初始棋盘配置
		public void InitGame(){
			Controls.WaittingChessmanState = ChessmanState.BlackChessman;
			
			Controls.Chessbroad = new Chessman[8,8];//clear the record
			
			control.InstantiateChessman(3,4,ChessmanState.WhiteChessman,ChessmanInstance);
			control.InstantiateChessman(4,3,ChessmanState.WhiteChessman,ChessmanInstance);
			control.InstantiateChessman(3,3,ChessmanState.BlackChessman,ChessmanInstance);
			control.InstantiateChessman(4,4,ChessmanState.BlackChessman,ChessmanInstance);
		}
		
		public void GetPlayCoord(int x,int y)
		{
			Debug.Log("获得坐标x"+x+"  y"+y);
			
			Debug.Log("现在要下的棋子为："+Controls.WaittingChessmanState);
			
			if(JumpControlFlag == true){return;}
			
			if(NetGame && MyColor != Controls.WaittingChessmanState)
			{
				Debug.Log("操作过快");
				return;
			}
			
			if(aiscript.AIOpen == true && aiscript.AIturn == true)
			{Debug.Log("操作过快，忽略");return;}
			
			//如果没有任何的地方可以下棋子，跳过
			if(!control.IsAnyPlaceCanToPlay(Controls.WaittingChessmanState))
			{
				Controls.WaittingChessmanState = control.GetOtherState(Controls.WaittingChessmanState);
				if(aiscript.AIOpen == true && aiscript.AIturn ==false)
				{aiscript.AIturn = true;}
				
				Debug.Log("无子可下。跳过");
				return;
			}
			
			bool result =  control.IsEnableToPlay(x,y,Controls.WaittingChessmanState);
			
			Debug.Log("判定结果:"+result);
			
			if(result)
			{
				control.InstantiateChessman(x,y,Controls.WaittingChessmanState,ChessmanInstance);
				control.EatChessman(x,y,Controls.WaittingChessmanState);
				Controls.WaittingChessmanState = control.GetOtherState(Controls.WaittingChessmanState);
				
				//play audio
				audio.Play();
				
				if(NetGame)
				{
					networkView.RPC("UpdateWaittingChessmanState",RPCMode.All,(int)Controls.WaittingChessmanState);
					networkView.RPC("Send",RPCMode.Others,x,y,(int)MyColor);
				}
				
				if(aiscript.AIOpen == true)
				{((AI)GameObject.Find("Main Camera").GetComponent("AI")).AIturn = true;}
			}
		}
		
		[RPC]
		public void Send(int x,int y,int state,NetworkMessageInfo info )
		{
			control.InstantiateChessman(x,y,(ChessmanState)state,ChessmanInstance);
			control.EatChessman(x,y,(ChessmanState)state);
			Controls.WaittingChessmanState = control.GetOtherState((ChessmanState)state);
			
			//play audio
			audio.Play();
			
			networkView.RPC("UpdateWaittingChessmanState",RPCMode.All,(int)Controls.WaittingChessmanState);
			Debug.Log("远程生成棋子："+x+","+y);
		}
		
		[RPC]
		public void SetColor(int state)
		{
			MyColor = (ChessmanState)state;
		}
		
		[RPC]
		public void UpdateWaittingChessmanState(int state)
		{
			Controls.WaittingChessmanState = (ChessmanState)state;
		}
		
		void OnPlayerConnected(NetworkPlayer player)
		{
			Debug.Log("ip: " + player.ipAddress +":" + player.port);
			
			GameObject.Find("Net").SetActive(false);
			
			networkView.RPC("SetColor",RPCMode.Others,(int)ChessmanState.WhiteChessman);
			
			JumpControlFlag = false;
		}
	}
	
}
