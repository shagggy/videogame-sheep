using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
  public Text ScoreText;
  public Text TimeText;
  public Image Result;
  private int DefaultTimer = 60;
  private bool isPlaying = false;

  // Use this for initialization
  void Start () {
    isPlaying = true;
    Data.Time = DefaultTimer;
    ProcessTimer.Restart();

    Cursor.lockState = CursorLockMode.Confined; //はみ出さないモード
    //Cursor.visible = false; //OSカーソル非表示
  
  }

  // Update is called once per frame
  void Update () {
    if (isPlaying) {
      Data.Time = DefaultTimer - (int)ProcessTimer.TotalSeconds;
      ScoreText.text = Data.Score.ToString()+" SHEEP";
      TimeText.text = Data.Time.ToString();
    }


    if(Data.Time <= 0){
      isPlaying = false;
      Result.GetComponent<Result>().OpenResult();
    }
  }
}
