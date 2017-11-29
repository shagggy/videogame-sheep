using UnityEngine;

// プレイヤー
public class Player : MonoBehaviour
{
  [SerializeField]
  private Vector3 velocity;              // 移動方向
  [SerializeField]
  private float moveSpeed = 8.0f;        // 移動速度
  [SerializeField]
  private float applySpeed = 2.2f;       // 回転の適用速度
  [SerializeField]
  private PlayerFollowCamera refCamera = null;  // カメラの水平回転を参照する用

  private bool isHold = false;
  private bool isThrowing = false;
  private GameObject holdObj;
  private Collider holdableObj;
  void Update()
  {
    
    // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
    velocity = Vector3.zero;
    if (Input.GetKey(KeyCode.W))
      velocity.z += 1;
    if (Input.GetKey(KeyCode.A))
      velocity.x -= 1;
    if (Input.GetKey(KeyCode.S))
      velocity.z -= 1;
    if (Input.GetKey(KeyCode.D))
      velocity.x += 1;

    // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
    velocity = velocity.normalized * moveSpeed * Time.deltaTime;

    // いずれかの方向に移動している場合
    if (velocity.magnitude > 0)
    {
      // 以下はカメラとは無関係の場合の移動----------------------------------------

      // プレイヤーの回転(transform.rotation)の更新
      // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の方向(velocity)に回す回転とします(瞬時)
      // transform.rotation = Quaternion.LookRotation(velocity);

      // プレイヤーの回転(transform.rotation)の更新
      // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の方向(velocity)に回す回転に段々近づけます
      // transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(velocity),applySpeed);

      // プレイヤーの位置(transform.position)の更新
      // 移動方向ベクトル(velocity)を足し込みます
      // transform.position += velocity;

      // ------------------------------------------------------------------------

      // 以下はカメラの向きへ移動

      // プレイヤーの回転(transform.rotation)の更新
      // 無回転状態のプレイヤーのZ+方向(後頭部)を、
      // カメラの水平回転(refCamera.hRotation)で回した移動の方向(velocity)に回す回転に段々近づけます
      transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(refCamera.hRotation * velocity),
        applySpeed
      );

      // プレイヤーの位置(transform.position)の更新
      // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
      transform.position += refCamera.hRotation * velocity;
    }


    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      if (isHold)
      {
        holdObj.transform.parent = null;
        holdObj.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        holdObj.GetComponent<Collider>().attachedRigidbody.isKinematic = false;
        holdObj.GetComponent<Rigidbody>().AddForce(transform.forward * 12 + new Vector3(0, 3, 0), ForceMode.VelocityChange);
        holdObj.layer = 11;
        // 型チェックか、CharacterClassを作るか
        holdObj.GetComponent<Sheep>().play();
        holdObj = null;
        isHold = false;
        isThrowing = true;
        this.Delay(1.0f, setIsThorwing, false);
      }
    }
    if (Input.GetKeyDown(KeyCode.Mouse1)){
      if (isHold)
      {
        holdObj.transform.parent = null;
        holdObj.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        holdObj.GetComponent<Collider>().attachedRigidbody.isKinematic = false;
        holdObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, 0), ForceMode.VelocityChange);
        holdObj.layer = 11;
        // 型チェックか、CharacterClassを作るか
        holdObj.GetComponent<Sheep>().play();
        holdObj = null;
        isHold = false;
        isThrowing = true;
        this.Delay(1.0f, setIsThorwing, false);
      }
    }
    if (Input.GetKeyDown(KeyCode.Mouse0) && holdableObj && !isThrowing)
    {
      if (!isHold)
      {
        holdObj = holdableObj.transform.gameObject;
        Debug.Log(holdableObj.transform);
        isHold = true;
        holdableObj.transform.parent = transform;
        holdableObj.attachedRigidbody.useGravity = false;
        holdableObj.attachedRigidbody.isKinematic = true;
        holdableObj.transform.localPosition = new Vector3(0f, 0.5f, 0.4f);
        holdableObj.transform.rotation = Quaternion.identity;
        holdObj.layer = 9;
        // 型チェックか、CharacterClassを作るか
        holdObj.GetComponent<Sheep>().stop();
      }
    }

   
  //if (Input.GetKeyDown(KeyCode.F))
  //{
  //  if (!isHold)
  //  {
  //    Ray holdRay = new Ray(transform.position, transform.forward);
  //    RaycastHit hit;
  //    float distance = 100; // 飛ばす&表示するRayの長さ
  //    float duration = 3;   // 表示期間（秒）
  //    Debug.DrawRay(holdRay.origin, holdRay.direction * distance, Color.red, duration, false);
  //    if (Physics.Raycast(holdRay, out hit, 3f))
  //    {
  //      if (hit.collider.tag == "Sheep")
  //      {
  //        isHold = true;
  //        hit.transform.parent = transform;
  //        hit.collider.attachedRigidbody.useGravity = false;
  //        hit.collider.attachedRigidbody.isKinematic = true;
  //        hit.transform.localPosition = new Vector3(0f, 0.5f, 0.4f);
  //        hit.transform.rotation = Quaternion.identity;
  //        holdObj = hit.transform.gameObject;
  //        holdObj.layer = 9;
  //        // 型チェックか、CharacterClassを作るか
  //        holdObj.GetComponent<Sheep>().stop();
  //      }
  //    }
  //  }
  //  else
  //  {
  //    holdObj.transform.parent = null;
  //    holdObj.GetComponent<Collider>().attachedRigidbody.useGravity = true;
  //    holdObj.GetComponent<Collider>().attachedRigidbody.isKinematic = false;
  //    holdObj.GetComponent<Rigidbody>().AddForce(transform.forward * 12 + new Vector3(0, 3, 0), ForceMode.VelocityChange);
  //    holdObj.layer = 11;
  //    // 型チェックか、CharacterClassを作るか
  //    holdObj.GetComponent<Sheep>().play();
  //    holdObj = null;
  //    isHold = false;
  //  }
  //}
  }

  private void setIsThorwing(bool b){
    isThrowing = b;
  }
  private void FixedUpdate()
  {
    holdableObj = null;
  }
  private void OnTriggerStay(Collider other)
  {
    if (other.tag == "Sheep" && !other.gameObject.GetComponent<Sheep>().getLeaving())
    {
      holdableObj = other;
    }
  }
}