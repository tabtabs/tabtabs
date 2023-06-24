using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImebar : MonoBehaviour
{
    Image timebarImage;
    void Start()
    {
        timebarImage = GetComponent<Image>();
    }

    void Update()
    {
        timebarImage.fillAmount -= Time.deltaTime * 0.1f;
        // fillAmount의 최대값은 1이다.
        // 1초가 지나면 0.1이 깎임(10초기준)
        if (timebarImage.fillAmount<=0)
        {// fillAmount값이 0 이하라면
            timebarImage.fillAmount = 1.0f;
            // timebar 초기화
            // 캐릭터의 hp를 깎는다.
        }
    }
}
