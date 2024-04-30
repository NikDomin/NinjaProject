using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class Init : MonoBehaviour
    {
        [SerializeField] private DataPersistenceManager manager;
        private void OnEnable()
        {
            manager.OnLoadEndSuccefully += GoToMainMenu;
            manager.LoadGame();
        }
        private void OnDisable()
        {
            manager.OnLoadEndSuccefully -= GoToMainMenu;

        }
        
        private void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }


    }
}