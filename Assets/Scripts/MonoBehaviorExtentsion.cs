using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class MonoBehaviorExtentsion
{

  public static IEnumerator DelayMethod<T>(this MonoBehaviour mono, float waitTime, Action<T> action, T t)
  {
    yield return new WaitForSeconds(waitTime);
    action(t);
  }

  public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime, Action action)
  {
    yield return new WaitForSeconds(waitTime);
    action();
  }

  //TimeScaleに関わらず、指定の秒数まつ
  public static IEnumerator WaitForSecondsIgnoreTimeScale(this MonoBehaviour mono, float waitTime, Action action)
  {
    float targetTime = Time.realtimeSinceStartup + waitTime;
    while (Time.realtimeSinceStartup < targetTime)
    {
      yield return new WaitForEndOfFrame();
    }
    action();
  }

  public static Coroutine Delay<T>(this MonoBehaviour mono, float waitTime, Action<T> action, T t)
  {
    return mono.StartCoroutine(DelayMethod(mono, waitTime, action, t));
  }

  public static Coroutine Delay(this MonoBehaviour mono, float waitTime, Action action)
  {
    return mono.StartCoroutine(DelayMethod(mono, waitTime, action));
  }

  public static Coroutine DelayIgnoreScale(this MonoBehaviour mono, float waitTime, Action action){
    return mono.StartCoroutine(WaitForSecondsIgnoreTimeScale(mono,waitTime,action));
  }



}