using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartEngine : MonoBehaviour
{
    public Image TankImage;
    public Text OnePlayer;
    public Text TwoPlayers;
    public Text Construction;
    private int selection = 1;
    void Start()
    {
        TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, OnePlayer.rectTransform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selection -= 1;
            if (selection == 0)
                selection = 3;
            PlaceSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selection += 1;
            if (selection == 4)
                selection = 1;
            PlaceSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
            LoadStage();
    }

    private void LoadStage()
    {
        if (selection == 1)
            SceneManager.LoadGameScene();
    }

    private void PlaceSelection()
    {
        if (selection == 1)
            TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, OnePlayer.rectTransform.position.y, 0);
        else if (selection == 2)
            TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, TwoPlayers.rectTransform.position.y, 0);
        else if (selection == 3)
            TankImage.rectTransform.position = new Vector3(TankImage.rectTransform.position.x, Construction.rectTransform.position.y, 0);
    }
}
