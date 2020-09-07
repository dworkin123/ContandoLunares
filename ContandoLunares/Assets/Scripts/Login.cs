using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField inputUserName;
    public InputField inputPassword;
    public GameObject HUDLogin, HUDMenu;


    //función para registrar un usuario
    public void LoginUser()
    {
        //comprobar si el usuairo que intentamos crear ya existe
        if (File.Exists(Application.persistentDataPath + "/users/" + inputUserName.text))
        {
            string chargeLine = File.ReadAllText(Application.persistentDataPath + "/users/" + inputUserName.text);
            //no se hace nada
            User loadedUser = JsonUtility.FromJson<User>(chargeLine);

            if (inputPassword.text == loadedUser.password)
            {
                //Pasamos a lo siguiente
                HUDLogin.SetActive(false);
                HUDMenu.SetActive(true);
                GetComponent<gameController>().Name = loadedUser.userName;
                GetComponent<gameController>().Gender = loadedUser.gender;
                GetComponent<gameController>().Age = loadedUser.age;
                //si la carpeta no existe, te la crea con el nombre de usuario que se ha logeado
                if (!Directory.Exists(Application.persistentDataPath + "/images/" + loadedUser.userName))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/images/" + loadedUser.userName);
                }
            }
        }
        else
        {
           //Sale mensajito de que no existe
        }

        inputUserName.text = "";
        inputPassword.text = "";


    }
    private class User
    {

        public string userName;
        public string password;
        public string gender;
        public string age;

    }
}
