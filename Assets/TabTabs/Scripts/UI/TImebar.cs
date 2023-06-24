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
        // fillAmount�� �ִ밪�� 1�̴�.
        // 1�ʰ� ������ 0.1�� ����(10�ʱ���)
        if (timebarImage.fillAmount<=0)
        {// fillAmount���� 0 ���϶��
            timebarImage.fillAmount = 1.0f;
            // timebar �ʱ�ȭ
            // ĳ������ hp�� ��´�.
        }
    }
}
