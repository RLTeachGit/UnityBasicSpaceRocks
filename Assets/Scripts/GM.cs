using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {


    //Regions allow code to be folded
    #region GlobalVariables

    [SerializeField]
    private GameObject BulletPrefab;  //Link in IDE

    [SerializeField]
    private GameObject ShipPrefab;  //Link in IDE

    [SerializeField]
    private GameObject[] RockPrefabs;   //Link in IDE

    private int mScore;     //This Variable is private to GM
    public static int   Score { //Make it availiable via a Getter
            get {
            return sGM.mScore;
        }
    }
    #endregion


    //Creating a singleton Design Pattern
    //A Singleton can be accesses anweher and its the same object for all
    //No just an instance (copy)
    //See http://csharpindepth.com/Articles/General/Singleton.aspx
    public static GM sGM = null;     //The static reference to the Singleton, its null anyway, but good reminder

    //We want to be up and running right away, before other stuff starts
    void Awake() {
        if (sGM == null) {     //If we are doing this for the first time ONLY
            sGM = this;     //Now the static variable is reference to our instance
            DontDestroyOnLoad(sGM.gameObject);  //This means it survives a scene change
            InitialiseGame();
        } else if (sGM != this) {  //If we get called again and are not the same as before, kill duplicate
            Destroy(gameObject);
        }
    }


    #region
    void    InitialiseGame() {
        CreatePlayerShip();
        for(int tRockIndex=0;tRockIndex<5;tRockIndex++) {
            CreateRock(0);
        }
    }
    #endregion

    #region PlayerControl
    public  static   void CreatePlayerShip() {
        Debug.Assert(sGM.ShipPrefab != null, "ShipPrefab prefab not linked in IDE");
        Instantiate(sGM.ShipPrefab, Vector3.zero, Quaternion.identity);
    }
    #endregion

    #region RockControl
    public  static  void    CreateRock(int vIndex) {
        Debug.Assert(sGM.RockPrefabs != null, "RockPrefabs not linked in IDE");
        if(vIndex<sGM.RockPrefabs.Length) {
            Instantiate(sGM.RockPrefabs[vIndex], RandomScreenPositition, Quaternion.identity);
        } else {
            Debug.LogFormat("RockPrefab Index {0} out of range",vIndex);
        }
    }
    #endregion

    public  static  void    CreateBullet(Vector3 vPosition, Vector3 vDirection) {
        Debug.Assert(sGM.BulletPrefab != null, "BulletPrefab not linked in IDE");
        Bullet tBullet=Instantiate(sGM.BulletPrefab, vPosition, Quaternion.identity).GetComponent<Bullet>();
        Debug.Assert(tBullet != null, "Bullet Script missing");
        tBullet.FireBullet(vPosition, vDirection);
    }


    #region Utilities
    public  static  Vector3 RandomScreenPositition {
        get {
            float tHeight = Camera.main.orthographicSize;  //Figure out what Camera can see
            float tWidth = Camera.main.aspect * tHeight;  //Use aspect ratio to work out Width
            return new Vector3(Random.Range(-tWidth, tWidth), Random.Range(-tHeight, tHeight),0.0f);
        }
    }
    #endregion

}


