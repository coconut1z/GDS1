using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chat
{

	public List<string> name;
	public List<string> text;
	public List<int> imageNumber;
	public int part;

	public Chat(){
		name = new List<string> ();
		text = new List<string> ();
		imageNumber = new List<int> ();
		part = 0;
	}

	public void addLine(string name, string text, int num){
		this.name.Add (name);
		this.text.Add (text);
		imageNumber.Add (num);
	}
}

