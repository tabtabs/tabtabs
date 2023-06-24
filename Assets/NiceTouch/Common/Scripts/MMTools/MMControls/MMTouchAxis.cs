using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MoreMountains.Tools
{	
	[System.Serializable]
	public class AxisEvent : UnityEvent<float> {}

	/// <summary>
	/// 이 구성 요소를 GUI 이미지에 추가하여 축으로 작동하도록 합니다. 
	/// 바인드를 눌렀다가 계속 누르고 인스펙터에서 조치를 해제했습니다.
	/// 마우스 및 멀티 터치 처리
	/// </summary> 
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]

	public class MMTouchAxis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
	{
		public enum ButtonStates { Off, ButtonDown, ButtonPressed, ButtonUp }
	
		/// 축이 눌렸을 때 호출할 메소드
		[Header("Binding")]
		public UnityEvent AxisPressedFirstTime; 
		
		/// 축이 해제될 때 호출할 메서드
		public UnityEvent AxisReleased;
		
		/// 축을 누르고 있는 동안 호출할 메서드
		public AxisEvent AxisPressed;

		/// 축을 누를 때 캔버스 그룹에 적용할 새 불투명도
		[Header("Pressed Behaviour")]
		[Information("Here you can set the opacity of the button when it's pressed. Useful for visual feedback.",InformationAttribute.InformationType.Info,false)]
		public float PressedOpacity = 0.5f;
		
		/// 축을 눌렀을 때 바인딩된 메서드를 보낼 값
		public float AxisValue;

		/// 이것을 true로 설정하면 축을 실제로 눌러야 트리거됩니다. 그렇지 않으면 간단한 마우스 오버로 트리거됩니다(터치 입력에 더 좋음).
		[Header("Mouse Mode")]
		[Information("If you set this to true, you'll need to actually press the axis for it to be triggered, otherwise a simple hover will trigger it (better for touch input).",InformationAttribute.InformationType.Info,false)]
		public bool MouseMode = false;

		public ButtonStates CurrentState { get; protected set; }

	    protected CanvasGroup _canvasGroup;
	    protected float _initialOpacity;

	    /// <summary>
	    /// 시작에서 캔버스 그룹을 가져오고 초기 알파를 설정합니다.
	    /// </summary>
	    protected virtual void Awake()
	    {
		    
			_canvasGroup = GetComponent<CanvasGroup>();
			if (_canvasGroup!=null)
			{
				_initialOpacity = _canvasGroup.alpha;
			}
			ResetButton();
	    }

		/// <summary>
		/// 모든 프레임에서 터치 영역을 누르면 바인딩된 메서드가 있으면 트리거합니다.
		/// </summary>
		protected virtual void Update()
	    {
			if (AxisPressed != null)
			{
				if (CurrentState == ButtonStates.ButtonPressed)
				{
					AxisPressed.Invoke(AxisValue);
				}
	        }
	    }

		/// <summary>
		/// 모든 프레임의 끝에서 필요한 경우 버튼의 상태를 변경합니다.
		/// </summary>
		protected virtual void LateUpdate()
		{
			if (CurrentState == ButtonStates.ButtonUp)
			{
				CurrentState = ButtonStates.Off;
			}
			if (CurrentState == ButtonStates.ButtonDown)
			{
				CurrentState = ButtonStates.ButtonPressed;
			}
		}

		/// <summary>
		/// 바운드 포인터 다운 동작을 트리거합니다.
		/// </summary>
		public virtual void OnPointerDown(PointerEventData data)
	    {
		    
			if (CurrentState != ButtonStates.Off)
			{
				return;
			}

			CurrentState = ButtonStates.ButtonDown;
			if (_canvasGroup!=null)
			{
				_canvasGroup.alpha=PressedOpacity;
			}
			if (AxisPressedFirstTime!=null)
	        {
				AxisPressedFirstTime.Invoke();
	        }
	    }

		/// <summary>
		/// 바인딩된 포인터 위로 작업을 트리거합니다.
		/// </summary>
		public virtual void OnPointerUp(PointerEventData data)
		{
			if (CurrentState != ButtonStates.ButtonPressed && CurrentState != ButtonStates.ButtonDown)
			{
				return;
			}

			CurrentState = ButtonStates.ButtonUp;
			if (_canvasGroup!=null)
			{
				_canvasGroup.alpha=_initialOpacity;
			}
			if (AxisReleased != null)
			{
				AxisReleased.Invoke();
			}
			AxisPressed.Invoke(0);
	    }

		/// <summary>
	    /// OnEnable, we reset our button state
	    /// </summary>
		protected virtual void OnEnable()
	    {
			ResetButton();
	    }

	    /// <summary>
	    /// Resets the button's state and opacity
	    /// </summary>
	    protected virtual void ResetButton()
	    {
			CurrentState = ButtonStates.Off;
			_canvasGroup.alpha = _initialOpacity;
			CurrentState = ButtonStates.Off;
		}

		/// <summary>
		/// 터치가 영역에 들어갈 때 바운드 포인터 입력 작업을 트리거합니다.
		/// </summary>
		public void OnPointerEnter(PointerEventData data)
		{
			if (!MouseMode)
			{
				OnPointerDown (data);
			}
		}

		/// <summary>
		/// 터치가 영역을 벗어날 때 바인딩된 포인터 종료 작업을 트리거합니다.
		/// </summary>
		public void OnPointerExit(PointerEventData data)
		{
			if (!MouseMode)
			{
				OnPointerUp(data);	
			}
		}
	}
}