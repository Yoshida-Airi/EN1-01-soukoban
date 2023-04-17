using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //コンソールに描画
    void PrintArray()
    {
        string debugText = "";
        for(int i=0;i<map.Length;i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    //プレイヤーの位置を取得
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

    //数字の移動
    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        //移動先が範囲外なら移動不可
        if(moveTo<0||moveTo>=map.Length)
        {
            return false;
        }
        //移動先に2(箱)が居たら
        if (map[moveTo]==2)
        {
            //どの方向へ移動するか算出
            int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先へ2(箱)を移動させる
            //箱の移動処理。MoveNumberメソッド内でMoveNumberメソッドを
            //呼び、処理が再帰している。移動可不可をboolで記録
            bool succes = MoveNumber(2, moveTo, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーの移動も失敗
            if(!succes)
            {
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    //配列の宣言
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //1の右移動(プレイヤー)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //位置取得
            int playerIndex = GetPlayerIndex();
            //数字の移動
            MoveNumber(1, playerIndex, playerIndex + 1);
            //文字の描画
            PrintArray();
        }

        //1の左移動(プレイヤー)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
