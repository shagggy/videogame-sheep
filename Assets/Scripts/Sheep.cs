using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sheep : MonoBehaviour
{
  private Rigidbody rb;
  private float timeToChangeDirection = 5.0f;
  private float resetTime;
  private Vector3 direction;
  private float speed = 1.5f;
  public string sheepType = "";
  private string oldType = "";
  private Color normal = new Color(236 / 255F, 236 / 255F, 236 / 255F, 1F);
  private Color blue = new Color(30 / 255F, 103 / 255F, 244 / 255F, 1F);
  private Color red = new Color(249 / 255F, 18 / 255F, 109 / 255F, 1F);
  private Color green = new Color(60 / 255F, 193 / 255F, 55 / 255F, 1F);
  private Color yellow = new Color(248 / 255F, 241 / 255F, 13 / 255F, 1F);
  private Dictionary<string, Color> colorDic = new Dictionary<string, Color>();

  // Use this for initialization
  private Renderer rd;
  private bool isStop = false;
  public bool isDestroyable = false;

  private Color alpha = new Color(0, 0, 0, 0.05f);
  private bool isLeaving = false;
  public void Awake()
  {
    colorDic.Add("normal", normal);
    colorDic.Add("blue", blue);
    colorDic.Add("red", red);
    colorDic.Add("green", green);
    colorDic.Add("yellow", yellow);    
  }
  public void Start()
  {
    ChangeDirection();
    rb = GetComponent<Rigidbody>();
    
    rd = transform.Find("Plane").gameObject.GetComponent<Renderer>();

    int i = Random.Range(0, 5);
    if (i == 0)
    {
      setSheepType("blue");
    }
    else if (i == 1)
    {
      setSheepType("red");
    }
    else if (i == 2)
    {
      setSheepType("green");
    }
    else if (i == 3)
    {
      setSheepType("yellow");
    }
    else
    {
      //Debug.Log("normal!");
      setSheepType("normal");
    }

  }
  // Update is called once per frame
  public void Update()
  {
    if(oldType != sheepType){
      //Debug.Log(oldType+" "+sheepType);
      ChangeColor(colorDic[sheepType]);
    }
    if((transform.position.y > 5 || transform.position.y < -2)&& isDestroyable){
      Destroy(gameObject);
    }
    if(!isStop){
      resetTime -= Time.deltaTime;
      if (resetTime <= 0)
      {
        ChangeDirection();
      }
      //Debug.Log(transform.forward);
      //rb.velocity = transform.forward * 2;
      //Debug.Log(direction);
      transform.position = (transform.position + transform.forward * Time.deltaTime * speed);
      transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime);
    }
    oldType = sheepType;

    if(isLeaving){
      
      if (rd.material.color.a >= 0)
        rd.material.color -= alpha;
      transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
  }
  public void setDestroyable(bool d){
    isDestroyable = d;
  }
  public bool getDestroyable(){
    return isDestroyable;
  }
  public void setSheepType(string type){
    sheepType = type;
  }

  public string getSheepType()
  {
    return sheepType;
  }
  public bool getLeaving()
  {
    return isLeaving;
  }

  private void ChangeColor(Color color)
  {
    Material[] mats = rd.materials;
    //Debug.Log(mats[0].color);
    mats[0].color = color;
    rd.materials = mats;
  }
  private void ChangeDirection()
  {
    float angle = Random.Range(0f, 360f);
    Quaternion quat = Quaternion.AngleAxis(angle, Vector3.up);
    direction = quat * Vector3.forward;
    direction.y = 0;
    direction.Normalize();
    //Debug.Log(rb.velocity);
    //transform.forward = newForward;
    resetTime = timeToChangeDirection;
  }

  public void Leave(){
    stop();
    rb.isKinematic = true;
    rb.velocity = Vector3.zero;
    rb.isKinematic = false;
    rb.useGravity = false;
    rb.AddForce(new Vector3(0, 4, 0), ForceMode.VelocityChange);
    isLeaving = true;
    AddScore(1);
  }
  private void AddScore(int s){
    Data.Score += s;
  }
  public void stop(){
    isStop = true;
  }
  public void play()
  {
    isStop = false;
  }
}