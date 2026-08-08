using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    DatabaseReference dbReference;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbReference = dbReference = FirebaseDatabase.GetInstance("https://adhdgameapp-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
                Debug.Log("Firebase Ready");

            }
            else
            {
                Debug.LogError("Firebase not ready");
            }
        });
    }

    public void SaveTest()
{
    string json = "{\"test\":\"hello\"}";

    dbReference.Child("test").SetRawJsonValueAsync(json);

    Debug.Log("Test Data Sent");
}

    public void SaveData(PlayerData data)
{
    string json = JsonUtility.ToJson(data, true);

    string uniqueId = data.playerName + "_" + System.DateTime.Now.Ticks;

    dbReference.Child("players")
               .Child(uniqueId)
               .SetRawJsonValueAsync(json);

    Debug.Log("Real Data Saved");
}
}