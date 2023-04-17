using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�R���\�[���ɕ`��
    void PrintArray()
    {
        string debugText = "";
        for(int i=0;i<map.Length;i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    //�v���C���[�̈ʒu���擾
    int GetPlayerIndex()
    {
        for(int i=0;i<map.Length;i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    //�����̈ړ�
    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo<0||moveTo>=map.Length)
        {
            return false;
        }
        //�ړ����2(��)��������
        if (map[moveTo]==2)
        {
            //�ǂ̕����ֈړ����邩�Z�o
            int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������
            //���̈ړ������BMoveNumber���\�b�h����MoveNumber���\�b�h��
            //�ĂсA�������ċA���Ă���B�ړ��s��bool�ŋL�^
            bool succes = MoveNumber(2, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if(!succes)
            {
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    //�z��̐錾
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //1�̉E�ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //�ʒu�擾
            int playerIndex = GetPlayerIndex();
            //�����̈ړ�
            MoveNumber(1, playerIndex, playerIndex + 1);
            //�����̕`��
            PrintArray();
        }

        //1�̍��ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
