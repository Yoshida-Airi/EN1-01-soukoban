using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Timeline;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�v���C���[�̈ʒu���擾
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null)
                {
                    continue;
                }
                if (field[y,x].tag=="Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }
    //�����̈ړ�
    bool MoveNumber(string tag,Vector2Int moveFrom,Vector2Int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo.y<0||moveTo.y>=field.GetLength(0))
        {
            return false;
        }
        if (moveTo.x < 0||moveTo.x>=field.GetLength(1))
        {
            return false;
        }

        //�ړ����2(��)��������
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y,moveTo.x].tag=="Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if(!success)
            {
                return false;
            }
        }

        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
       
    }

    //�N���A�\��
   bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();
        for (int y = 0; y < map.GetLength(0); y++) 
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y,x]==3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for(int i=0;i<goals.Count;i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if(f==null||f.tag!="Box")
            {
                return false;
            }
        }
        return true;
    }

    public GameObject playerPrehub;
    public GameObject BoxPrehub;
    public GameObject goalPrehub;

    public GameObject ClearText;

    //�z��̐錾
    int[,] map;
    GameObject[,] field;

    // Start is called before the first frame update
    void Start()
    {
      

        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[,]
        {
            {0,0,0,0,0 },
            {0,0,1,0,0,},
            {0,3,2,3,0 },
            {0,2,3,2,0 },
            {0,0,0,0,0 }
        };

        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];
        

        for (int y = 0; y < map.GetLength(0); y++) 
        {
            for (int x = 0; x < map.GetLength(1); x++) 
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate
                       (
                            playerPrehub,
                            new Vector3(x, map.GetLength(0) - y, 0),
                            Quaternion.identity
                       );
                }

                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate
                     (
                          BoxPrehub,
                          new Vector3(x, map.GetLength(0) - y, 0),
                          Quaternion.identity
                     );
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //1�̉E�ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //�ʒu�擾
            Vector2Int playerIndex = GetPlayerIndex();
            //�����̈ړ�
            MoveNumber("player", playerIndex, playerIndex + new Vector2Int(1, 0));
            //�����N���A���Ă�����
            if (IsCleard())
            {
                Debug.Log("Clear!!");
                ClearText.SetActive(true);
            }
        }

        //1�̍��ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("player", playerIndex, playerIndex + new Vector2Int(-1, 0));
            //�����N���A���Ă�����
            if (IsCleard())
            {
                Debug.Log("Clear!!");
                ClearText.SetActive(true);
            }
        }

        //��̈ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //�ʒu�擾
            Vector2Int playerIndex = GetPlayerIndex();
            //�����̈ړ�
            MoveNumber("player", playerIndex, playerIndex + new Vector2Int(0,-1));
            //�����N���A���Ă�����
            if (IsCleard())
            {
                Debug.Log("Clear!!");
                ClearText.SetActive(true);
            }
        }

        //���̈ړ�(�v���C���[)
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //�ʒu�擾
            Vector2Int playerIndex = GetPlayerIndex();
            //�����̈ړ�
            MoveNumber("player", playerIndex, playerIndex + new Vector2Int(0, 1));
            //�����N���A���Ă�����
            if (IsCleard())
            {
                Debug.Log("Clear!!");
                ClearText.SetActive(true);
            }
        }

       

    }
}
