using UnityEngine;
using UnityEngine.UI;
using GameJolt.UI;
using GameJolt.API;
using GameJolt.API.Objects;
using System.Collections.Generic;

public enum TrophiesID
{
    BuyTower = 107043,
    UpgradeTower = 107040,
    SellItem = 107041,
    BuyBarrier = 107044,
    KillWorker = 107032,
    KillSoldier = 107039,
    KillFlyer = 107042,
    KillQueen = 107038,
    Level1 = 107033,
    Level2 = 107034,
    Level3 = 107035,
    Level4 = 107036,
    Level5 = 107037,
    PlatinumAllTrophies = 107029,
}

public enum ScoreboardID
{
    Level1 = 421005,
    Level2 = 421006,
    Level3 = 421007,
    Level4 = 421008,
    Level5 = 421009,

}

public class GamejoltManager : MonoBehaviour
{
    #region Singleton
    private static GamejoltManager _instance = null;

    public static GamejoltManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<GamejoltManager>();
        }
        return _instance;
    }
    #endregion

    public delegate void MyDel();

    [System.Serializable]
    public class TheTrophy
    {
        [SerializeField] public string name;
        [SerializeField] public int id;
        [SerializeField] public bool unlocked;

        public TheTrophy(string name, int id, bool unlocked)
        {
            this.name = name;
            this.id = id;
            this.unlocked = unlocked;
        }
    }


    bool loggedOn = false;
    [SerializeField] private Button logInButton;
    [SerializeField] private Button logOutButton;

    [SerializeField] private List<TheTrophy> userTrophies;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        logInButton.gameObject.SetActive(false);
        logOutButton.gameObject.SetActive(false); 
#endif
        SetLogStatus();
    }

    public void ShowLogin()
    {
        GameJoltUI.Instance.ShowSignIn(x => CheckLogSuccess(x));
    }

    public void ShowLogin(MyDel myFunction)
    {
        GameJoltUI.Instance.ShowSignIn(x => CheckLogSuccess(x, myFunction));
    }

    public void LogOut()
    {
        if (loggedOn)
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
            SetLogStatus();
        }
    }

    public void ShowScore()
    {
        if (loggedOn)
        {
            GameJoltUI.Instance.ShowLeaderboards();
        }
        else
        {
            ShowLogin(GameJoltUI.Instance.ShowLeaderboards);
        }
    }

    public void PopulateUserTrophies()
    {
        userTrophies = new List<TheTrophy>();
        Trophies.Get(allTrophies =>
        {
            foreach (var item in allTrophies)
            {
                userTrophies.Add(new TheTrophy($"{item.Difficulty}:{item.Title}",item.ID, item.Unlocked));
            }
        });
    }

    public void ShowTrophies()
    {
        if (loggedOn)
        {
            GameJoltUI.Instance.ShowTrophies();
        }
        else
        {
            ShowLogin(GameJoltUI.Instance.ShowTrophies);
        }
    }

    
    public void CheckLogSuccess(bool success, System.Delegate myFunction = null)
    {
        SetLogStatus();

        if (myFunction != null && loggedOn)
        {
            myFunction.DynamicInvoke();
        }
        if (loggedOn)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Something went wrong!");
        }
    }

    private void SetLogStatus()
    {
#if !UNITY_WEBGL
        loggedOn = GameJoltAPI.Instance.CurrentUser != null;
        logInButton.gameObject.SetActive(!loggedOn);
        logOutButton.gameObject.SetActive(loggedOn);
#else
        loggedOn = GameJoltAPI.Instance.HasSignedInUser;
#endif
        if (loggedOn)
        {
            PopulateUserTrophies();
        }
    }

    public void AddScore(int score, int levelNumber)
    {
        if (!loggedOn)
        {
            return;
        }

        switch (levelNumber)
        {
            case 0:
                Scores.Add(score, $"{score} points.", (int)ScoreboardID.Level1);
                break;
            case 1:
                Scores.Add(score, $"{score} points.", (int)ScoreboardID.Level2);
                break;
            case 2:
                Scores.Add(score, $"{score} points.", (int)ScoreboardID.Level3);
                break;
            case 3:
                Scores.Add(score, $"{score} points.", (int)ScoreboardID.Level4);
                break;
            case 4:
                Scores.Add(score, $"{score} points.", (int)ScoreboardID.Level5);
                break;
            default:
                break;
        }

        AddLevelTrophy(levelNumber);
    }

    public void AddLevelTrophy(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                CheckAndAddTrophy(TrophiesID.Level1);
                break;
            case 1:
                CheckAndAddTrophy(TrophiesID.Level2);
                break;
            case 2:
                CheckAndAddTrophy(TrophiesID.Level3);
                break;
            case 3:
                CheckAndAddTrophy(TrophiesID.Level4);
                break;
            case 4:
                CheckAndAddTrophy(TrophiesID.Level5);
                break;
            default:
                break;
        }
    }

    public void CheckAndAddTrophy(TrophiesID trophyID, params TrophiesID[] trophyIDs)
    {

        if (!loggedOn)
        {
            return;
        }

        foreach (var item in trophyIDs)
        {
            var foundTrophy = userTrophies.Find(x => x.id == (int)item);
            if (foundTrophy != null && !foundTrophy.unlocked)
            {
                Debug.Log($"Trophy id {item} still locked.");
                return;
            }
            foundTrophy = userTrophies.Find(x => x.id == (int)trophyID);
            if (foundTrophy != null && foundTrophy.unlocked)
            {
                Debug.Log($"Trophy id {trophyID} is unlocked already.");
                return;
            }
        }

        Trophies.Get((int)trophyID, theTrophy => CheckTrophy(theTrophy, trophyID));
    }
    
    private void CheckTrophy(Trophy theTrophy, TrophiesID trophyID)
    {
        bool unlocked = theTrophy.Unlocked;
        
        if (!unlocked)
        {
            Trophies.Unlock((int)trophyID, success => CheckLogSuccess(success));
            var foundTrophy = userTrophies.Find(x => x.id == (int)trophyID);
            if (foundTrophy != null)
            {
                foundTrophy.unlocked = true;
            }
            CheckPlatinumTrophy();
        }
        else
        {
            Debug.Log($"{theTrophy.Title} was already acquired.");
        }
    }

    private void CheckPlatinumTrophy()
    {
        CheckAndAddTrophy(TrophiesID.PlatinumAllTrophies,
            TrophiesID.BuyTower,
            TrophiesID.UpgradeTower,
            TrophiesID.SellItem,
            TrophiesID.BuyBarrier,
            TrophiesID.KillWorker,
            TrophiesID.KillSoldier,
            TrophiesID.KillFlyer,
            TrophiesID.KillQueen,
            TrophiesID.Level1,
            TrophiesID.Level2,
            TrophiesID.Level3,
            TrophiesID.Level4,
            TrophiesID.Level5);
    }

    public void TestScore()
    {
        if (loggedOn)
        {
            int theScore = Random.Range(10000, 10000000);
            Scores.Add(theScore, $"{theScore} points.", 421403);
        }
    }
}
