using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
  private string oldtype = "normal";
  private string type = "normal";

  private Color white;
  private Color blue;
  private Color red;
  private Color green;
  private Color yellow;

  private Renderer rend;

  private Dictionary<string, Color> colorDic = new Dictionary<string, Color>();

  private ParticleSystem particle;
  private ParticleSystem.MainModule particleMain;

  private Collider[] Cols;

  void Awake () {
    white = new Color(200 / 255F, 200 / 255F, 200 / 255F, 1F);
    blue = new Color(30 / 255F, 103 / 255F, 244 / 255F, 1F);
    red = new Color(249 / 255F, 18 / 255F, 109 / 255F, 1F);
    green = new Color(60 / 255F, 193 / 255F, 55 / 255F, 1F);
    yellow = new Color(248 / 255F, 241 / 255F, 13 / 255F, 1F);

    colorDic.Add("normal", white);
    colorDic.Add("blue", blue);
    colorDic.Add("red", red);
    colorDic.Add("green", green);
    colorDic.Add("yellow", yellow);
  }

  // Use this for initialization
  void Start () {
    Cols = GetComponents<Collider>();
    Cols[0].isTrigger = false;
    rend = GetComponent<Renderer>();
    //Debug.Log("floor-start");
    particle = GetComponent<ParticleSystem>();
    particleMain = particle.main;
    particle.Stop();
    ChangeParticleColor(type);
    ChangeColor(colorDic[type]);
    //InvokeRepeating("changeFloorType", 10f, 10f);

    if (Time.time > 4f)
    {
      GetComponentInChildren<BoxCollider>().isTrigger = true;
    }
	}
	
	// Update is called once per frame
	void Update () {
    if(type != oldtype){
      ChangeColor(colorDic[type]);
      ChangeParticleColor(type);
    }
    oldtype = type;
    if(Cols[0].isTrigger){
      Cols[0].isTrigger = false;
    }
	}

  public void setFloorType(string t){
    type = t;
  }

  private void ChangeParticleColor(string type)
  {
    if (type != "normal")
    {
      particle.Play();
      particleMain.startColor = colorDic[type];
    }else{
      particle.Stop();
    }
  }

  private void ChangeColor(Color color)
  {
    //Debug.Log(rend);
    rend.material.color = color;
    rend.material.SetColor("_EmissionColor",color);
  }

  void OnTriggerStay(Collider enterObj)
  {
    if (enterObj.transform.GetComponent<Sheep>() != null)
    {
      if (type != "normal")
      {
        string sheepType = enterObj.transform.GetComponent<Sheep>().getSheepType();
        //Debug.Log("enter!------" + sheepType + type);

        bool sheepIsDestroyable = enterObj.transform.GetComponent<Sheep>().getDestroyable();
        bool sheepIsLeaving = enterObj.transform.GetComponent<Sheep>().getLeaving();
        if (sheepType == type && sheepType != "normal" && sheepIsDestroyable && enterObj.gameObject.layer != 9 && !sheepIsLeaving)
        {
          //Debug.Log("enter!------" + sheepType + type);
          //Debug.Log(enterObj.transform.GetComponent<Rigidbody>().velocity);
          enterObj.transform.GetComponent<Sheep>().Leave();

          //enterObj.transform.GetComponent<ParticleSystem>().Play();
          //Destroy(enterObj.gameObject);
        }
      }
    }
    
  }

}