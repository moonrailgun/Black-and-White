using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class Controls
	{
		public GameObject ChessmanInstance;
		
		static public Chessman[,] Chessbroad = new Chessman[8,8];
		
		static public ChessmanState WaittingChessmanState;
		
		public Controls(GameObject a)
		{
			ChessmanInstance = a;
		}
		
		//check a coord is enable to play
		//x,y are check coords
		//state is chessman's state whice want to play 
		public bool IsEnableToPlay(int x,int y,ChessmanState State)
		{
			ChessmanState MainState = State;
			ChessmanState SecondState = GetOtherState(MainState);
			
			if(Chessbroad[x,y] != null)
			{
				return false;
			}
			
			for(int i=-1;i<=1;i++)
			{
				for(int j=-1;j<=1;j++)
				{
					if(x+i<=7&&x+i>=0&&y+j<=7&&y+j>=0)//addressing confine
					{	
						if(Chessbroad[x+i,y+j] != null && Chessbroad[x+i,y+j].State == SecondState)
						{
							//Debug.Log("得到一条路径"+i+","+j);
							//one path
							int tempx = x+i;
							int tempy = y+j;
							for(int k=0; tempx<=7&&tempx>=0&&tempy<=7&&tempy>=0&&(Chessbroad[tempx,tempy]!=null); tempx+=i,tempy+=j,k++)
							{
								//Debug.Log(tempx+","+tempy);
								//Debug.Log(MainState);
								//Debug.Log(ChessBroad[tempx,tempy].State);
								if(Chessbroad[tempx,tempy].State == MainState)
								{
									return true;
								}
							}
						}
					}
				}
			}
			//Debug.Log("this");
			return false;
		}
		
		public ChessmanState GetOtherState(ChessmanState State1)
		{
			if(State1 == ChessmanState.BlackChessman) return ChessmanState.WhiteChessman;
			else if (State1 == ChessmanState.WhiteChessman) return ChessmanState.BlackChessman;
			else return ChessmanState.Null;
		}
		
		public void EatChessman(int x,int y,ChessmanState State)
		{
			ChessmanState MainState = State;
			ChessmanState SecondState = GetOtherState(MainState);
			
			//Debug.Log("main state:"+ MainState);
			//Debug.Log("second state:"+ SecondState);
			
			for(int i=-1;i<=1;i++)
			{
				for(int j=-1;j<=1;j++)
				{
					if(x+i<=7&&x+i>=0&&y+j<=7&&y+j>=0)//addressing confine
					{	
						if(Chessbroad[x+i,y+j] != null && Chessbroad[x+i,y+j].State == SecondState)
						{
							Debug.Log("找到路径"+i+","+j);
							//one path
							
							if(!IsPathEnableToEat(i,j,x,y,MainState))
							{
								continue;
							}
							
							int tempx = x+i;
							int tempy = y+j;
							for(int k=0; tempx<=7&&tempx>=0&&tempy<=7&&tempy>=0&&(Chessbroad[tempx,tempy]!=null); tempx+=i,tempy+=j,k++)
							{
								if(Chessbroad[tempx,tempy].State == MainState)
								{
									break;//jump out this path
								}
								//Debug.Log(tempx+","+tempy);
								Chessbroad[tempx,tempy].ChangeColor(SecondState);
							}
						}
					}
				}
			}
		}
		
		private bool IsPathEnableToEat(int i,int j ,int baseX,int baseY,ChessmanState state)
		{
			ChessmanState MainState = state;
			
			int x = baseX+i;
			int y = baseY+j;
			
			for(;x<=7&&x>=0&&y<=7&&y>=0;x+=i,y+=j)
			{
				Debug.Log(x+","+y+","+i+","+j);
				
				if(Chessbroad[x,y] == null)
				{
					return false;
				}
				if(Chessbroad[x,y].State == MainState)
				{
					return true;
				}
			}
			return false;
		}
		
		public int GetCanEatChessmanNum(int x,int y,ChessmanState AIChessmanState)
		{
			//is this part can play?
			if(!IsEnableToPlay(x,y,AIChessmanState))
			{
				return 0;
			}
			
			int sum = 0;
			ChessmanState MainState = AIChessmanState;
			ChessmanState SecondState = GetOtherState(MainState);
			
			for(int i=-1;i<=1;i++)
			{
				for(int j=-1;j<=1;j++)
				{
					if(x+i<=7&&x+i>=0&&y+j<=7&&y+j>=0)//addressing confine
					{	
						if(Chessbroad[x+i,y+j] != null && Chessbroad[x+i,y+j].State == SecondState)
						{
							//Debug.Log("找到路径"+i+","+j);
							//one path
							
							if(!IsPathEnableToEat(i,j,x,y,MainState))
							{
								continue;
							}
							
							int tempx = x+i;
							int tempy = y+j;
							for(int k=0; tempx<=7&&tempx>=0&&tempy<=7&&tempy>=0&&(Chessbroad[tempx,tempy]!=null); tempx+=i,tempy+=j,k++)
							{
								if(Chessbroad[tempx,tempy].State == MainState)
								{
									sum += k;
									break;//jump out this path
								}
								//Debug.Log(tempx+","+tempy);
							}
						}
					}
				}
			}
			
			return sum;
		}
		
		public void InstantiateChessman(int x,int y,ChessmanState state,GameObject ChessmanInstance)
		{
			if(x>7 || x<0 || y>7 || y<0)
			{
				Debug.Log("坐标超出棋盘");
				return;
			}
			
			Vector3 pos = new Vector3(x*2+1,0.25f,y*2+1);
			
			GameObject chessman = (GameObject)MonoBehaviour.Instantiate(ChessmanInstance,pos,Quaternion.identity);
			
			Chessbroad[x,y] = new Chessman(x,y,state,chessman);
		}
		
		public bool IsAnyPlaceCanToPlay(ChessmanState state)
		{
			for(int i=0;i<8;i++)
			{
				for(int j=0;j<8;j++)
				{
					if(IsEnableToPlay(i,j,state))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
