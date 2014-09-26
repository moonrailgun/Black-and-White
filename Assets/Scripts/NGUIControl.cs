using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite{
	public class NGUIControl : MonoBehaviour {
		private bool isGodView = true;
		private Vector3 GodViewPos = new Vector3(8,15,8);
		private Quaternion GodViewRot;
		private Vector3 NormalViewPos = new Vector3(8,10,-0.4f);
		private Quaternion NormalViewRot;
		//private int MaxScreenWidth = Screen.width;
		//private int MaxScreenHeight = Screen.height;
		
		private int ChessmanNum = 0;
		private int SpaceNum;
		private int BlackChessmanNum;
		private int WhiteChessmanNum;
		private string InformationText;
		
		public Material cb;
		public Material cb2;
		
		GameObject MainCamera;
		GameObject Informations;
		GameObject EndWindow;
		CheckChess checkchess;
		
		void Awake()
		{
			MainCamera = GameObject.Find("Main Camera");
			NormalViewRot = GodViewRot = MainCamera.transform.rotation;
			NormalViewRot.eulerAngles = new Vector3(60,0,0);
			
			Informations = GameObject.Find("Informations");
			checkchess = MainCamera.GetComponent<CheckChess>();
			
			
			EndWindow = GameObject.Find("EndWindow");
		}
		
		void Start()
		{
			Informations.SetActive(false);
			EndWindow.SetActive(false);
		}
		
		void Update()
		{
			if(Input.GetKey(KeyCode.Tab))
			{
				Informations.SetActive(true);
			}
			else
			{
				Informations.SetActive(false);
			}
			
			//view
			if(isGodView)
			{
				MainCamera.transform.position = GodViewPos;
				MainCamera.transform.rotation = GodViewRot;
			}
			else 
			{
				MainCamera.transform.position = NormalViewPos;
				MainCamera.transform.rotation = NormalViewRot;
			}
			
			//count
			SpaceNum = BlackChessmanNum = WhiteChessmanNum = 0;
			for(int i = 0;i<8;i++)
			{
				for(int j = 0;j<8;j++)
				{
					if(Controls.Chessbroad[i,j] == null || Controls.Chessbroad[i,j].State == ChessmanState.Null)
					{
						SpaceNum++;
					}
					else
					{
						if(Controls.Chessbroad[i,j].State == ChessmanState.BlackChessman)
						{
							BlackChessmanNum++;
						}
						else if(Controls.Chessbroad[i,j].State == ChessmanState.WhiteChessman)
						{
							WhiteChessmanNum++;
						}
					}
					
				}
			}
			
			//txt
			InformationText = "";
			ChessmanNum = BlackChessmanNum+WhiteChessmanNum;
			
			InformationText += "游戏时间："+Mathf.RoundToInt(Time.time)+"秒\n";
			InformationText += "棋子总数："+ChessmanNum+"\n";
			InformationText += "空白数："+SpaceNum+"\n";
			InformationText += "黑子数："+BlackChessmanNum+"\n";
			InformationText += "白子数："+WhiteChessmanNum+"\n";
			
			InformationText += "待下棋子："+Controls.WaittingChessmanState.ToString()+"\n";
			
			InformationText += "游戏模式："+Application.loadedLevelName+"\n";
			
			if(checkchess.NetGame == true)
			{
				InformationText += "所执棋子："+ checkchess.MyColor + "\n";
			}
			
			Informations.GetComponent<UILabel>().text = InformationText;

			
			JudgeWinAndLose();
			
		}
		
		public void ChangeViewFlag(){
			isGodView = !isGodView;
			Debug.Log(isGodView);
		}
		
		void JudgeWinAndLose()
		{
			if((BlackChessmanNum == 0 || WhiteChessmanNum == 0 || SpaceNum <= 5) && (checkchess.control.IsAnyPlaceCanToPlay(ChessmanState.BlackChessman) == false &&checkchess.control.IsAnyPlaceCanToPlay(ChessmanState.BlackChessman) == false))
			{
				string text;
				
				if(BlackChessmanNum > WhiteChessmanNum)
				{
					print("游戏结果：黑方胜利!  游戏时间："+System.DateTime.Now);
					text = "黑方胜利!";
				}
				else if(WhiteChessmanNum > BlackChessmanNum)
				{
					print("游戏结果：白方胜利!  游戏时间："+System.DateTime.Now);
					text = "白方胜利!";
				}
				else
				{
					print("游戏结果：平局!  游戏时间："+System.DateTime.Now);
					text = "平局!";
				}
				
				//game over
				checkchess.enabled = false;
				MainCamera.GetComponent<AI>().enabled = false;
				this.enabled = false;
				//show end window
				ShowEndWindow(text);
			}
		}
		
		public void BackToMenu()
		{
			Application.LoadLevel("StartMenu");
		}
		
		public void Restart()
		{
			string LoadedName = Application.loadedLevelName;
			Application.LoadLevel(LoadedName);
		}
		
		public void ShowEndWindow(string text)
		{
			text += "\n黑"+BlackChessmanNum+"\n白"+WhiteChessmanNum;
			
			EndWindow.SetActive(true);
			GameObject.Find("EndWindow/Label").GetComponent<UILabel>().text = text;
		}
	}
}