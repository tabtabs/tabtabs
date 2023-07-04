using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    // 1. ����� �ػ󵵰� ����ȭ�麸�� "����" ���
    //- ����ȭ���� �ʺ�(W)�� �����ؾ� �ϰ� X�� ���� ����Ǿ���Ѵ�. 
    //- 1920x1080�� �ػ� ������ 200x100�ػ� ��⿡ �����Ѵٸ�
    //- X = [1-{(1920/1080)}/(200/100)}] / 2
    //- Y = 0 // �������� ����
    //- W = (1920/1080)/(200/100)
    //- H = 1 // ��������

    // 2. ����� �ػ󵵰� ����ȭ�麸�� �� "����"���
    //- ����ȭ���� ���̰� �����ؾ� �ϸ� Y�� ���� ����Ǿ�� �Ѵ�.
    //- X = 0 // �������� ����
    //- Y = [1-{(100/200)/(1920/1080)}] /2
    //- W = 1 // ����ȭ���� �ʺ�� ����
    //- H = (100/200)/(1920/1080)

    private void Awake()
    {
        SetResolution(1080, 1920);
    }

    void SetCanvasScaler(int _width, int _height)
    {
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        // CanvasScaler ������Ʈ�� ���� ������Ʈ�� ã�Ƽ� ����
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(_width, _height);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand; // Ȯ��
    }
    void SetResolution(int width = 1080, int height = 1920)
    {
        SetCanvasScaler(width, height);

        int deviceWidth = Screen.width; // ����� �ػ� �ʺ�
        int deviceHeight = Screen.height; // ����� �ػ� ����

        Screen.SetResolution(width, (int)(((float)deviceHeight / deviceWidth) * width), true);
        // �ػ� ����

        if ((float)width / height < (float)deviceWidth / deviceHeight)
        {// ���� ����� �ػ󵵺� �� ũ�ٸ�
            float newWidth = ((float)width / height) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
            // ����ī�޶��� ViewPortRect�� ����
            // Rect : X, Y, W, H ��
        }
        else
        {// ����ȭ���� �ػ󵵺� �� ũ�ٸ�
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)width / height);
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);

        }
    }
}
