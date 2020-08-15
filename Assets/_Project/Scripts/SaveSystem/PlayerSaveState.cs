using System;
using System.Xml.Schema;

[Serializable]
public class PlayerSaveState
{
    [UnityEngine.SerializeField]
    private int lastLevel;

    [UnityEngine.SerializeField]
    private bool eventSent , isAdRemoved , isConsentGiven , shownReviewUs;

    [UnityEngine.SerializeField]
    private int highestScore , currentScore , gold , gem;

    [UnityEngine.SerializeField]
    private string uuid , userName;

    public PlayerSaveState()
    {
        lastLevel = 1;
        ///////////////////////////
        eventSent = false;
        isAdRemoved = false;
        isConsentGiven = false;
        shownReviewUs = false;
        ///////////////////////////
        highestScore = 0;
        currentScore = 0;
        gold = 0;
        gem = 0;
        //////////////////////////
        uuid = null;
        userName = null;
    }

    public int LastLevel
    {
        set
        {
            lastLevel = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return lastLevel;
        }
    }

    public bool EventSent
    {
        set
        {
            eventSent = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return eventSent;
        }
    }

    public bool IsAdRemoved
    {
        set
        {
            isAdRemoved = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return isAdRemoved;
        }
    }

    public bool IsConsentGiven
    {
        set
        {
            isConsentGiven = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return isConsentGiven;
        }
    }

    public bool ShownReviewUs
    {
        set
        {
            shownReviewUs = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return shownReviewUs;
        }
    }

    public int HighestScore
    {
        set
        {
            highestScore = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return highestScore;
        }
    }

    public int CurrentScore
    {
        set
        {
            currentScore = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return currentScore;
        }
    }

    public int Gold
    {
        set
        {
            gold = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return gold;
        }
    }

    public int Gem
    {
        set
        {
            gem = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return gem;
        }
    }

    public string UUID
    {
        set
        {
            uuid = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return uuid;
        }
    }

    public string UserName
    {
        set
        {
            userName = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return userName;
        }
    }
}