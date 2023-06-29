using UnityEngine;

public class CopyPosition : MonoBehaviour
{
	[SerializeField]
	private	bool		x, y, z;	
	[SerializeField]
	private	Transform	target;		

	private void Update()
	{
		if ( !target ) return;

		transform.position = new Vector3( // 미니맵을 바라보는 카메라가 플레이어를 따라가게하는 함수
			(x ? target.position.x : transform.position.x), 
			(y ? target.position.y : transform.position.y),
			(z ? target.position.z : transform.position.z));
	}
}

