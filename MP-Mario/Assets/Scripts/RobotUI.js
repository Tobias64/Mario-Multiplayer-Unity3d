#pragma strict

private static var Player : Transform; //This get the Player Transform

var Speed : float = 1; //This is the Enemy Speed
var Distance : float = 15; //The Distance before he can see the player
var animator: Animator;
var Smooth : float = 2; //This is how fast the Enemy Rotate\


function Update () 
{
	if(GameObject.FindGameObjectWithTag("Player"))
	{
		Player = GameObject.FindGameObjectWithTag("Player").transform;
		//Check the Distance
		if (Vector3.Distance(Player.position,transform.position) <= Distance)
		{

			animator.SetBool("mad", true); //If he see you his color change to Red
			var tempRot = transform.rotation;
			tempRot.z = 0;
			tempRot.x = 0;
			transform.rotation = Quaternion.Slerp(tempRot, Quaternion.LookRotation(Player.position - transform.position), Time.deltaTime * Smooth);
			//Look At the Player
			transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
			//Move Towards the Player
		}
		else
		{
			//sets the enemies animation to false
			animator.SetBool("mad", false); 
		}
	}
}

public static function Tag()
{

}

