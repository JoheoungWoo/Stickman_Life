using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomain : MonoBehaviour
{
    public void goToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
