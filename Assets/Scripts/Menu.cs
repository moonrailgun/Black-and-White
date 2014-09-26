using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class Menu : MonoBehaviour {
		static public string IPAddress = "";
		
		GameObject NetWork;
		GameObject Panel;
		GameObject Message;
		
		void Awake()
		{
			NetWork = GameObject.Find("NetWork");
			Panel = GameObject.Find("Panel");
			Message = GameObject.Find("NetWork/Input/Label");
		}
		
		void Start()
		{
			NetWork.SetActive(false);
		}
		
		public void SolitaireGameOpen(){
			Application.LoadLevel("SolitaireGame");
		}
		
		public void MultiplayerGameOpen(){
			Application.LoadLevel("MultiplayerGame");
		}
		
		public void NetGameOpen(){
			Panel.SetActive(false);
			NetWork.SetActive(true);
		}
		
		public void BackToMenu()
		{
			Panel.SetActive(true);
			NetWork.SetActive(false);
		}
		
		public void AddMessage(string message)
		{
			Message.GetComponent<UILabel>().text += message + "\n";
		}
		
		public void LinkToNetWork()
		{
			IPAddress = GameObject.Find("NetWork/Input/Label").GetComponent<UILabel>().text;
			Application.LoadLevel("NetGame");
		}
		
		public void CreateGame()
		{
			IPAddress = "";
			Application.LoadLevel("NetGame");
		}
	}
}
