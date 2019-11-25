using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global{
   
	public const int RECRUIT = 1;
	public const int VETEREN = 2;
	public const int BATTLEH = 3;
		
	public static int Difficulty = 3;
	public static int Stage;
	public static bool startAt2 = false;
	public static bool startAt3 = false;
	public static bool startAt4 = false;
	public static bool startAt5 = false;
	public static bool startAt6 = false;
    public static bool bossMedley = false;


	public static bool onWeapon = true;
	public static int specialBounds = 1;
	public static bool stationsSpawned = false;

	public static int asteroidLevel = 1;
	public static bool asteroidCancel = true;

	public static bool tutorial = false;

    public static bool final = false;
	public static bool finalmusic = false;
    public static float volumeMult = 1.0f;

	// Weapons
	public const int BASIC = 101;           //~3.2 DPS - base line
	public const int CANNON = 102;          //~3.625 DPS
	public const int SHOTGUN = 103;         //~3.5 DPS if all hit
	public const int RNG = 104;             //~3.18 DPS - balanced
    public const int PLAYERICEWEP = 105;    //~2.4 DPS except it slows down enemies
    public const int REDOX = 106;           //2.25 DPS except its at max debuff
    public const int BLITZGUN = 107;        //16 DPS except i calculated this by hand cause i didnt get it to work

    public const int CANNON2 = 201;         //~7.25 DPS - balanced
	public const int SHOTGUN2 = 202;        //~6.6 DPS if all hit - balanced
    public const int SNIPER = 203;          //~1.5 DPS except its 5 damage per shot + piercing    
    public const int MINIGUN = 204;         //~7.475 except it has terrible spread - balanced
    public const int VELKOZ = 205;          //~7.45 DPS with maximum flak hits or 6 DPS with no flak hits - balanced
    public const int HDRONEI = 206;         //~6.2 DPS - balanced if not weird to use
    public const int FLAK = 207;            //~6.24 DPS if all hit - balanced
    public const int GUIDEDLAUNCHER = 208;  //~lower than 15 DPS

    //Weapons in stage 3
    public const int HDRONEII = 301;        //~10.7 ish DPS - balanced
    public const int SATCANNON = 302;       //1-2 DPS except its 10 damage per shot
    public const int RNG2 = 303;            //~6.1 to 6.7 DPS plus a bit more - balanced 
    public const int PLAYERICEWEP2 = 304;   //~6.75 DPS except it has minimal flak hitting the enemy and slows down enemies - balanced
    public const int REDOX2 = 305;          //~7.36 DPS except its at max debuff - balanced
    public const int HELLFIRE = 306;        //~7.05 DPS - balanced

    //Stage 4
    public const int RPR = 401;             //~12 DPS
    public const int LAS1 = 402;            //~10 DPS
    public const int SPLITTER = 403;        //~13+ DPS except all flaks need to hit
    public const int BOUNCELAUNCHER = 404;  //~12 DPS
    public const int RNG3 = 405;            //~12 to 14 DPS 
    public const int HDRONEIII = 406;       //~13.5 ish DPS

    // Stage 5
    public const int SATCANNON2 = 501;      //15 DPS
    public const int SNIPER2 = 502;         //15.75 DPS
    public const int PLASMACOIL = 503;      //balanced
    public const int FLAMETHROWER = 504;    //balanced
    public const int DARKWEAPON = 505;      //balanced
    public const int BUZZSAW = 506;         //
    public const int RAILGUN = 507;         //balanced

    // Stage 6
    public const int CANNON3 = 601;         //~17 DPS with all hit    
    public const int RPR2 = 602;            //~16.2 DPS
    public const int SHOTGUN3 = 603;        //~15.5 DPS with all hit
    public const int MEGAMINIGUN = 604;     //~17.3 DPS
    public const int MJOLNIR = 605;         //~13 DPS at 20 damage per hit
    







    //Abilities
    public const int DRONE = 1;
    public const int FRENZY = 2;
    public const int PHASE = 3;
    public const int SHIELD = 4;
    public const int SPEEDBOOST = 5;
    public const int STASISBUBBLE = 6;
    public const int SWORD = 7;
    public const int TELEPORT = 8;
    public const int COMPOSITEWEAPONRY = 9;
    public const int APREGENBOOST = 10;
    public const int HEAL = 11;
    public const int LASTSTAND = 12;
    public const int BFOLASER = 13;
    public const int EMP = 14;
    public const int SHIELDREGEN = 15;
    public const int BULLETRING = 16;
    public const int ROCKETRING = 17;
    public const int TIMESLOW = 18;
    public const int REGENERATOR = 19;
    public const int SWORDRING = 20;
    public const int SHIELDPASSIVE = 21;
    public const int COMPANIONDRONEI = 22;
    public const int COMPANIONDRONEII = 23;
    public const int DRONEMKII = 24;
}
