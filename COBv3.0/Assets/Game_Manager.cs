using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    static public GameObject[][] Avatar = { new GameObject[] {null,null}, new GameObject[] {null, null} };
    static public GameObject CubeIdle;
    static private int QtdUsers;
    void Start()
    {
        QtdUsers = 0;
    }
    static public void AvatarAddicioned(int indexUser)
    {
        Debug.Log("Avatar Adicionado: " + indexUser);
        QtdUsers--;
        if (indexUser == 0 /*&& QtdUsers == 0*/)
        {
            CubeIdle.GetComponent<IdleForm>().activate = true;
        }
    }
    static public void AvatarRemovement(int indexUser)
    {
        Debug.Log("Avatar Removido: " + indexUser);
        QtdUsers++;
        if (indexUser == 0 /*&& QtdUsers == 1*/)
        {
            CubeIdle.GetComponent<IdleForm>().activate = false;
        }
        //Avatar[indexUser][1].SetActive(false);
        //Avatar[indexUser][0].SetActive(false);
    }

}
