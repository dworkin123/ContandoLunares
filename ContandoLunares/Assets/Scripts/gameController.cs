using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class gameController : MonoBehaviour
{

    public string Name;
    public string Gender;
    public string Age;
    public GameObject HUDGaleria, HUDCamara, HUDPerfil, HudLogin, HUDFolders;
    public GameObject CuerpoHombre, CuerpoMujer,Cube;
    public Text TextGender, TextName, TextAge;
    public bool ModelIsFolders;
    public string foldertype;
    string[] FilesCount = new string[0];
    public GameObject ImageFolder;
    public bool BodyOrFolder;
    public bool isCameraActive;
    public string bodypart;
    public Sprite imageBody, imageFolder;
    public Image folderImage1;



    // Start is called before the first frame update
    void Start()
    {
        isCameraActive = false;
        BodyOrFolder = true;
        HudLogin.SetActive(true);
        //comprobar que las carpetas creadas existan
        if (!Directory.Exists(Application.persistentDataPath + "/users/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/users/");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/images/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/images/");
        }
    }
    //Cargar Perfil
    public void ChargeProfile()
    {
        if (!HUDPerfil.activeSelf)
        {
            TextName.text = Name;
            TextGender.text = Gender;
            TextAge.text = Age;

            HUDPerfil.SetActive(true);
            HUDCamara.SetActive(false);
            HUDGaleria.SetActive(false);
            isCameraActive = false;
            DisableBody();

        }
        else
        {
            HUDPerfil.SetActive(false);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    print(hit.collider.name);
                    if (hit.collider.name != "Cube")
                    {
                    BodyButtons(hit.collider.name);
                    }
                }
            }


        }
        
    }

    //Activar Galeria
    public void EnableFolders()
    {
        if (!HUDGaleria.activeSelf)
        {
            ModelIsFolders = true;
            EnableBody();
            HUDGaleria.SetActive(true);
            HUDCamara.SetActive(false);
            HUDPerfil.SetActive(false);
            isCameraActive = false;
            
            
        }
        else
        {
            HUDGaleria.SetActive(false);
            DisableBody();
            ModelIsFolders = false;
        }
    }

    //Activar Camara
    public void EnableCamera()
    {
        if (!HUDCamara.activeSelf)
        {
            ModelIsFolders = false;
           // HUDCamara.SetActive(true);
            HUDGaleria.SetActive(false);
            HUDPerfil.SetActive(false);
            EnableBody();
            
        }
        else
        {
            HUDCamara.SetActive(false);
            DisableBody();
            isCameraActive = false;
        }
    }


    public void BodyButtons(string _name)
    {
        if (ModelIsFolders == true)
        {
            Debug.Log("Uso carpeta");
            //Muestra las carpetas
            BotonController(_name);
            isCameraActive = false;
        }
        else
        {
            bodypart = _name;
            if (!Directory.Exists(Application.persistentDataPath + "/images/" + Name + "/" + bodypart))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/images/" + Name + "/" + bodypart);
            }
            Debug.Log("Uso camara");
            HUDCamara.SetActive(true);

            //aqui es donde definimos la parte del cuerpo con _name
            HUDCamara.GetComponent<Camerascript>().enableCamera();
            isCameraActive = true;
            DisableBody();

            
        }

    }

    //Habilitar Cuerpo
    public void EnableBody()
    {
        HUDFolders.SetActive(false);
        Cube.SetActive(true);
        if (Gender == "F")
        {
            CuerpoMujer.SetActive(true);
        }
        else
        {
            CuerpoHombre.SetActive(true);
        }
    }
    //Deshabilitar cuerpo
    public void DisableBody()
    {
        Cube.SetActive(false);
        CuerpoHombre.SetActive(false);
        CuerpoMujer.SetActive(false);
    }
    public void BotonController(string _foldertype)
    {
        foldertype = _foldertype;
        ColocarEnHUD();
    }
    //EliminarHud
    public void ColocarEnHUD()
    {
        if (foldertype != null)
        {
            //leo cuantos personajes hay
            FilesCount = new string[0];
            if (foldertype == "Folders")
            {
                if (BodyOrFolder == true)
                {
                    //Es Folders
                    HUDFolders.SetActive(true);
                    DisableBody();
                    FilesCount = Directory.GetDirectories(Application.persistentDataPath + "/images/" + Name);
                    BodyOrFolder = false;
                    //Cambiamos imagen a cuerpo
                    folderImage1.sprite = imageFolder;
                }
                else
                {
                    HUDFolders.SetActive(false);
                    //Es Body
                    EnableBody();
                    BodyOrFolder = true;
                    //Cambiamos imagen a folder
                    folderImage1.sprite = imageBody;
                }
            }
           
            else
            {
                print(Application.persistentDataPath + "/images/" + Name + "/" + foldertype);
                DisableBody();
                HUDFolders.SetActive(true);
                if (!Directory.Exists(Application.persistentDataPath + "/images/" + Name + "/" + foldertype))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/images/" + Name + "/" + foldertype);
                }
                FilesCount = Directory.GetFiles(Application.persistentDataPath + "/images/" + Name + "/" + foldertype);
            }

            //Quitar los que hay
            DeleteHud();
            //print(FilesCount[0]);

            //por cada uno hago
            
            for (int i = 0; i < FilesCount.Length; i++)
            {
                //Creo un nombre temporal, nombre del personaje
                string TempName = FilesCount[i];//TodaLaURL
                                               //Divido la url por /
                string[] SplitURL = TempName.Split(new string[] { "/" }, System.StringSplitOptions.None);
                for (int o = 0; o < SplitURL.Length; o++)
                {
                    print(SplitURL[o]);
                }
                //Divido el final de la url por \
                //   string[] GetNameFormat = SplitURL[SplitURL.Length - 1].Split(new string[] { "/" }, System.StringSplitOptions.None);

                //Divido el final de el final de la url por .
                //  string[] GetName = GetNameFormat[1].Split(new string[] { "." }, System.StringSplitOptions.None);
                //print("Nombre del personaje: " + GetName[0]);
                string[] GetName = SplitURL[SplitURL.Length-1].Split(new string[] { "." }, System.StringSplitOptions.None);
                if (foldertype=="Folders")
                {

                    print(GetName[0]);
                     //instancio el prefab boton en el grid
                     GameObject NewBtn = Instantiate(ImageFolder, HUDFolders.transform);
                     //Cargo la imagen

                     //Aqui va codigo de cargar desde resources la imagen

                     //añado al boton este onclick
                     NewBtn.name = GetName[0];
                     NewBtn.transform.GetChild(0).GetComponent<Text>().text = NewBtn.name;
                     //Le añadimos la funcion al boton de cargar sus carpetas
                     NewBtn.GetComponent<Button>().onClick.AddListener(delegate { BotonController(GetName[0]); });
                }
                else if (GetName[1] == "png")
                {
                    print(GetName[0]+"."+GetName[1]);
                     //instancio el prefab boton en el grid
                     GameObject NewBtn = Instantiate(ImageFolder, HUDFolders.transform);
                     //Cargo la imagen
                     LoadImage(NewBtn.GetComponent<Image>(), i);
                     //añado al boton este onclick
                     NewBtn.name = GetName[0];
                     //Le añadimos la funcion al boton de cargar sus carpetas
                     NewBtn.GetComponent<Button>().onClick.AddListener(delegate { showImage(NewBtn.GetComponent<Image>()); });

                }
                

            }
        }
    }
    public void showImage (Image image)
    {
        HUDCamara.SetActive(true);
       // HUDGaleria.SetActive(false);
        HUDCamara.GetComponent<RawImage>().texture = image.mainTexture;
        DisableBody();
       // DeleteHud();

    }
    //Elimina todas las imagenes para recargarlas
    public void DeleteHud()
    {
        for (int i = 0; i < HUDFolders.transform.childCount; i++)
        {
            Destroy(HUDFolders.transform.GetChild(i).gameObject);
        }
    }
    

    public void LoadImage(Image _rend, int _index)
    {

        Texture2D tex = null;
        byte[] fileData;
        string PcPath = FilesCount[_index];

        if (File.Exists(PcPath))
        {

            fileData = File.ReadAllBytes(PcPath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            if (_rend != null)
            {
                _rend.sprite = Sprite.Create(tex,
                    new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }

    }



    //Crea las carpetas
    public void SaveInFolder(string foldername)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/images/" + Name+"/"+ foldername))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/images/" + Name + "/" + foldername);
        }

    }


}
