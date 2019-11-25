using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatTest : MonoBehaviour
{

	public ChatTest(){
		
	}

	public Chat Stage1Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, United Intergalactic Federation, do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Reporting arrival to sector 73118-A2, requesting permission to engage",0);
		c.addLine ("UIF Operator", "Permission Granted",1);
		c.addLine ("UIF Operator", "Xavier 303, be advised there is a rogue frigate red hot in your location",1);
		c.addLine ("UIF Operator", "Apprehend if possible, I repeat apprehend if possible",1);
		c.addLine ("Privisi Xavier", "Copy that UIF, Xavier 303 out", 0);
		c.part = 1;
		return c;
	}

	public Chat Stage1End(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Target responed with aggressive behaviour which resulted in elimination, apprehension failed",0);
		c.addLine ("UIF Operator", "Copy that Xavier 303, failure of apprehension noted, please proceed to sector 5931-O14 when ready",1);
		c.addLine ("Privisi Xavier", "Proceeding to sector 5931-O14, Xavier 303 out",0);
		c.part = 2;
		return c;
	}

	public Chat Stage2Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Reporting arrival to sector 5931-O14",0);
		c.addLine ("UIF Operator", "Copy that Xavier 303, be advised there are signs that a super carrier may have entered your sector," +
			" engage on sight, I repeat engage on sight",1);
		c.addLine ("Privisi Xavier", "Super carrier? Haven't seen one of those in a while...",0);
		c.addLine ("Privisi Xavier", "Very well command, orders recieved, I will engage on sight",0);
		c.addLine ("Privisi Xavier", "Xavier 303 out",0);
		c.part = 3;
		return c;
	}

	public Chat Stage2End(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Reporting succesful elimination of super carrier",0);
		c.addLine ("UIF Operator", "Copy that Xavier 303, congratulations on your success",1);
		c.addLine ("Privisi Xavier", "Command... This super carrier was strange",0);
		c.addLine ("Privisi Xavier", "The speed of nano-creation was faster than what we've ever experienced",0);
		c.addLine ("Privisi Xavier", "And to make things worse, it took a formation at the end that we have never seen before",0);
		c.addLine ("UIF Operator", "Roger that, mayhaps the super-technology be a sign you are nearing Supercluster",1);
		c.addLine ("UIF Operator", "Xavier 303 you are to proceed to Sector 802-Q0 next",1);
		c.addLine ("Privisi Xavier", "Proceeding to sector 802-Q0, Xavier 303 out",0);
		c.part = 4;
		return c;
	}

	public Chat Stage3Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "Xavier 303 approaching asteroid field... Damn no signal",0);
		c.part = 5;
		return c;
	}

	public Chat Stage3End(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "Damn that snake like ship, I've never seen anything like that",0);
		c.part = 6;
		return c;
	}

	public Chat Stage4Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Reporting arrival to sector 802-Q0",0);
		c.addLine ("UIF Operator", "Copy that Xavier 303, We're getting a powerful signal in your sector, please take caution when proceeding",1);
		c.addLine ("Privisi Xavier", "Copy that command, taking due caution and proceeding",0);
		c.addLine ("Privisi Xavier", "I'll take that signal out if it's the last thing I do",0);
		c.addLine ("Privisi Xavier", "Xavier 303 out",0);
		c.part = 7;
		return c;
	}

	public Chat Stage4End(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read me?",0);
		c.addLine ("UIF Operator", "Xavier 303 we read you loud and clear",1);
		c.addLine ("Privisi Xavier", "Reporting destruction of high value target, large cruiser class starship",0);
		c.addLine ("UIF Operator", "Copy that Xavier 303, your contribution will be rewarded",1);
		c.addLine ("Privisi Xavier", "I don't deserve this honor...",0);
		c.addLine ("Privisi Xavier", "I've lost that chance years ago...",0);
		c.addLine ("UIF Operator", "Xavier 303, you didn't come through clearly, please repeat",1);
		c.addLine ("Privisi Xavier", "There is nothing to repeat commander, what is my next sector?",0);
		c.addLine ("UIF Operator", "Xavier 303 please proceed to sector 99-V5",1);
		c.addLine ("Privisi Xavier", "Copy that command. Xavier 303 out",0);
		c.part = 8;
		return c;
	}

	public Chat Stage5Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "This is Xavier 303, UIF do you read?",0);
		c.addLine ("UIF Operator", "Xa---r 3-3, we are d-tec--ng -ignal i-trusi-n",1);
		c.addLine ("Privisi Xavier", "Damn.. Xavier 303 proceeding, I repeat Xavier 303 proceeding",0);
		c.addLine ("UIF Operator", "B- a----ed, -----e s----l ahea-, e-tr-m- ------ --ead, e----- da--er",1);
		c.addLine ("Privisi Xavier", "Shit... This is like last time...",0);
		c.addLine ("Privisi Xavier", "I wont let it become a repeat of last time!!.",0);
		c.part = 9;
		return c;
	}

	public Chat Stage5End(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "That was a big boy",0);
		c.addLine ("Privisi Xavier", "Heading to sector 0, wish me luck fallen comrades",0);
		c.part = 10;
		return c;
	}

	public Chat Stage6Start(){
		Chat c = new Chat ();
		c.addLine ("Privisi Xavier", "It's all or nothing, Xavier 303 out!",0);
		c.part = 11;
		return c;
	}
	public Chat returnChatByPart(int part){
		switch(part){
		case 1:
			return Stage1Start ();
		case 2:
			return Stage1End() ;
		case 3:
			return Stage2Start ();
		case 4:
			return Stage2End ();
		case 5:
			return Stage3Start ();
		case 6:
			return Stage3End() ;
		case 7:
			return Stage4Start ();
		case 8:
			return Stage4End ();
		case 9:
			return Stage5Start ();
		case 10:
			return Stage5End ();
		case 11:
			return Stage6Start ();
		default:
			Chat c = new Chat ();
			c.addLine ("ERROR", "ERROR",1);
			return c;
		}
	}

}

