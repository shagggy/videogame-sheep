using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMainSceneScript : MonoBehaviour
{

  public void ToMain()
  {
    SceneManager.LoadScene("main");
  }
}