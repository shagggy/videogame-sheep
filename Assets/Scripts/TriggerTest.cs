﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }
  
  // Update is called once per frame
  void Update () {
  
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("enter");
  }
  //private void OnTriggerStay(Collider other)
  //{
  //  Debug.Log("stay");
  //}
}
