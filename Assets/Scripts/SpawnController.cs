using UnityEngine;
using System.Collections;

//指定したゲームオブジェクトを無限スポーンさせるスクリプト
public class SpawnController : MonoBehaviour
{

  //発生するオブジェクトをInspectorから指定する用
  public GameObject spawnObject;
  //発生間隔用
  public float interval = 3.0f;


  void Start()
  {
    //コルーチンの開始
    StartCoroutine("Spawn");
    InvokeRepeating("randMove", 1f, 1f);
  }

  private void randMove(){
    transform.position = new Vector3(Random.Range(2.5f, 17.5f), 4, Random.Range(-2.5f, 12.5f));
  }

  IEnumerator Spawn()
  {
    //無限ループの開始
    while (true)
    {
      //自分をつけたオブジェクトの位置に、発生するオブジェクトをインスタンス化して生成する
      GameObject target = Instantiate(spawnObject, transform.position, Quaternion.identity) as GameObject;

      // DelayMethodを3.5秒後に呼び出す
      StartCoroutine(DelayMethod(1.5f, target));
      yield return new WaitForSeconds(interval);
    }
  }

  IEnumerator DelayMethod(float delay, GameObject target)
  {
    //delay秒待つ
    yield return new WaitForSeconds(delay);
    /*処理*/
    target.GetComponent<Sheep>().setDestroyable(true);
  }

  void DelayMethod()
  {
    Debug.Log("Delay call");
  }

}