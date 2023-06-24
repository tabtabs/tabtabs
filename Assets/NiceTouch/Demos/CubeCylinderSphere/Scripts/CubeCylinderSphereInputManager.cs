using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.NiceTouch
{	
	/// <summary>
	/// 이 영구 싱글톤은 입력을 처리하고 플레이어에게 명령을 보냅니다.
	/// </summary>
	public class CubeCylinderSphereInputManager : MonoBehaviour
	{
		
		public MMTouchControls MobileControls;

		public CharacterMovement CubeCharacter;

	    protected float _horizontalMove = 0;
	    protected float _verticalMove = 0;

	    
	    /// <summary>
	    /// 업데이트 시 다양한 명령을 확인하여 플레이어에게 전송합니다.
	    /// </summary>
	    protected virtual void Update()
		{		

			if (!MobileControls.IsMobile && CubeCharacter!=null)
			{
				NonMobileCubeMovement();

				if (Input.GetButtonUp("Jump"))
				{
					NonMobileCubeJump();
				}
			}		
				
		}

		public virtual void NonMobileCubeMovement()
		{
			if (!MobileControls.IsMobile)
			{
				CubeCharacter.Move(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));	
			}
		}

		public virtual void NonMobileCubeJump()
		{
			CubeCharacter.Jump();
		}
	}
}