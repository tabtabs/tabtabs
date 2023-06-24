A more user friendly doc is available at http://www.moremountains.com/nice-touch-documentation
Find out more about the asset at http://www.moremountains.com/nice-touch-most-simple-multitouch-virtual-input-unity

Nice Touch v1.4

## WHAT'S IN THE ASSET ?
-------------------------

자산에는 Common 및 Demos라는 두 개의 기본 폴더가 있습니다. 
이름에서 알 수 있듯이 Common에는 모바일 컨트롤이 작동하는 데 필요한 모든 스크립트와 시각적 자산이 포함되어 있습니다.
게임에 해당 폴더를 유지하고 싶을 것입니다. 
모든 스크립트는 Scripts/MMTools 폴더에 그룹화됩니다.
Nice Touch의 핵심 스크립트는 Corgi Engine과 Infinite Runner Engine에서 사용되는 더 큰 라이브러리인 MMTools의 일부이기 때문입니다.

데모에는 두 개의 데모 장면이 포함되어 있습니다.
NiceTouchTestScene은 철저한 디버그 장면입니다.
가능한 모든 이벤트(조이스틱을 한 방향으로 밀거나 버튼을 눌렀다 떼는 등)에 대해 콘솔 또는 화면 로그를 트리거합니다.
정 이벤트를 바인딩하는 방법이 궁금할 때 유용할 수 있습니다.
CubeCylinderSphere(I'm good at names)는 Nice Touch 컨트롤을 사용하여 세 가지 다른 "캐릭터"를 제어하는 매우 미니멀한 게임입니다.
기본적이지만 이러한 컨트롤을 게임에 통합할 수 있는 방법에 대한 좋은 예입니다. 데모 폴더는 컨트롤이 작동하는 데 필요하지 않으므로 안전하게 제거할 수 있습니다.


## 이것을 내 게임에 어떻게 추가합니까?
------------------------------

완전히 새로운 게임을 시작하거나 지금 장면에 GUI가 없는 경우 가장 빠른 방법은 UICamera 프리팹(Common/Resources/에 있음)을 장면으로 끌어다 놓는 것입니다.
이미 GUI가 있는 경우 가장 빠른 방법이기도 하지만 해당 프리팹을 장면으로 드래그하고 펼친 다음 MMControls 부분을 가져와 기존 캔버스에 넣는 것이 좋습니다.

그런 다음 (잠재적으로) 컨트롤을 원하는 대로 재배치할 수 있습니다. Unity의 uGUI 모범 사례를 사용하여 구축되었습니다. 익숙하지 않은 경우 Unity 웹사이트에 수많은 정보가 있습니다. 기본적으로 다른 물체처럼 물건을 움직일 수 있습니다. 또한 일부 컨트롤을 제거하고 싶을 수도 있습니다(두 개의 조이스틱과 D-패드가 필요한 사람은 누구입니까?).

이제 남은 작업은 이러한 컨트롤을 게임에 바인딩하는 것입니다.
Nice Touch는 게임 클래스의 대상 기능, 메서드를 제어합니다.
일반적으로 모든 기존(키보드 또는 게임패드) 입력을 처리하는 InputManager 클래스를 대상으로 하거나 캐릭터를 직접 대상으로 지정할 수 있습니다.
첫 번째 방법은 Corgi Engine 통합을 위해 선택되었으며 CubeCylinderSphere 데모 장면에 두 번째 방법의 예가 있습니다.
어쨌든 주요 아이디어는 동일합니다. 컨트롤을 메서드에 연결해야 합니다. 상호 작용할 때 이(또는 이러한) 메서드를 트리거합니다.

즉, 게임 측에 깨끗한 메서드가 필요합니다. 예를 들어, 다음은 내 테스트 CharacterMovement 클래스의 메서드입니다. 매우 기본적이지만 다음과 같은 아이디어를 얻을 수 있습니다.

public virtual void Move(Vector2 newMovement)
{
  if (!_axisBased)
  {
    _horizontalMovement = newMovement.x;
    _verticalMovement = newMovement.y;
  }
}

public virtual void Jump()
{
  _rigidbody.AddForce(Vector3.up * (JumpForce * -Physics.gravity.y));
}

As you can see, calling the Jump method (once) will make the character go up in the air, and passing a Vector2 to the Move method will modify the horizontal and vertical movement values of the character, which will then be applied to its position at Update(). Now if I want to have a button trigger the jump for that character everytime it's pressed for the first time (before a release), here's what I have to do :

- In the button's inspector, in the MMTouchButton component part, in the ButtonPressedFirstTime() box, select my character in the Object property
- In the newly populated dropdown of that same box, select my character's class, and then my Jump method
- That's it.
As you can see, there's not much to it. Here are a few remarks :

- Joysticks and arrows require "dynamic" methods. Which is Unity's way of naming methods that take one (or more) parameters. Joysticks will need a method that takes a Vector2 as a parameter, and Arrows will require a method with a float parameter.
These "dynamic" methods will always be at the very top when you open your character's class dropdown from the inspector
- For joysticks, you need to select the joystick knob to make changes, the upper level gameobject is just the "base" of your joystick, and is purely cosmetic, get rid of it if you want
- For joysticks, a Target Camera must be set. If you've just drag and dropped and are using the UICamera prefab, there's nothing to do. If you've changed the main UI camera, you'll just need to specify it (it's the first item in the MMTouchJoystick's inspector)
For all the controls, from the inspector, you can change the pressed opacity, this is the opacity that will be applied when the control is interacted with


## 플랫폼 감지는 어떻게 작동합니까?
-------------------------------------

Hierarchy 패널을 보면 모든 컨트롤이 MMTouchControls 게임 개체 아래에 중첩되어 있음을 알 수 있습니다.
이 게임 개체는 기본적으로 MMTouchControls 구성 요소가 있는 CanvasGroup입니다. (사용하려는 경우) 모바일 감지를 처리하는 것은 이 구성 요소입니다.
매우 간단한 방식으로 작동합니다. 모바일 플랫폼(iOS 또는 Android)을 대상으로 하는 경우 재생을 누르면 컨트롤이 표시됩니다.
다른 플랫폼을 대상으로 하는 경우 해당 플랫폼이 숨겨집니다. 인스펙터에서 한 모드 또는 다른 모드를 강제할 수도 있습니다.

## 어딘가에 문서가 있습니까?
-------------------------------------

있다!
There's a complete API documentation at http://www.moremountains.com/nice-touch/Docs/index.html
And a functional documentation at http://reunono.github.io/NiceTouch/

## 여전히 질문이 있습니다!
---------------------------

If something's still not clear, you can always drop me a line using the form at http://www.moremountains.com/nice-touch-documentation. It's entirely possible that I forgot to describe something in this page, but please make sure you've read it all before filling this form. You can also please check the FAQ before sending me an email. Chances are, your question's answered right there. If it's not, then go ahead!
Also, if you're asking for support, please send me your invoice number, along with your Unity version and the version of Nice Touch you're using, so I can help you best.
