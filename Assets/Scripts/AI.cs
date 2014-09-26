using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class AI : MonoBehaviour {
		
		public bool AIOpen;
		
		public GameObject ChessmanInstance;
		
		public ChessmanState AIChessmanState;
		
		public bool AIturn = false;
		
		public float delayTime = 1.0f;
		
		public float time;
		
		int x,y,maxnum;
		
		AudioSource audio;
		
		Controls AIcontrol;
		
		void Start () 
		{
			AIcontrol = new Controls(ChessmanInstance);
			x=-1;y=-1;maxnum = 0;
			
			time = delayTime;
			
			AIChessmanState = ChessmanState.WhiteChessman;
			
			AIturn = false;
			
			audio = this.GetComponent<AudioSource>();
		}
		
		// Update is called once per frame
		void Update ()
		{
			//Debug.Log(time);
			if(AIOpen && Controls.WaittingChessmanState == AIChessmanState && AIturn)
			{
				//AI
				
				//delay
				time -= Time.deltaTime;
				
				if(AIturn == true && time < 0)
				{
					AIPlay();
					AIturn = false;
					time = delayTime;
				}
			}
		}
		
		private void AIPlay()
		{
			if(!AIturn)
			{return;}
			
			if(!AIcontrol.IsAnyPlaceCanToPlay(AIChessmanState))
			{
				AIturn = false;
				Controls.WaittingChessmanState = AIcontrol.GetOtherState(Controls.WaittingChessmanState);
				
				Debug.Log("AI:无子可下。跳过");
				return;
			}
			
			for(int i = 0;i<8;i++)
			{
				for (int j = 0;j<8 ;j++)
				{
					int num = AIcontrol.GetCanEatChessmanNum(i,j,AIChessmanState);
					if(num != 0)
					{
						Debug.Log(i+","+j+","+num);
						if(num > maxnum)
						{
							x=i;y=j;maxnum = num;
						}
					}
				}
			}
			
			Debug.Log("最终遍历结果:"+x+"," + y+"," + maxnum);
			
			if(maxnum > 0)
			{
				AIcontrol.InstantiateChessman(x,y,AIChessmanState,ChessmanInstance);
				AIcontrol.EatChessman(x,y,AIChessmanState);
				Controls.WaittingChessmanState = AIcontrol.GetOtherState(AIChessmanState);
				
				//play audio
				audio.Play();
				
				Debug.Log("电脑生成棋子"+x+","+y);
				x=-1;y=-1;maxnum = 0;
			}
		}
	}
}
