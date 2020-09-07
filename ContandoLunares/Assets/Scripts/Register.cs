using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField inputUserName;
    public InputField inputPassword;
    public string Gender;
    public InputField inputAge;
    public InputField inputChangePass;



    //función para registrar un usuario
    
    public void RegisterUser()
    {
        //comprobar si el usuairo que intentamos crear ya existe
        if (File.Exists(Application.persistentDataPath + "/users/" + inputUserName.text))
        {

            //no se hace nada, se avisa de que ya existe

        }
        else
        {
            //comprobamos si se ha puesto usuario o contraseña
            if (inputUserName.text != "" || inputPassword.text != "")
            {
                //si no existe se guardan los datos del usuario

                User savedUser = new User
                {
                    userName = inputUserName.text,
                    password = inputPassword.text,
                    gender = Gender,
                    age = inputAge.text
                };
                //línea de guardado para los datos, se utiliza para guardar datos, archivo de guardado
                string saveLine = JsonUtility.ToJson(savedUser, true);
                File.Create(Application.persistentDataPath + "/users/" + inputUserName.text).Close();
                File.WriteAllText(Application.persistentDataPath + "/users/" + inputUserName.text, saveLine);
            }
            else
            {
                //No has puesto usuario o cotraseña
            }
        }


        inputUserName.text = "";
        inputPassword.text = "";
        Gender = "";
        inputAge.text = "";

        
    }
    
    public void ChangePass()
    {
        //comprobamos si se ha puesto contraseña
        if (inputChangePass.text != "")
        {
            //guardamos la contrseña

            User savedUser = new User
            {

                userName = GetComponent<gameController>().Name,
                password = inputChangePass.text,
                gender = GetComponent<gameController>().Gender,
                age = GetComponent<gameController>().Age



            };
            //línea de guardado para los datos, se utiliza para guardar datos, archivo de guardado
            string saveLine = JsonUtility.ToJson(savedUser, true);
            File.Create(Application.persistentDataPath + "/users/" + GetComponent<gameController>().Name).Close();
            File.WriteAllText(Application.persistentDataPath + "/users/" + GetComponent<gameController>().Name, saveLine);
        }
        else
        {
            //No has puesto usuario o cotraseña
        }

        inputChangePass.text = "";
    }

    public void DefineGender(string gender)
    {
        Gender = "M";
        Gender = gender;
    }

    private class User
    {

        public string userName;
        public string password;
        public string gender;
        public string age;

    }
}
