using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public enum ChessmanState
	{
		BlackChessman,
		WhiteChessman,
		Null
	}
	
	public class Chessman
	{
		
		public GameObject ChessmanObject;
		public ChessmanState State = ChessmanState.Null;
		public int CoordX;
		public int CoordY;
		
		public Chessman(int x,int y,ChessmanState state,GameObject Instance)
		{
			CoordX = x;
			CoordY = y;
			State = state;
			ChessmanObject = Instance;
			
			if(state == ChessmanState.BlackChessman)
			{
				ShowBlack();
			}
			else if(state == ChessmanState.WhiteChessman)
			{
				ShowWhite();
			}
		}
		
		#region animation part
		public void ShowWhite(){
			ChessmanObject.animation.Play("idle_white");
		}
		
		public void ShowBlack(){
			ChessmanObject.animation.Play("idle_black");
		}
		
		private void WhiteToBlack(){
			ChessmanObject.animation.Play("whitetoblack");
			if(State == ChessmanState.WhiteChessman)
			{
				State = ChessmanState.BlackChessman;
			}
		}
		
		private void BlackToWhite(){
			ChessmanObject.animation.Play("blacktowhite");
			if(State == ChessmanState.BlackChessman)
			{
				State = ChessmanState.WhiteChessman;
			}
		}
		
		public void ChangeColor()
		{
			if(State == ChessmanState.BlackChessman) {BlackToWhite();}
			else if(State == ChessmanState.WhiteChessman) {WhiteToBlack();}
			else Debug.Log("出错！！！！！！！！！！！！！！！！！！！");
		}
		
		public void ChangeColor(ChessmanState fromState)
		{
			if(fromState == ChessmanState.BlackChessman) {BlackToWhite();}
			else if(fromState == ChessmanState.WhiteChessman) {WhiteToBlack();}
			else Debug.Log("出错！！！！！！！！！！！！！！！！！！！");
		}
		
		public void UpdataChessmanState()
		{
			if(State == ChessmanState.WhiteChessman)
			{
				ChessmanObject.animation.Play("idle_white");
			}
			else if(State == ChessmanState.BlackChessman)
			{
				ChessmanObject.animation.Play("idle_black");
			}
		}
		
		#endregion
	}
	
	
	
}
