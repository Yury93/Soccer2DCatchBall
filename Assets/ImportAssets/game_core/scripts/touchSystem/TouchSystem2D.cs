using UnityEngine;
using System.Collections;


/// <summary>
/// Touch system2d(Vector2) class; Deals with the hits on the screen.
/// </summary>
public class TouchSystem2D : MonoBehaviour {

	/// <summary>
	/// Layer mask name for example input.
	/// </summary>
	public 	LayerMask touchInputMask;
	public	Camera	cam;
	
	/// <summary>
	///RAYCASTHIT is the structure used to 
 	///get information back from a raycast 
	/// </summary>
	private RaycastHit2D hit;
	
	void Start()
	{
		if(!cam){cam=Camera.main;}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//MOUSE SECTION: HERE IS DETECTED WHEN SOME BOXCOLLIDER IS CLICKED
		if(Input.GetMouseButtonDown(0))
		{
			//Ray2D ray = camera.ScreenPointToRay (Input.mousePosition);
			hit=Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,touchInputMask);
			if(hit)
			{
				GameObject recipient=hit.transform.gameObject;
				if(Input.GetMouseButtonDown(0))
				{
					recipient.SendMessage ("OnTouchBegan",new Vector3(hit.point.x,hit.point.y,0), SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
		//TOUCH SECTION: HERE IS DETECTED WHEN SOME BOXCOLLIDER IS TOUCHED
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) 
			{
				//Ray ray = camera.ScreenPointToRay (touch.position);
				hit=Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,touchInputMask);
				if(hit)//Physics2D.Raycast(ray,out hit, touchInputMask))
				{
					GameObject recipient=hit.transform.gameObject;
					if(touch.phase==TouchPhase.Began )//|| touch.phase==TouchPhase.Stationary)
					{
						recipient.SendMessage 	("OnTouchBegan",new Vector3(hit.point.x,hit.point.y,0),	SendMessageOptions.DontRequireReceiver);
					}
				}
				
			}
		}
		
	}
}
