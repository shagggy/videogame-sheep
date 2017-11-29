using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Util
{
  public static List<GameObject>  GetAll (this GameObject obj)
  {
    List<GameObject> allChildren = new List<GameObject> ();
    GetChildren (obj, ref allChildren);
    return allChildren;
  }

//子要素を取得してリストに追加
  public static void GetChildren (GameObject obj, ref List<GameObject> allChildren)
  {
    Transform children = obj.GetComponentInChildren<Transform> ();
    //子要素がいなければ終了
    if (children.childCount == 0) {
      return;
    }
    foreach (Transform ob in children) {
      allChildren.Add (ob.gameObject);
      GetChildren (ob.gameObject, ref allChildren);
    }
  }

  public static List<int> GetShuffleNumLit(int maxNum, int number)
  {
    List<int> numList = new List<int>();
    List<int> returnNumList = new List<int>();

    for (int i = 0; i < maxNum; i++)
    {
      numList.Add(i);
    }

    int myNum;

    int n = numList.Count;
    for (int i = 0; i < n; i++)
    {
      int r = i + (int)(Random.Range(0, 1f) * (n - i));
      myNum = numList[r];
      numList[r] = numList[i];
      numList[i] = myNum;
    }

    for (int i = 0; i < number; i++)
    {
      returnNumList.Add(numList[i]);
    }

    return returnNumList;
  }
}