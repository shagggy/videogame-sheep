using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {
  //private List<string> floorTypeList = new List<string>();
  private List<Floor> floorList = new List<Floor>();
  public GameObject floor;
	// Use this for initialization
	void Start () {
    for (int i = 0; i < 16; i++)
    {
      Vector3 pos = new Vector3((i % 4) * 5, -2, (i / 4) * 5);
      var f = Instantiate(floor, pos, Quaternion.identity) as GameObject;

      f.transform.parent = transform;
    }
    foreach (Transform t in transform)
    {
      floorList.Add(t.gameObject.GetComponent<Floor>());
    }
    //InvokeRepeating("changeFloorType", 2f, 2f);

    StartCoroutine("RepeatMethod", 8f);

    //int[] flArr = new int[]
    //{
    //  1,2,3,4,
    //  1,2,3,4,
    //  1,2,3,4,
    //  1,2,3,4
    //};

    //ChangeFloorTypeByIntArr(flArr);
  }

  private IEnumerator RepeatMethod(float f){
    while(true){
      randChangeFloorType(Random.Range(4, 9));
      yield return new WaitForSeconds(f);
    }
  }

  private void ChangeFloorTypeByIntArr(int[] flArr){
    for (int i = 0; i < 16; i++)
    {
      int num = flArr[i];
      string type = "";
      if (num == 0)
      {
        type = "normal";
      }
      else if (num == 1)
      {
        type = "blue";
      }
      else if (num == 2)
      {
        type = "red";
      }
      else if (num == 3)
      {
        type = "green";
      }
      else if (num == 4)
      {
        type = "yellow";
      }
      floorList[i].setFloorType(type);
    }
  }

  private void randChangeFloorType(int num){

    List<int> shuffleNumList = Util.GetShuffleNumLit(16,num);
    for (int i = 0; i < 16; i++)
    {
      floorList[i].setFloorType("normal");
    }

    for (int j = 0; j < shuffleNumList.Count; j++)
    {
      int r = Random.Range(0, 4);
      string type = "";
      if (r == 0)
      {
          type = "blue";
      }
      else if (r == 1)
      {
          type = "red";
      }
      else if (r == 2)
      {
          type = "green";
      }
      else if (r == 3)
      {
          type = "yellow";
      }
      floorList[shuffleNumList[j]].setFloorType(type);
    }

  }

  private void changeFloorColor(){
      
  }


  // Update is called once per frame
  void Update () {
		
	}
}
