using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour {

  public void LoadStart()
  {
    SceneManager.LoadScene("title");
    ProcessTimer.Stop();
    ProcessTimer.Reset();
    Data.Time = 30;
    Data.Score = 0;
    Time.timeScale = 1;
  }
}
