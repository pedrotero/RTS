using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Text tWinner;
    public Text tLoser;
    public float lerper;
    // Start is called before the first frame update
    void Start()
    {
        int win = PlayerPrefs.GetInt("Winner");
        if (win == 0)
        {
            tWinner.text = "Player " + 1 + " Wins!";
            tLoser.text = "Player " + 2 + " Lost :C";
            tLoser.color = Color.blue;
        }
        else if (win == 1)
        {
            tWinner.text = "Player " + 2 + " Wins!";
            tLoser.text = "Player " + 1 + " Lost :C";
            tLoser.color = Color.red;
        }
        
        
        tLoser.GetComponent<Rigidbody>().velocity = new Vector3(60,90);

    }

    // Update is called once per frame
    void Update()
    {
        lerper += 0.001f;
        float lerpinv = 1 - lerper;
        if (lerper > 1f) lerper = 0;
        tWinner.color = Color.HSVToRGB(lerper, 0.8f, 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Start");
    }
}
