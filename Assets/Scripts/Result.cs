using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour{
  public Text ResultText;

  private void Start()
  {
    this.gameObject.SetActive(false);
  }

  private void Update()
  {
    
  }

  public void OpenResult(){
    this.gameObject.SetActive(true);
    Time.timeScale = 0;
    ResultText.text = Data.Score+"頭の羊が召され、私は眠りについた。";

  }
}
