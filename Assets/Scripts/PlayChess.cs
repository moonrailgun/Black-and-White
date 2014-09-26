using UnityEngine;
using System.Collections;

namespace MRG.BlackAndWhite
{
	public class PlayChess : MonoBehaviour {
		//下棋
		private GameObject MainCamera;
		private GameObject NGUI;
		
		Material cb,cb2;
		
		void Start()
		{
			NGUI = GameObject.Find("UI Root/Camera/Panel");
			MainCamera = GameObject.FindWithTag("Player");
			
			cb = NGUI.GetComponent<NGUIControl>().cb;
			cb2 = NGUI.GetComponent<NGUIControl>().cb2;
			
			renderer.material = cb;
		}
		
		void OnMouseDown() {
			Vector3 pos = this.transform.position;
			int x,y;
			x = (int)(pos.x-1)/2;
			y = (int)(pos.z-1)/2;
			
			//call function
			MainCamera.GetComponent<CheckChess>().GetPlayCoord(x,y);
		}
		
		void OnMouseOver(){
			renderer.material = cb2;
		}
		
		void OnMouseExit() {
			renderer.material = cb;
		}
	}
}

