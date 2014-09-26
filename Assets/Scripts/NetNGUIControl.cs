using UnityEngine;
using System.Collections;
using System.Net;

namespace MRG.BlackAndWhite
{
	public class NetNGUIControl : MonoBehaviour {
		UILabel label;
		float time;
		string text = "";
		string addtext = "";
		int i =0;
		
		// Use this for initialization
		void Start () {
			label = GameObject.Find("Net/Texture/Label").GetComponent<UILabel>();
			time = 1;
			if(Menu.IPAddress == "")
			{
				string hostName = Dns.GetHostName();
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				
				
				foreach(IPAddress a in hostEntry.AddressList)
				{
					string IP = a.ToString();
					if(IP.Contains("192.168"))
					{
						text += IP + "\n";
					}
				}
				text += "正在等待玩家加入\n";
			}
			else
			{
				text = "正在加入"+Menu.IPAddress+"\n";
			}
		}
		
		// Update is called once per frame
		void Update () {
			time -= Time.deltaTime;
			if(time < 0)
			{
				i++;
				time = 1;
				addtext += "。";
				if(i>3)
				{
					addtext = "";
					i = 0;
				}
			}
			
			label.text = text+addtext;
		}
	}
}